import type { CategoriaMedicamento, ApiResponse, PaginatedDatos, CategoriaRequest } from "../types";
import { HttpService } from "./httpservice";

export class CategoryService extends HttpService {
    private static instance: CategoryService;
    private readonly enpoint = '/CategoriaMedicamento';
    private constructor() {
        super();
    }

    public static getInstance(): CategoryService {
        if (!CategoryService.instance) {
            CategoryService.instance = new CategoryService();
        }
        return CategoryService.instance;
    }

    async getCategories(): Promise<CategoriaMedicamento[]> {
        const response = await this.http.get<ApiResponse<PaginatedDatos<CategoriaMedicamento>>>(this.enpoint);
        return response.data.datos.items;
    }

    async getCantidadMedicamentosAsignadoCategoria(id: number) {
        const response = await this.http.get<ApiResponse<number>>(`${this.enpoint}/ObtenerCantidadDeMedicamentosAsignadaUnaCategoria/${id}`);
        return response.data.datos;
    }

    async getCategory(id: number): Promise<CategoriaMedicamento> {
        const response = await this.http.get<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }

    async createCategory(category: CategoriaRequest) {
        const response = await this.http.post<ApiResponse<CategoriaMedicamento>>(this.enpoint, category)
        return response.data.datos;
    }

    async updateCategory(id: number, category: CategoriaRequest) {
        const response = await this.http.put<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`, category)
        return response.data.datos;
    }

    async deleteCategory(id: number) {
        const response = await this.http.delete<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }
}