export type EstadoToma = 'tomado' | 'pendiente' | 'omitido'

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

export const medicamentosConTomas: MedicamentoConTomas[] = [
  {
    id: 1,
    nombre: 'Paracetamol',
    dosis: 500,
    unidad: 'mg',
    categoria: 'Analgésicos',
    colorAccent: '#3366ee',
    colorBg: '#eef4ff',
    colorText: '#3366ee',
    icon: 'pi-heart-fill',
    tomas: [
      { id: 1, hora: '07:00', estado: 'tomado' },
      { id: 2, hora: '13:00', estado: 'tomado' },
      { id: 3, hora: '19:00', estado: 'pendiente' },
    ],
  },
  {
    id: 2,
    nombre: 'Metformina',
    dosis: 850,
    unidad: 'mg',
    categoria: 'Antidiabéticos',
    colorAccent: '#10b981',
    colorBg: '#ecfdf5',
    colorText: '#059669',
    icon: 'pi-bolt',
    tomas: [
      { id: 4, hora: '08:00', estado: 'tomado' },
      { id: 5, hora: '14:00', estado: 'omitido' },
      { id: 6, hora: '20:00', estado: 'pendiente' },
    ],
  },
  {
    id: 3,
    nombre: 'Losartán',
    dosis: 50,
    unidad: 'mg',
    categoria: 'Antihipertensivos',
    colorAccent: '#8b5cf6',
    colorBg: '#f5f3ff',
    colorText: '#7c3aed',
    icon: 'pi-heart',
    tomas: [
      { id: 7, hora: '09:00', estado: 'tomado' },
      { id: 8, hora: '21:00', estado: 'pendiente' },
    ],
  },
  {
    id: 4,
    nombre: 'Omeprazol',
    dosis: 20,
    unidad: 'mg',
    categoria: 'Protectores Gástricos',
    colorAccent: '#f59e0b',
    colorBg: '#fffbeb',
    colorText: '#d97706',
    icon: 'pi-star-fill',
    tomas: [
      { id: 9, hora: '07:30', estado: 'tomado' },
      { id: 10, hora: '19:30', estado: 'pendiente' },
    ],
  },
]
