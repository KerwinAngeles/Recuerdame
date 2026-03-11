import { HttpService } from "./httpservice";
import type { ApiResponse, PaginatedDatos, TomaProgramada, TomaProgramadaRequest } from "../types";
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

    async getTomas(): Promise<TomaProgramada[]> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        console.log("Response getTomas: " + response.data.datos.items.filter(t => t.estadoToma === EstadoToma.Pendiente).length)
        return response.data.datos.items;
    }

     async updateTomaProgramada(id: number, tomaProgramada: TomaProgramadaRequest) {
            await this.http.put<ApiResponse<TomaProgramada>>(`${this.enpoint}/${id}`, tomaProgramada)
        }

    async getCantidadDeTomasRealizadas(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma == EstadoToma.Tomada).length;
    }

    async getCantidadDeTomasPendientes(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma == EstadoToma.Pendiente).length;
    }

    async getCantidadDeTomasOmitidas(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => t.estadoToma == EstadoToma.Cancelada).length;
    }

    async getDosisDeHoy(): Promise<number> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        return response.data.datos.items.filter(t => new Date(t.fechaHoraProgramada).toDateString() === new Date().toDateString()).length;
    }

    async getProximaToma(): Promise<string> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<TomaProgramada>>>(this.enpoint);
        const ahora = new Date();
        const tomasHoy = response.data.datos.items.map(t => new Date(t.fechaHoraProgramada)).filter(fecha => fecha.toDateString() === ahora.toDateString() && fecha > ahora).sort((a, b) => a.getTime() - b.getTime())[0];
        if (tomasHoy) {
            return tomasHoy.toLocaleTimeString('es-DO', {
                hour: '2-digit',
                minute: '2-digit'
            });
        }
        return "6:00";
    }

    async updateToma(id: number, request: TomaProgramadaRequest): Promise<void> {
        await this.http.put(`${this.enpoint}/${id}`, request);
    }

}
