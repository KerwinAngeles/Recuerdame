import type { EstadoToma } from "./data/tomasProgramadas";

export interface CategoriaMedicamento {
    id: number;
    nombre: string;
    descripcion: string;
    estado: boolean;
    cantidadMedicamentoAsignadoAunaCategoria: number;
}

export interface PaginatedDatos<T> {
    items: T[];
    totalRegistros: number;
    paginaActual: number;
    tamanoPagina: number;
    totalPaginas: number;
    tienePaginaAnterior: boolean;
    tienePaginaSiguiente: boolean;
}

export interface ApiResponse<T> {
    exito: boolean;
    mensaje: string;
    datos: T;
    errores: string[] | null;
}

export interface Medicamento {
    id: number
    nombre: string
    descripcion: string
    image?: string
    dosis: number
    frecuenciaHora: number
    fechaInicio: Date
    fechaFinal: Date
    estaActivo: boolean
    categoriaId: number
    nota: string
    CategoriasName: string
}

export interface Stat {
    title: string,
    value: string,
    icon: string,
    trend: string,
    trendUp: boolean,
    iconBg: string,
    iconColor: string,
    orbColor: string,
    desc: string
}


export interface RecentActivity {
    icon: string,
    text: string,
    sub: string,
    time: string,
    iconBg: string,
    iconColor: string,
}

export interface MedicamentoRequest {
    id: number
    categoriaId: number
    nombre: string
    descripcion: string
    dosis: number
    frecuenciaHora: number
    fechaInicio: Date
    fechaFinal: Date
    nota: string
}

export interface TomaProgramada {
    id: number
    medicamentoId: number
    fechaHoraConfirmacion: Date
    fechaHoraProgramada: Date
    estadoToma: EstadoToma
    medicamento: Medicamento
    medicamentoNombre: string
    categoriaNombre: string
}

export interface TomaProgramadaRequest {
    id?: number
    medicamentoId: number
    fechaHora: Date
    estadoToma: EstadoToma
}

export interface CategoriaRequest {
    id?: number
    nombre: string
    descripcion: string
}

export interface ChartData {
    labels: string[],
    datasets: {
        label: string,
        data: number[],
        fill: boolean,
        borderColor: string,
        backgroundColor: string,
        tension: number,
        pointBackgroundColor: string,
        pointBorderColor: string,
        pointBorderWidth: number,
        pointRadius: number,
        pointHoverRadius: number,
    }[],
}

export interface ChartOptions {
    maintainAspectRatio: boolean,
    plugins: {
        legend: {
            labels: {
                color: string,
                font: {
                    family: string,
                    size: number,
                    weight: string,
                },
                usePointStyle: boolean,
                pointStyleWidth: number,
            },
        },
        tooltip: {
            backgroundColor: string,
            titleColor: string,
            bodyColor: string,
            borderColor: string,
            borderWidth: number,
            padding: number,
            cornerRadius: number,
        },
    },
    scales: {
        x: {
            ticks: {
                color: string,
                font: {
                    family: string,
                    size: number,
                },
            },
            grid: {
                color: string,
                drawBorder: boolean,
            },
            border: {
                display: boolean,
            },
        },
        y: {
            ticks: {
                color: string,
                font: {
                    family: string,
                    size: number,
                },
            },
            grid: {
                color: string,
                drawBorder: boolean,
            },
            border: {
                display: boolean,
            },
        },
    },
}
