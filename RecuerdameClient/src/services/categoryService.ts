import type { CategoriaMedicamento, ApiResponse, PaginatedDatos } from "../types";
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

    async getCategory(id: number): Promise<CategoriaMedicamento> {
        const response = await this.http.get<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }

    async createCategory(category: any) {
        const response = await this.http.post<ApiResponse<CategoriaMedicamento>>(this.enpoint)
        return response.data.datos;
    }

    async updateCategory(id: number, category: any) {
        const response = await this.http.put<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`)
        return response.data.datos;
    }

    async deleteCategory(id: number) {
        const response = await this.http.delete<ApiResponse<CategoriaMedicamento>>(`${this.enpoint}/${id}`);
        return response.data.datos;
    }
}