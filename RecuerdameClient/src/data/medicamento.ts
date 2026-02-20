import { ref } from 'vue'
import type { Medicamento } from '../types'

export const medicamentos = ref<Medicamento[]>([
  {
    id: 100,
    nombre: 'Paracetamol 500mg',
    descripcion: 'Analgésico y antipirético de uso común para el alivio del dolor leve a moderado.',
    image: 'bamboo-watch.jpg',
    dosis: 500,
    frecuencia: '1 vez al día',
    fechaInicio: new Date(),
    fechaFin: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000),
    estado: true,
  },
   {
    id: 1002,
    nombre: 'Paracetamol 500mg',
    descripcion: 'Analgésico y antipirético de uso común para el alivio del dolor leve a moderado.',
    image: 'bamboo-watch.jpg',
    dosis: 500,
    frecuencia: '1 vez al día',
    fechaInicio: new Date(),
    fechaFin: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000),
    estado: false,
  },
   {
    id: 1003,
    nombre: 'Paracetamol 500mg',
    descripcion: 'Analgésico y antipirético de uso común para el alivio del dolor leve a moderado.',
    image: 'bamboo-watch.jpg',
    dosis: 500,
    frecuencia: '1 vez al día',
    fechaInicio: new Date(),
    fechaFin: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000),
    estado: true,
  },
])