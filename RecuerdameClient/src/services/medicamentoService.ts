import type { ApiResponse, PaginatedDatos, Medicamento, MedicamentoRequest } from "../types";
import { HttpService } from "./httpservice";

export class MedicamentoService extends HttpService {
    private static instance: MedicamentoService;
    private readonly enpoint = '/Medicamento';
    private constructor() {
        super();
    }

    public static getInstance(): MedicamentoService {
        if (!MedicamentoService.instance) {
            MedicamentoService.instance = new MedicamentoService();
        }
        return MedicamentoService.instance;
    }

    async getMedicamentos(): Promise<Medicamento[]> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<Medicamento>>>(this.enpoint);
        return response.data.datos.items;
    }

    async getMedicamento(id: number): Promise<Medicamento> {
        const response = await this.http.get<ApiResponse<Medicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }

    async createMedicamento(medicamento: MedicamentoRequest) {
        const response = await this.http.post<ApiResponse<Medicamento>>(this.enpoint, medicamento)
        return response.data.datos;
    }

    async updateMedicamento(id: number, medicamento: MedicamentoRequest) {
        const response = await this.http.put<ApiResponse<Medicamento>>(`${this.enpoint}/${id}`, medicamento)
        return response.data.datos;
    }

    async deleteMedicamento(id: number) {
        const response = await this.http.delete<ApiResponse<Medicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }
}