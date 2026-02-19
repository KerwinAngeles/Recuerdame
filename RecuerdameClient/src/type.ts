export interface Product {
  id: string
  nombre: string
  descripcion: string
  image?: string
  dosis: number
  frecuencia: string
  fechaInicio: Date
  fechaFin: Date
  estado: boolean
}
