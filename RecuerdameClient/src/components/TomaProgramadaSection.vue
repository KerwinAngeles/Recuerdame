<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref } from 'vue'
import { EstadoToma } from '@/enums/enums'
import { TomaProgramadaService } from '@/services/tomaProgramadaService'
import type { TomaProgramada } from '@/types'
import RegistrarTomaModal from './RegistrarTomaModal.vue'

const props = defineProps<{
  countTomadas: number
  countPendientes: number
  countCanceladas: number
  proximaToma: string
}>()

const emit = defineEmits<{
  (e: 'updated'): void
}>()

const modalVisible = ref(false)
const tomaSeleccionada = ref<TomaProgramada | null>(null)

const abrirModal = (toma: TomaProgramada) => {
  tomaSeleccionada.value = toma
  modalVisible.value = true
}

const onTomaActualizada = async () => {
  await cargarTomasProgramadas()
  emit('updated')
}

const medicamentosConTomasApi = ref<TomaProgramada[]>([])
const todasLasTomas = ref<TomaProgramada[]>([])

const cargarTomasProgramadas = async () => {
  const service = TomaProgramadaService.getInstance()
  const tomas = await service.getTomas()
  todasLasTomas.value = tomas

  const ahora = new Date()
  const mapa = new Map<number, TomaProgramada>()

  tomas.forEach(toma => {
    const existing = mapa.get(toma.medicamentoId)
    const tomaDate = new Date(toma.fechaHoraProgramada)
    const tomaEsFuturaPendiente = toma.estadoToma === EstadoToma.Pendiente && tomaDate > ahora

    if (!existing) {
      mapa.set(toma.medicamentoId, toma)
    } else {
      const existingDate = new Date(existing.fechaHoraProgramada)
      const existingEsFuturaPendiente = existing.estadoToma === EstadoToma.Pendiente && existingDate > ahora

      // Prioridad: futura pendiente > cualquier otra; si ambas son futuras pendientes, la más cercana
      if (tomaEsFuturaPendiente && !existingEsFuturaPendiente) {
        mapa.set(toma.medicamentoId, toma)
      } else if (tomaEsFuturaPendiente && existingEsFuturaPendiente && tomaDate < existingDate) {
        mapa.set(toma.medicamentoId, toma)
      }
    }
  })

  medicamentosConTomasApi.value = Array.from(mapa.values())
}

const now = ref(new Date())
let clockTimer: ReturnType<typeof setInterval>

onMounted(() => {
  cargarTomasProgramadas()
  clockTimer = setInterval(() => { now.value = new Date() }, 30_000)
})

onUnmounted(() => clearInterval(clockTimer))

const fechaHoy = computed(() =>
  now.value.toLocaleDateString('es', { weekday: 'long', day: 'numeric', month: 'long' })
)

const medicamentosActivos = computed(() =>
  medicamentosConTomasApi.value.filter(m => m.medicamentoActivo)
)

const esBloqueada = (toma: TomaProgramada): boolean =>
  toma.estadoToma === EstadoToma.Pendiente && new Date(toma.fechaHoraProgramada) > now.value

const proximaTomaBanner = computed<TomaProgramada | null>(() => {
  const ahora = now.value
  const pending = todasLasTomas.value
    .filter(t =>
      t.estadoToma === EstadoToma.Pendiente && t.medicamentoActivo == true &&
      new Date(t.fechaHoraProgramada).toDateString() === ahora.toDateString() &&
      new Date(t.fechaHoraProgramada) > ahora
    )
  pending.sort((a, b) => new Date(a.fechaHoraProgramada).getTime() - new Date(b.fechaHoraProgramada).getTime())
  return pending[0] ?? null
})

const totalTomas = computed(() => props.countTomadas + props.countPendientes + props.countCanceladas)
const porcentajeCompletado = computed(() =>
  totalTomas.value > 0 ? Math.round((props.countTomadas / (props.countTomadas + props.countPendientes)) * 100) : 0
)

