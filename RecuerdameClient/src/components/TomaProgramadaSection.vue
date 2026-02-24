<script setup lang="ts">
import { computed } from 'vue'
import type { MedicamentoConTomas, EstadoToma } from '@/data/tomasProgramadas'

const props = defineProps<{ 
  medicamentos: MedicamentoConTomas[]
  countTomadas: number
  countPendientes: number
  countOmitidas: number
}>()

// Hora actual en formato HH:MM para comparar con las tomas
const now = new Date()
const horaActual = `${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}`
const fechaHoy = now.toLocaleDateString('es', { weekday: 'long', day: 'numeric', month: 'long' })

// Totales globales
const totalTomas = computed(() => props.countTomadas + props.countPendientes + props.countOmitidas)

const porcentajeGlobal = computed(() =>
  totalTomas.value ? Math.round((props.countTomadas / totalTomas.value) * 100) : 0
)

// Próxima toma pendiente más cercana a la hora actual
const proximaTomaId = computed(() => {
  let nextId: number | null = null
  let nextHora = '99:99'
  for (const med of props.medicamentos) {
    for (const toma of med.tomas) {
      if (toma.estado === 'pendiente' && toma.hora >= horaActual && toma.hora < nextHora) {
        nextHora = toma.hora
        nextId = toma.id
      }
    }
  }
  return nextId
})

const proximaTomaBanner = computed(() => {
  for (const med of props.medicamentos) {
    const toma = med.tomas.find(t => t.id === proximaTomaId.value)
    if (toma) return { med, toma }
  }
  return null
})

// Estadísticas por medicamento
function getTomadas(med: MedicamentoConTomas) {
  return med.tomas.filter(t => t.estado === 'tomado').length
}
function getPorcentaje(med: MedicamentoConTomas) {
  return Math.round((getTomadas(med) / med.tomas.length) * 100)
}
// Circunferencia para SVG (r=15.9155 → circunferencia ≈ 100)
function getDashArray(med: MedicamentoConTomas) {
  return `${getPorcentaje(med)} 100`
}

function isProxima(tomaId: number) {
  return tomaId === proximaTomaId.value
}

const estadoConfig: Record<EstadoToma, { bg: string; text: string; border: string; icon: string; label: string }> = {
  tomado:    { bg: '#ecfdf5', text: '#059669', border: '#a7f3d0', icon: 'pi-check',  label: 'Tomado'    },
  pendiente: { bg: '#eef4ff', text: '#3366ee', border: '#bcd3ff', icon: 'pi-clock',  label: 'Pendiente' },
  omitido:   { bg: '#fff1f2', text: '#e11d48', border: '#fecdd3', icon: 'pi-times',  label: 'Omitido'   },
}
</script>

