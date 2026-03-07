<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import type { MedicamentoConTomas} from '@/data/tomasProgramadas'
import { EstadoToma } from '@/enums/enums'
import { TomaProgramadaService } from '@/services/tomaProgramadaService'
import type { TomaProgramada } from '@/types';

const props = defineProps<{ 
  medicamentos: MedicamentoConTomas[]
  countTomadas: number
  countPendientes: number
  countOmitidas: number
}>()

const medicamentosConTomasApi = ref<TomaProgramada[]>([]);

const cargarTomasProgramadas = async () => {
  const service = TomaProgramadaService.getInstance();
  const tomas = await service.getTomas();
  const mapa = new Map<number, TomaProgramada>();
  tomas.items.forEach(toma => {
    if (!mapa.has(toma.medicamentoId)) {
      mapa.set(toma.medicamentoId, {
        id: toma.medicamentoId,
        medicamentoId: toma.medicamentoId,
        medicamentoNombre: toma.medicamentoNombre,
        fechaHoraProgramada: toma.fechaHoraProgramada,
        fechaHoraConfirmacion: toma.fechaHoraConfirmacion,
        estadoToma: toma.estadoToma,
        categoriaNombre: toma.categoriaNombre,
        dosis: toma.dosis,
        frecuenciaHora: toma.frecuenciaHora
      })
    }
  })

  medicamentosConTomasApi.value = Array.from(mapa.values())
}

onMounted(() => {
  cargarTomasProgramadas()
})

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
      if (toma.estado === EstadoToma.Pendiente && toma.hora >= horaActual && toma.hora < nextHora) {
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


const estadoConfig: Record<EstadoToma, { bg: string; text: string; border: string; icon: string; label: string }> = {
  [EstadoToma.Tomada]:    { bg: '#ecfdf5', text: '#059669', border: '#a7f3d0', icon: 'pi-check',  label: 'Tomado'    },
  [EstadoToma.Pendiente]: { bg: '#eef4ff', text: '#3366ee', border: '#bcd3ff', icon: 'pi-clock',  label: 'Pendiente' },
  [EstadoToma.Cancelada]:   { bg: '#fff1f2', text: '#e11d48', border: '#fecdd3', icon: 'pi-times',  label: 'Cancelado'   },
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
          >
            {{ proximaTomaBanner.med.dosis }}
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
        v-for="med in medicamentosConTomasApi"
        :key="med.id"
        class="group relative bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col transition-all duration-[250ms] ease-[cubic-bezier(0.4,0,0.2,1)] hover:-translate-y-1 hover:shadow-[0_16px_40px_rgba(13,27,62,0.12)]"
      >
        <!-- Franja de acento izquierda por estado -->
        <div
          class="absolute left-0 top-0 bottom-0 w-[3.5px]"
          :style="{ background: estadoConfig[med.estadoToma]?.text ?? '#8a97b4' }"
        ></div>

        <div class="pl-6 pr-5 pt-5 pb-5 flex flex-col gap-4">

          <!-- Fila superior: icono + nombre + badge de estado -->
          <div class="flex items-start gap-3">

            <!-- Icono circular por estado -->
            <div
              class="w-10 h-10 rounded-[12px] flex items-center justify-center shrink-0"
              :style="{ background: estadoConfig[med.estadoToma]?.bg ?? '#f1f5f9' }"
            >
              <i
                class="pi text-base"
                :class="estadoConfig[med.estadoToma]?.icon ?? 'pi-circle'"
                :style="{ color: estadoConfig[med.estadoToma]?.text ?? '#8a97b4' }"
              ></i>
            </div>

            <!-- Nombre y categoría -->
            <div class="flex-1 min-w-0 pt-0.5">
              <h3 class="text-[14px] font-bold text-[#0d1b3e] tracking-[-0.02em] truncate m-0 mb-[3px]">
                {{ med.medicamentoNombre }}
              </h3>
              <p class="text-[11px] text-[#8a97b4] m-0">
                {{ med.categoriaNombre }}
                <span class="text-[#d1d9ec] mx-1">·</span>
                Cada {{ med.frecuenciaHora }}h
              </p>
            </div>

            <!-- Badge de estado -->
            <span
              class="inline-flex items-center gap-1 text-[10px] font-bold px-2 py-1 rounded-full border shrink-0"
              :style="{
                background: estadoConfig[med.estadoToma]?.bg ?? '#f1f5f9',
                color: estadoConfig[med.estadoToma]?.text ?? '#8a97b4',
                borderColor: estadoConfig[med.estadoToma]?.border ?? '#e1e8f5'
              }"
            >
              <i class="pi text-[9px]" :class="estadoConfig[med.estadoToma]?.icon"></i>
              {{ estadoConfig[med.estadoToma]?.label }}
            </span>
          </div>

          <!-- Separador punteado -->
          <div class="border-t border-dashed border-[#e1e8f5]"></div>

          <!-- Pie de tarjeta: hora + dosis -->
          <div class="flex items-center justify-between">
            <div class="flex items-center gap-1.5">
              <i class="pi pi-clock text-[11px] text-[#8a97b4]"></i>
              <span class="text-[12px] font-semibold text-[#5a6a8a] tabular-nums">
                {{ new Date(med.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
              </span>
            </div>
            <span
              class="text-[11px] font-bold px-2.5 py-1 rounded-[6px]"
              :style="{
                background: estadoConfig[med.estadoToma]?.bg ?? '#f1f5f9',
                color: estadoConfig[med.estadoToma]?.text ?? '#8a97b4'
              }"
            >
              {{ med.dosis }} mg
            </span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