const estadoConfig: Record<EstadoToma, {
  bg: string; text: string; border: string; icon: string; label: string;
  gradient: string; glow: string
}> = {
  [EstadoToma.Tomada]:    { bg: '#ecfdf5', text: '#059669', border: '#a7f3d0', icon: 'pi-check-circle', label: 'Tomado',    gradient: 'linear-gradient(135deg, #059669, #10b981)', glow: 'rgba(16,185,129,0.2)' },
  [EstadoToma.Pendiente]: { bg: '#eef4ff', text: '#3366ee', border: '#bcd3ff', icon: 'pi-clock',        label: 'Pendiente', gradient: 'linear-gradient(135deg, #3366ee, #06b6d4)', glow: 'rgba(51,102,238,0.2)' },
  [EstadoToma.Cancelada]: { bg: '#fff1f2', text: '#e11d48', border: '#fecdd3', icon: 'pi-times-circle', label: 'Cancelado', gradient: 'linear-gradient(135deg, #e11d48, #fb7185)', glow: 'rgba(225,29,72,0.2)'  },
}
</script>

<template>
  <div class="flex flex-col gap-5">

    <!-- ── Cabecera de sección ─────────────────────────────────────── -->
    <div class="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
      <div>
        <p class="inline-flex items-center gap-1.5 text-[11px] font-bold uppercase tracking-[0.1em] text-[#8a97b4] m-0 mb-1">
          <i class="pi pi-calendar-clock" style="font-size: 11px"></i>
          {{ fechaHoy }}
        </p>
        <h2 class="text-[clamp(1.125rem,2.5vw,1.375rem)] font-extrabold text-[#0d1b3e] m-0 tracking-[-0.03em] leading-[1.2]">
          Tomas Programadas
        </h2>
      </div>

      <!-- Progress tracker -->
      <div class="flex items-center gap-4 shrink-0">
        <div class="flex flex-col gap-1.5 min-w-[160px]">
          <div class="flex items-center justify-between">
            <span class="text-[11px] text-[#8a97b4] font-medium">Completadas hoy</span>
            <span class="text-[11px] font-extrabold text-[#0d1b3e]">{{ countTomadas }}/{{ countTomadas + countPendientes }}</span>
          </div>
          <div class="h-[5px] bg-[#e8edf8] rounded-full overflow-hidden">
            <div
              class="h-full rounded-full transition-all duration-700"
              :style="{
                width: porcentajeCompletado + '%',
                background: 'linear-gradient(90deg, #3366ee, #06b6d4)'
              }"
            ></div>
          </div>
          <div class="flex items-center gap-2 text-[10px] text-[#8a97b4]">
            <span class="inline-flex items-center gap-0.5">
              <span class="w-1.5 h-1.5 rounded-full bg-emerald-400 inline-block"></span> {{ countTomadas }} tomadas
            </span>
            <span class="inline-flex items-center gap-0.5">
              <span class="w-1.5 h-1.5 rounded-full bg-blue-400 inline-block"></span> {{ countPendientes }} pendientes
            </span>
          </div>
        </div>
      </div>
    </div>

    <!-- ── Banner próxima toma ─────────────────────────────────────── -->
    <div
      v-if="proximaTomaBanner"
      class="relative overflow-hidden flex items-center gap-4 rounded-[20px] px-5 py-4"
      style="background: linear-gradient(135deg, #0a1628 0%, #0d2260 40%, #1e4fd8 75%, #0891b2 100%); box-shadow: 0 12px 36px rgba(14,36,120,0.35)"
    >
      <!-- Decorative orbs -->
      <div class="absolute -right-10 -top-10 w-48 h-48 rounded-full pointer-events-none" style="background: radial-gradient(circle, rgba(255,255,255,0.06) 0%, transparent 70%)"></div>
      <div class="absolute right-12 bottom-0 w-32 h-32 rounded-full pointer-events-none" style="background: radial-gradient(circle, rgba(6,182,212,0.15) 0%, transparent 70%)"></div>
      <div class="absolute left-1/3 top-0 w-20 h-20 rounded-full pointer-events-none" style="background: radial-gradient(circle, rgba(99,131,255,0.1) 0%, transparent 70%)"></div>

      <!-- Grid de puntos decorativo -->
      <div class="absolute inset-0 pointer-events-none opacity-[0.04]"
        style="background-image: radial-gradient(circle, white 1px, transparent 1px); background-size: 20px 20px">
      </div>

      <!-- Icono pulsante -->
      <div class="relative w-12 h-12 rounded-[14px] flex items-center justify-center shrink-0" style="background: rgba(255,255,255,0.12); backdrop-filter: blur(12px)">
        <i class="pi pi-bell text-white" style="font-size: 18px"></i>
        <span class="absolute -top-1 -right-1 w-3.5 h-3.5 rounded-full bg-amber-400 border-2 border-[#0d1b3e] animate-pulse"></span>
      </div>

      <!-- Contenido -->
      <div class="flex-1 min-w-0">
        <p class="text-[10px] font-bold uppercase tracking-[0.14em] m-0 mb-0.5" style="color: rgba(255,255,255,0.45)">Próxima Toma</p>
        <div class="flex items-baseline gap-2.5 flex-wrap">
          <span class="text-[20px] font-extrabold text-white tracking-[-0.04em] tabular-nums leading-none">
            {{ new Date(proximaTomaBanner.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
          </span>
          <span class="text-[15px] font-semibold" style="color: rgba(255,255,255,0.85)">
            {{ proximaTomaBanner.medicamentoNombre }}
          </span>
        </div>
        <p class="text-[11px] m-0 mt-1 flex items-center gap-1.5" style="color: rgba(255,255,255,0.45)">
          <span>{{ proximaTomaBanner.dosis }} mg</span>
          <span style="color: rgba(255,255,255,0.2)">·</span>
          <span>{{ proximaTomaBanner.categoriaNombre }}</span>
          <span style="color: rgba(255,255,255,0.2)">·</span>
          <span>c/{{ proximaTomaBanner.frecuenciaHora }}h</span>
        </p>
      </div>

      <!-- Badge "próximamente" -->
      <div class="shrink-0 hidden sm:flex items-center gap-1.5 rounded-full px-3 py-2" style="background: rgba(255,255,255,0.1); backdrop-filter: blur(8px)">
        <span class="w-1.5 h-1.5 rounded-full bg-amber-400 animate-pulse"></span>
        <span class="text-[11px] font-semibold" style="color: rgba(255,255,255,0.8)">En espera</span>
      </div>
    </div>

    <div v-if="medicamentosActivos.length > 0"></div>
    <!-- ── Grid de tarjetas ────────────────────────────────────────── -->
    <div class="grid grid-cols-1 gap-4 md:grid-cols-2 xl:grid-cols-3">
      <div
        v-for="med in medicamentosConTomasApi"
        :key="med.id"
        class="group relative bg-white rounded-[20px] overflow-hidden flex flex-col transition-all duration-300 ease-out"
        :class="{ 'hover:-translate-y-1.5': !esBloqueada(med) }"
        :style="{
          border: '1px solid ' + (esBloqueada(med) ? '#e1e8f5' : (estadoConfig[med.estadoToma]?.border ?? '#e1e8f5')),
          boxShadow: '0 2px 16px rgba(13,27,62,0.07)',
          opacity: esBloqueada(med) ? 0.72 : 1
        }"
      >
        <!-- Barra superior degradada -->
        <div
          class="h-[3.5px] w-full shrink-0"
          :style="{ background: esBloqueada(med) ? 'linear-gradient(135deg, #b0b8cc, #c8d0de)' : (estadoConfig[med.estadoToma]?.gradient ?? '#8a97b4') }"
        ></div>

        <!-- Fondo tintado sutil -->
        <div
          class="absolute inset-0 top-[3.5px] pointer-events-none"
          :style="{ background: estadoConfig[med.estadoToma]?.bg ?? 'transparent', opacity: 0.025 }"
        ></div>

        <div class="relative px-5 pt-5 pb-5 flex flex-col gap-4">
          <!-- Fila superior: icono + nombre + badge -->
          <div class="flex items-start gap-3">

            <!-- Icono con glow al hacer hover -->
            <div
              class="w-11 h-11 rounded-[13px] flex items-center justify-center shrink-0 transition-all duration-300"
              :style="{
                background: esBloqueada(med) ? '#f5f7fc' : (estadoConfig[med.estadoToma]?.bg ?? '#f1f5f9'),
                boxShadow: '0 2px 8px ' + (esBloqueada(med) ? 'rgba(138,151,180,0.1)' : (estadoConfig[med.estadoToma]?.glow ?? 'transparent'))
              }"
            >
              <i
                class="pi text-[17px]"
                :class="esBloqueada(med) ? 'pi-lock' : (estadoConfig[med.estadoToma]?.icon ?? 'pi-circle')"
                :style="{ color: esBloqueada(med) ? '#8a97b4' : (estadoConfig[med.estadoToma]?.text ?? '#8a97b4') }"
              ></i>
            </div>

            <!-- Nombre y categoría -->
            <div class="flex-1 min-w-0 pt-0.5">
              <h3 class="text-[14px] font-bold text-[#0d1b3e] tracking-[-0.02em] truncate m-0 mb-0.5">
                {{ med.medicamentoNombre }}
              </h3>
              <p class="text-[11px] text-[#8a97b4] m-0 flex items-center gap-1">
                <span>{{ med.categoriaNombre }}</span>
                <span class="text-[#d8deed]">·</span>
                <span>c/{{ med.frecuenciaHora }}h</span>
              </p>
            </div>

            <!-- Badge de estado -->
            <span
              class="inline-flex items-center gap-1 text-[10px] font-bold px-2.5 py-1 rounded-full border shrink-0"
              :style="esBloqueada(med) ? { background: '#f5f7fc', color: '#8a97b4', borderColor: '#e1e8f5' } : {
                background: estadoConfig[med.estadoToma]?.bg,
                color: estadoConfig[med.estadoToma]?.text,
                borderColor: estadoConfig[med.estadoToma]?.border
              }"
            >
              <i class="pi text-[9px]" :class="esBloqueada(med) ? 'pi-lock' : estadoConfig[med.estadoToma]?.icon"></i>
              {{ esBloqueada(med) ? 'Próxima' : estadoConfig[med.estadoToma]?.label }}
            </span>
          </div>

          <!-- Separador -->
          <div class="border-t border-dashed border-[#edf0f8]"></div>

          <!-- Pie: hora + dosis -->
          <div class="flex items-center justify-between gap-2">
            <div class="flex items-center gap-1.5 bg-[#f5f7fc] rounded-[9px] px-3 py-2">
              <i class="pi pi-clock text-[10px] text-[#8a97b4]"></i>
              <span class="text-[12px] font-bold text-[#4a5a80] tabular-nums tracking-tight">
                {{ new Date(med.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
              </span>
            </div>
            <span
              class="text-[11px] font-extrabold px-3 py-2 rounded-[9px]"
              :style="{
                background: estadoConfig[med.estadoToma]?.bg,
                color: estadoConfig[med.estadoToma]?.text
              }"
            >
              {{ med.dosis }} mg
            </span>
          </div>

          <!-- Botón de acción (solo pendientes) -->
          <template v-if="med.estadoToma === EstadoToma.Pendiente">
            <!-- Toma disponible: botón activo -->
            <button
              v-if="!esBloqueada(med)"
              @click="abrirModal(med)"
              class="w-full flex items-center justify-center gap-2 py-2.5 rounded-[12px] text-[12px] font-bold text-white border-none cursor-pointer transition-all duration-200 hover:opacity-90 active:scale-[0.98]"
              :style="{ background: estadoConfig[med.estadoToma]?.gradient, boxShadow: '0 4px 14px ' + estadoConfig[med.estadoToma]?.glow }"
            >
              <i class="pi pi-pen-to-square text-[11px]"></i>
              Registrar toma
            </button>
            <!-- Toma bloqueada: indicador de espera -->
            <div
              v-else
              class="w-full flex items-center justify-center gap-2 py-2.5 rounded-[12px] text-[12px] font-semibold border text-[#8a97b4] bg-[#f5f7fc] border-[#e1e8f5] select-none"
            >
              <i class="pi pi-lock text-[11px]"></i>
              Disponible a las {{ new Date(med.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
            </div>
          </template>

          <!-- Confirmación (tomadas) -->
          <template v-else-if="med.estadoToma === EstadoToma.Tomada && med.fechaHoraConfirmacion">
            <p class="text-[10px] text-[#8a97b4] m-0 flex items-center gap-1">
              <i class="pi pi-check-circle text-emerald-400 text-[10px]"></i>
              Confirmado: {{ new Date(med.fechaHoraConfirmacion!).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
            </p>
          </template>
        </div>
      </div>
    </div>

    <!-- ── Estado vacío ────────────────────────────────────────────── -->
    <div
      v-if="!medicamentosConTomasApi.length"
      class="flex flex-col items-center justify-center gap-3 py-16 rounded-[20px] border border-dashed border-[#e1e8f5]"
    >
      <div class="w-14 h-14 rounded-full bg-[#eef4ff] flex items-center justify-center">
        <i class="pi pi-inbox text-[22px] text-[#3366ee]"></i>
      </div>
      <div class="text-center">
        <p class="text-[14px] font-bold text-[#0d1b3e] m-0 mb-0.5">Sin tomas programadas</p>
        <p class="text-[12px] text-[#8a97b4] m-0">No hay medicamentos programados para hoy</p>
      </div>
    </div>

    <!-- Modal -->
    <RegistrarTomaModal
      v-model:visible="modalVisible"
      :toma="tomaSeleccionada"
      @updated="onTomaActualizada"
    />
  </div>
</template>
