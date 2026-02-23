import { HttpService } from "./httpservice";
import type { ApiResponse, PaginatedDatos, TomaProgramada } from "../types";
import { EstadoToma } from "../enums/enums";

export interface TomaProgramadaFiltros {
    medicamentoId?: number
    estadoToma?: EstadoToma
    fechaDesde?: string
    fechaHasta?: string
    pagina?: number
    tamanoPagina?: number
}

export class TomaProgramadaService extends HttpService {

    private static instance: TomaProgramadaService
    private readonly enpoint = '/TomaProgramada';
    private constructor() {
        super();
    }
    public static getInstance(): TomaProgramadaService {
        if (!TomaProgramadaService.instance) {
            TomaProgramadaService.instance = new TomaProgramadaService()
        }
        return TomaProgramadaService.instance
    }

    async getTomas(filtros?: TomaProgramadaFiltros): Promise<PaginatedDatos<TomaProgramada>> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint, { params: filtros });
        return response.data.datos;
    }

    async getCantidadDeTomasRealizadas(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma.toString() === EstadoToma.Realizada).length;
    }

    async getCantidadDeTomasPendientes(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma.toString() === EstadoToma.Pendiente).length;
    }

    async getCantidadDeTomasOmitidas(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma.toString() === EstadoToma.Omitida).length;
    }

}
