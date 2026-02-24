<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { TomaProgramadaService } from '@/services/tomaProgramadaService'
import type { TomaProgramada } from '@/types'
import { EstadoToma } from '@/enums/enums'

const tomas = ref<TomaProgramada[]>([])
const loading = ref(false)

// Filtros
const filtroEstado = ref<EstadoToma | null>(null)
const filtroFechaDesde = ref<string>('')
const filtroFechaHasta = ref<string>('')

// Paginación
const paginaActual = ref(1)
const tamanoPagina = ref(15)
const totalRegistros = ref(0)
const totalPaginas = ref(0)

const estadoOpciones = [
  { label: 'Todos', value: null },
  { label: 'Pendiente', value: EstadoToma.Pendiente },
  { label: 'Realizada', value: EstadoToma.Realizada },
  { label: 'Omitida', value: EstadoToma.Omitida },
]

const cargarTomas = async () => {
  loading.value = true
  try {
    const result = await TomaProgramadaService.getInstance().getTomas({
      estadoToma: filtroEstado.value ?? undefined,
      fechaDesde: filtroFechaDesde.value || undefined,
      fechaHasta: filtroFechaHasta.value || undefined,
      pagina: paginaActual.value,
      tamanoPagina: tamanoPagina.value,
    })
    tomas.value = result.items
    totalRegistros.value = result.totalRegistros
    totalPaginas.value = result.totalPaginas
  } finally {
    loading.value = false
  }
}

const aplicarFiltros = () => {
  paginaActual.value = 1
  cargarTomas()
}

const limpiarFiltros = () => {
  filtroEstado.value = null
  filtroFechaDesde.value = ''
  filtroFechaHasta.value = ''
  paginaActual.value = 1
  cargarTomas()
}

const irPagina = (pagina: number) => {
  if (pagina < 1 || pagina > totalPaginas.value) return
  paginaActual.value = pagina
  cargarTomas()
}

const paginasVisibles = computed(() => {
  const paginas: number[] = []
  const inicio = Math.max(1, paginaActual.value - 2)
  const fin = Math.min(totalPaginas.value, paginaActual.value + 2)
  for (let i = inicio; i <= fin; i++) paginas.push(i)
  return paginas
})

const estadoBadge = (estado: EstadoToma) => {
  switch (estado?.toString()) {
    case EstadoToma.Realizada:
      return { label: 'Realizada', bg: '#dcfce7', color: '#15803d', dot: '#22c55e' }
    case EstadoToma.Omitida:
      return { label: 'Omitida', bg: '#fee2e2', color: '#b91c1c', dot: '#ef4444' }
    default:
      return { label: 'Pendiente', bg: '#fef9c3', color: '#a16207', dot: '#eab308' }
  }
}

const formatFecha = (fecha: Date | string) => {
  if (!fecha) return '—'
  return new Date(fecha).toLocaleString('es-ES', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  })
}

const contadorEstado = (estado: EstadoToma) =>
  tomas.value.filter(t => t.estadoToma?.toString() === estado).length

onMounted(cargarTomas)
</script>

