import { EstadoToma } from "../enums/enums";

export interface TomaItem {
  id: number
  hora: string
  estado: EstadoToma
}

export interface MedicamentoConTomas {
  id: number
  nombre: string
  dosis: number
  categoria: string
  tomas: TomaItem[]
}