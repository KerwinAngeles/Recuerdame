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
  unidad: string
  categoria: string
  colorAccent: string
  colorBg: string
  colorText: string
  icon: string
  tomas: TomaItem[]
}