<template>
  <div class="flex flex-col gap-5">

    <!-- ── Cabecera de sección ─────────────────────────────── -->
    <div class="flex flex-col gap-1 md:flex-row md:items-end md:justify-between">
      <div>
        <p class="inline-flex items-center gap-1.5 text-[11px] font-bold uppercase tracking-[0.1em] text-[#8a97b4] mb-1.5 m-0">
          <i class="pi pi-calendar" style="font-size: 11px"></i>
          {{ fechaHoy }}
        </p>
        <h2 class="text-[clamp(1.125rem,2.5vw,1.375rem)] font-extrabold text-[#0d1b3e] mb-0.5 m-0 tracking-[-0.03em] leading-[1.2]">
          Tomas Programadas
        </h2>
        <p class="text-[13px] text-[#8a97b4] m-0">
          {{ countTomadas }} de {{ totalTomas }} dosis completadas hoy
        </p>
      </div>
      <button class="border-none bg-transparent text-[13px] font-semibold text-[#3366ee] cursor-pointer p-0 transition-colors duration-150 whitespace-nowrap hover:text-[#1a3fb5] self-start md:self-end mb-0.5">
        Ver agenda completa →
      </button>
    </div>

    <!-- ── Barra de progreso global ───────────────────────── -->
    <div class="bg-white rounded-[16px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] px-5 py-4">
      <div class="flex items-center justify-between mb-3">
        <div class="flex flex-wrap gap-x-4 gap-y-1">
          <span class="flex items-center gap-1.5 text-[12px] font-semibold text-[#059669]">
            <span class="w-2 h-2 rounded-full bg-[#10b981]"></span>
            {{ countTomadas }} Tomadas
          </span>
          <span class="flex items-center gap-1.5 text-[12px] font-semibold text-[#3366ee]">
            <span class="w-2 h-2 rounded-full bg-[#3366ee]"></span>
            {{ countPendientes }} Pendientes
          </span>
          <span v-if="countOmitidas > 0" class="flex items-center gap-1.5 text-[12px] font-semibold text-[#e11d48]">
            <span class="w-2 h-2 rounded-full bg-[#f43f5e]"></span>
            {{ countOmitidas }} Omitidas
          </span>
        </div>
        <span class="text-[14px] font-extrabold text-[#0d1b3e] tracking-[-0.02em]">{{ porcentajeGlobal }}%</span>
      </div>

      <!-- Track multicolor -->
      <div class="relative h-3 bg-[#f0f4fb] rounded-full overflow-hidden">
        <div
          class="absolute left-0 top-0 h-full rounded-full bg-gradient-to-r from-[#3366ee] to-[#10b981] transition-all duration-700"
          :style="{ width: `${(countTomadas / totalTomas) * 100}%` }"
        ></div>
        <div
          v-if="countOmitidas > 0"
          class="absolute top-0 h-full bg-[#f43f5e] transition-all duration-700"
          :style="{
            left: `${(countTomadas / totalTomas) * 100}%`,
            width: `${(countOmitidas / totalTomas) * 100}%`,
          }"
        ></div>
      </div>
    </div>

    <!-- ── Banner próxima toma ────────────────────────────── -->
    <div
      v-if="proximaTomaBanner"
      class="relative overflow-hidden flex items-center gap-4 bg-gradient-to-r from-[#0d1b3e] via-[#112254] to-[#1e4fd8] rounded-[16px] px-5 py-4 shadow-[0_6px_24px_rgba(13,27,62,0.25)]"
    >
      <!-- Orb decorativo -->
      <div class="absolute -right-12 -top-12 w-44 h-44 rounded-full bg-white/5 pointer-events-none"></div>
      <div class="absolute right-16 -bottom-8 w-28 h-28 rounded-full bg-[#3366ee]/20 pointer-events-none"></div>

      <!-- Icono -->
      <div class="relative w-11 h-11 rounded-[13px] bg-white/15 flex items-center justify-center shrink-0">
        <i class="pi pi-bell text-white text-lg"></i>
        <span class="absolute -top-1 -right-1 w-3 h-3 rounded-full bg-[#f59e0b] border-2 border-[#0d1b3e] animate-pulse"></span>
      </div>

      <!-- Info -->
      <div class="flex-1 min-w-0">
        <p class="text-[10px] font-bold uppercase tracking-[0.1em] text-white/50 m-0 mb-0.5">Próxima Toma</p>
        <p class="text-[15px] font-bold text-white m-0 tracking-[-0.02em]">
          {{ proximaTomaBanner.toma.hora }}
          <span class="text-white/60 font-semibold mx-1.5">—</span>
          {{ proximaTomaBanner.med.nombre }}
          <span
            class="ml-2 text-[11px] font-bold px-2 py-0.5 rounded-full"
            :style="{ background: proximaTomaBanner.med.colorBg, color: proximaTomaBanner.med.colorAccent }"
          >
            {{ proximaTomaBanner.med.dosis }} {{ proximaTomaBanner.med.unidad }}
          </span>
        </p>
      </div>

      <!-- Indicador -->
      <div class="shrink-0 hidden sm:flex items-center gap-2 bg-white/10 rounded-full px-3 py-1.5">
        <span class="w-1.5 h-1.5 rounded-full bg-[#f59e0b] animate-pulse"></span>
        <span class="text-[11px] font-semibold text-white/80">Próximamente</span>
      </div>
    </div>

    <!-- ── Tarjetas por medicamento ────────────────────────── -->
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2">
      <div
        v-for="med in medicamentos"
        :key="med.id"
        class="group relative bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col transition-all duration-[250ms] ease-[cubic-bezier(0.4,0,0.2,1)] hover:-translate-y-1 hover:shadow-[0_16px_40px_rgba(13,27,62,0.12)] hover:border-[rgba(51,102,238,0.15)]"
      >
        <!-- Barra de acento izquierda con color del medicamento -->
        <div
          class="absolute left-0 top-0 bottom-0 w-1 rounded-r-full"
          :style="{ background: med.colorAccent }"
        ></div>

        <div class="pl-6 pr-5 pt-5 pb-5 flex flex-col gap-4">

          <!-- Fila superior: icono + nombre + anillo de progreso -->
          <div class="flex items-start gap-3">
            <!-- Icono -->
            <div
              class="w-10 h-10 rounded-[12px] flex items-center justify-center shrink-0 text-base transition-all duration-[250ms] group-hover:scale-110 group-hover:shadow-lg"
              :style="{ background: med.colorBg, color: med.colorAccent }"
            >
              <i :class="`pi ${med.icon}`"></i>
            </div>

            <!-- Nombre y categoría -->
            <div class="flex-1 min-w-0 pt-0.5">
              <h3 class="text-[14px] font-bold text-[#0d1b3e] tracking-[-0.02em] truncate m-0 mb-[3px]">
                {{ med.nombre }}
              </h3>
              <div class="flex items-center gap-1.5 flex-wrap">
                <span class="text-[11px] text-[#8a97b4]">{{ med.categoria }}</span>
                <span
                  class="text-[10px] font-bold px-1.5 py-0.5 rounded-[5px]"
                  :style="{ background: med.colorBg, color: med.colorAccent }"
                >
                  {{ med.dosis }} {{ med.unidad }}
                </span>
              </div>
            </div>

            <!-- Anillo SVG de progreso -->
            <div class="relative w-10 h-10 shrink-0">
              <svg class="w-10 h-10 -rotate-90" viewBox="0 0 36 36">
                <circle
                  cx="18" cy="18" r="15.9155"
                  fill="none"
                  stroke="#e1e8f5"
                  stroke-width="3"
                />
                <circle
                  cx="18" cy="18" r="15.9155"
                  fill="none"
                  :stroke="med.colorAccent"
                  stroke-width="3"
                  stroke-linecap="round"
                  :stroke-dasharray="getDashArray(med)"
                  class="transition-all duration-700"
                />
              </svg>
              <span
                class="absolute inset-0 flex items-center justify-center text-[9px] font-extrabold tracking-tight"
                :style="{ color: med.colorAccent }"
              >
                {{ getPorcentaje(med) }}%
              </span>
            </div>
          </div>

          <!-- Separador punteado -->
          <div class="border-t border-dashed border-[#e1e8f5]"></div>

          <!-- Pills de tomas -->
          <div class="flex flex-wrap gap-2">
            <div
              v-for="toma in med.tomas"
              :key="toma.id"
              class="relative flex items-center gap-[5px] text-[11px] font-semibold px-3 py-[7px] rounded-full border transition-all duration-200 select-none"
              :style="{
                background: estadoConfig[toma.estado].bg,
                color: estadoConfig[toma.estado].text,
                borderColor: estadoConfig[toma.estado].border,
              }"
            >
              <i :class="`pi ${estadoConfig[toma.estado].icon}`" class="text-[10px]"></i>
              <span class="font-bold">{{ toma.hora }}</span>
              <span class="opacity-60 text-[10px]">{{ estadoConfig[toma.estado].label }}</span>

              <!-- Indicador pulsante si es la próxima toma -->
              <span
                v-if="isProxima(toma.id)"
                class="absolute -top-1 -right-1 w-2.5 h-2.5 rounded-full border-2 border-white animate-ping"
                :style="{ background: med.colorAccent }"
              ></span>
              <span
                v-if="isProxima(toma.id)"
                class="absolute -top-1 -right-1 w-2.5 h-2.5 rounded-full border-2 border-white"
                :style="{ background: med.colorAccent }"
              ></span>
            </div>
          </div>

          <!-- Barra de progreso lineal + contador -->
          <div class="flex items-center gap-3">
            <div class="flex-1 h-1.5 bg-[#f0f4fb] rounded-full overflow-hidden">
              <div
                class="h-full rounded-full transition-all duration-700"
                :style="{ width: `${getPorcentaje(med)}%`, background: med.colorAccent }"
              ></div>
            </div>
            <span class="text-[10px] font-bold text-[#8a97b4] shrink-0 tabular-nums">
              {{ getTomadas(med) }}/{{ med.tomas.length }}
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
