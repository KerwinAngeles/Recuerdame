import axios, { type AxiosInstance, AxiosError } from "axios";

export class HttpService {
    protected readonly http: AxiosInstance;
    protected constructor() {
        this.http = axios.create({
            baseURL: import.meta.env.VITE_API_URL,
            headers: {
                'Content-Type': 'application/json',
            },
        });
        this.setupInterceptors();
    }

    private setupInterceptors() {
        this.http.interceptors.response.use(
            (response) => response,
            (error: AxiosError) => Promise.reject(this.handleError(error))
        );
    }

    private handleError(error: AxiosError): Error {
        const status = error.response?.status;
        const message =
            (error.response?.data as { message?: string })?.message ??
            error.message;

        const errorMap: Record<number, string> = {
            400: `Bad Request: ${message}`,
            403: 'No tienes permisos para realizar esta acción',
            404: 'Recurso no encontrado',
            500: 'Error interno del servidor',
        };

        return new Error(status ? (errorMap[status] ?? message) : message);
    }
}