<template>
  <div class="space-y-5">

    <!-- ── Page Header ─────────────────────────────────── -->
    <div class="flex flex-col md:flex-row md:items-end justify-between gap-4 bg-white border border-[#e1e8f5] rounded-2xl p-6 shadow-sm">
      <div>
        <p class="inline-flex items-center gap-1.5 text-[10px] font-bold uppercase tracking-widest text-[#3366ee] mb-1.5">
          <i class="pi pi-history text-[9px]"></i>
          Seguimiento de Tratamiento
        </p>
        <h1 class="text-[1.75rem] font-extrabold text-[#0d1b3e] tracking-tight leading-tight mb-1">
          Historial de Tomas
        </h1>
        <p class="text-[13px] text-[#8a97b4]">
          Consulta el registro completo de tomas programadas, realizadas y omitidas.
        </p>
      </div>
      <div class="flex gap-2.5 flex-shrink-0">
        <span class="inline-flex items-center gap-1.5 px-3 py-1.5 bg-[#f0f4fb] border border-[#e1e8f5] rounded-lg text-[12px] font-semibold text-[#4a5878]">
          <i class="pi pi-list text-[11px] text-[#3366ee]"></i>
          {{ totalRegistros }} registros
        </span>
      </div>
    </div>

    <!-- ── Summary Strip ──────────────────────────────── -->
    <div class="flex flex-wrap gap-3">
      <div class="inline-flex items-center gap-3 bg-white border border-[#e1e8f5] rounded-[14px] px-5 py-3.5 shadow-sm">
        <div class="w-2 h-2 rounded-full bg-[#eab308]"></div>
        <div class="flex items-baseline gap-1.5">
          <span class="text-[20px] font-extrabold text-[#0d1b3e] tracking-tight leading-none">
            {{ contadorEstado(EstadoToma.Pendiente) }}
          </span>
          <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Pendientes</span>
        </div>
      </div>
      <div class="inline-flex items-center gap-3 bg-white border border-[#e1e8f5] rounded-[14px] px-5 py-3.5 shadow-sm">
        <div class="w-2 h-2 rounded-full bg-[#22c55e]"></div>
        <div class="flex items-baseline gap-1.5">
          <span class="text-[20px] font-extrabold text-[#059669] tracking-tight leading-none">
            {{ contadorEstado(EstadoToma.Realizada) }}
          </span>
          <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Realizadas</span>
        </div>
      </div>
      <div class="inline-flex items-center gap-3 bg-white border border-[#e1e8f5] rounded-[14px] px-5 py-3.5 shadow-sm">
        <div class="w-2 h-2 rounded-full bg-[#ef4444]"></div>
        <div class="flex items-baseline gap-1.5">
          <span class="text-[20px] font-extrabold text-[#b91c1c] tracking-tight leading-none">
            {{ contadorEstado(EstadoToma.Omitida) }}
          </span>
          <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Omitidas</span>
        </div>
      </div>
    </div>

    <!-- ── Filtros ─────────────────────────────────────── -->
    <div class="bg-white border border-[#e1e8f5] rounded-2xl p-5 shadow-sm">
      <p class="text-[11px] font-bold uppercase tracking-widest text-[#8a97b4] mb-4">Filtros</p>
      <div class="flex flex-wrap gap-3 items-end">

        <!-- Estado -->
        <div class="flex flex-col gap-1.5 min-w-[150px]">
          <label class="text-[11px] font-semibold text-[#4a5878] uppercase tracking-wide">Estado</label>
          <select
            v-model="filtroEstado"
            class="px-3 py-2 text-[13px] font-medium text-[#0d1b3e] bg-[#f8faff] border border-[#e1e8f5] rounded-xl focus:outline-none focus:border-[#3366ee] focus:ring-2 focus:ring-[#3366ee]/10 transition-all"
          >
            <option v-for="op in estadoOpciones" :key="String(op.value)" :value="op.value">
              {{ op.label }}
            </option>
          </select>
        </div>

        <!-- Fecha desde -->
        <div class="flex flex-col gap-1.5 min-w-[160px]">
          <label class="text-[11px] font-semibold text-[#4a5878] uppercase tracking-wide">Desde</label>
          <input
            v-model="filtroFechaDesde"
            type="date"
            class="px-3 py-2 text-[13px] font-medium text-[#0d1b3e] bg-[#f8faff] border border-[#e1e8f5] rounded-xl focus:outline-none focus:border-[#3366ee] focus:ring-2 focus:ring-[#3366ee]/10 transition-all"
          />
        </div>

        <!-- Fecha hasta -->
        <div class="flex flex-col gap-1.5 min-w-[160px]">
          <label class="text-[11px] font-semibold text-[#4a5878] uppercase tracking-wide">Hasta</label>
          <input
            v-model="filtroFechaHasta"
            type="date"
            class="px-3 py-2 text-[13px] font-medium text-[#0d1b3e] bg-[#f8faff] border border-[#e1e8f5] rounded-xl focus:outline-none focus:border-[#3366ee] focus:ring-2 focus:ring-[#3366ee]/10 transition-all"
          />
        </div>

        <!-- Acciones -->
        <div class="flex gap-2 mt-auto">
          <button
            @click="aplicarFiltros"
            class="inline-flex items-center gap-2 px-4 py-2 text-[13px] font-semibold text-white bg-gradient-to-r from-[#3366ee] to-[#1e4fd8] rounded-xl shadow-md shadow-blue-500/20 hover:-translate-y-0.5 hover:shadow-lg transition-all duration-200"
          >
            <i class="pi pi-filter text-[11px]"></i>
            Filtrar
          </button>
          <button
            @click="limpiarFiltros"
            class="inline-flex items-center gap-2 px-4 py-2 text-[13px] font-semibold text-[#4a5878] bg-white border border-[#e1e8f5] rounded-xl hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff] transition-all duration-200"
          >
            <i class="pi pi-times text-[11px]"></i>
            Limpiar
          </button>
        </div>
      </div>
    </div>

    <!-- ── Table ──────────────────────────────────────── -->
    <div class="bg-white border border-[#e1e8f5] rounded-2xl shadow-sm overflow-hidden">

      <!-- Loading skeleton -->
      <div v-if="loading" class="p-6 space-y-3">
        <div v-for="i in 5" :key="i" class="h-12 bg-[#f0f4fb] rounded-xl animate-pulse"></div>
      </div>

      <!-- Empty state -->
      <div v-else-if="tomas.length === 0" class="flex flex-col items-center justify-center py-16 px-6 text-center">
        <div class="w-14 h-14 rounded-full bg-[#f0f4fb] flex items-center justify-center mb-4">
          <i class="pi pi-inbox text-[24px] text-[#8a97b4]"></i>
        </div>
        <p class="text-[15px] font-semibold text-[#0d1b3e] mb-1">Sin resultados</p>
        <p class="text-[13px] text-[#8a97b4]">No hay tomas que coincidan con los filtros aplicados.</p>
      </div>

      <!-- Table -->
      <div v-else class="overflow-x-auto">
        <table class="w-full text-[13px]">
          <thead>
            <tr class="border-b border-[#e1e8f5] bg-[#f8faff]">
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">#</th>
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">Fecha Programada</th>
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">Medicamento</th>
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">Categoría</th>
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">Estado</th>
              <th class="text-left px-5 py-3.5 text-[10px] font-bold uppercase tracking-widest text-[#8a97b4] whitespace-nowrap">Confirmación</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(toma, idx) in tomas"
              :key="toma.id"
              class="border-b border-[#f0f4fb] last:border-0 hover:bg-[#f8faff] transition-colors"
            >
              <!-- # -->
              <td class="px-5 py-3.5 text-[#8a97b4] font-medium">
                {{ (paginaActual - 1) * tamanoPagina + idx + 1 }}
              </td>
              <!-- Fecha programada -->
              <td class="px-5 py-3.5 font-medium text-[#0d1b3e] whitespace-nowrap">
                {{ formatFecha(toma.fechaHoraProgramada) }}
              </td>
              <!-- Medicamento -->
              <td class="px-5 py-3.5">
                <div class="flex items-center gap-2.5">
                  <div class="w-7 h-7 rounded-lg bg-gradient-to-br from-[#3366ee]/10 to-[#3366ee]/20 flex items-center justify-center shrink-0">
                    <i class="pi pi-heart-fill text-[10px] text-[#3366ee]"></i>
                  </div>
                  <span class="font-semibold text-[#0d1b3e]">{{ toma.medicamentoNombre || toma.medicamento?.nombre || '—' }}</span>
                </div>
              </td>
              <!-- Categoría -->
              <td class="px-5 py-3.5 text-[#4a5878]">
                {{ toma.categoriaNombre || toma.medicamento?.CategoriasName || '—' }}
              </td>
              <!-- Estado -->
              <td class="px-5 py-3.5">
                <span
                  class="inline-flex items-center gap-1.5 px-2.5 py-1 rounded-full text-[11px] font-bold"
                  :style="{ backgroundColor: estadoBadge(toma.estadoToma).bg, color: estadoBadge(toma.estadoToma).color }"
                >
                  <span
                    class="w-1.5 h-1.5 rounded-full"
                    :style="{ backgroundColor: estadoBadge(toma.estadoToma).dot }"
                  ></span>
                  {{ estadoBadge(toma.estadoToma).label }}
                </span>
              </td>
              <!-- Confirmación -->
              <td class="px-5 py-3.5 text-[#8a97b4] whitespace-nowrap">
                {{ toma.fechaHoraConfirmacion ? formatFecha(toma.fechaHoraConfirmacion) : '—' }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- Pagination -->
      <div v-if="totalPaginas > 1" class="flex items-center justify-between px-5 py-4 border-t border-[#f0f4fb]">
        <span class="text-[12px] text-[#8a97b4]">
          Página <strong class="text-[#0d1b3e]">{{ paginaActual }}</strong> de <strong class="text-[#0d1b3e]">{{ totalPaginas }}</strong>
          &nbsp;·&nbsp; {{ totalRegistros }} registros
        </span>
        <div class="flex items-center gap-1">
          <button
            @click="irPagina(paginaActual - 1)"
            :disabled="paginaActual === 1"
            class="w-8 h-8 rounded-lg flex items-center justify-center text-[12px] text-[#4a5878] border border-[#e1e8f5] hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff] disabled:opacity-30 disabled:cursor-not-allowed transition-all"
          >
            <i class="pi pi-angle-left"></i>
          </button>
          <button
            v-for="pagina in paginasVisibles"
            :key="pagina"
            @click="irPagina(pagina)"
            class="w-8 h-8 rounded-lg flex items-center justify-center text-[12px] font-semibold border transition-all"
            :class="pagina === paginaActual
              ? 'bg-[#3366ee] text-white border-[#3366ee] shadow-sm'
              : 'text-[#4a5878] border-[#e1e8f5] hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff]'"
          >
            {{ pagina }}
          </button>
          <button
            @click="irPagina(paginaActual + 1)"
            :disabled="paginaActual === totalPaginas"
            class="w-8 h-8 rounded-lg flex items-center justify-center text-[12px] text-[#4a5878] border border-[#e1e8f5] hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff] disabled:opacity-30 disabled:cursor-not-allowed transition-all"
          >
            <i class="pi pi-angle-right"></i>
          </button>
        </div>
      </div>
    </div>

  </div>
</template>
