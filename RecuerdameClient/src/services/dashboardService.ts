import type { Medicamento, ApiResponse, PaginatedDatos } from "../types";
import { HttpService } from "./httpservice";
export class DashboardService extends HttpService {
    private static instance: DashboardService
    private readonly enpoint = '/Medicamento';
    private constructor() {
        super();
    }
    public static getInstance(): DashboardService {
        if (!DashboardService.instance) {
            DashboardService.instance = new DashboardService()
        }
        return DashboardService.instance
    } 

    async getCantidadDeMedicamentos(): Promise<number> {
            const response = await this.http.get<ApiResponse<PaginatedDatos<Medicamento>>>(this.enpoint);
            return response.data.datos.items.filter(m => m.estaActivo).length; 
    }
}