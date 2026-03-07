<script setup lang="ts">
import { ref, onMounted } from 'vue'
import Stat from '../components/Stat.vue'
import Button from '../components/Button.vue'
import TomaProgramadaSection from '../components/TomaProgramadaSection.vue'
import type { MedicamentoConTomas } from '../data/tomasProgramadas.ts'
import { TomaProgramadaService } from '../services/tomaProgramadaService.ts'
import { MedicamentoService } from '../services/medicamentoService.ts'
import { EstadoToma } from '../enums/enums.ts'
import type { TomaProgramada } from '../types.ts'

const tomasPendientes = ref(0)
const tomasRealizadas = ref(0)
const tomasOmitidas = ref(0)
const cantidadDeMedicamentos = ref(0)
const dosisDeHoy = ref(0)
const proximaToma = ref<string>("--:--")
const medicamentosConTomasApi = ref<MedicamentoConTomas[]>([])

const COLORES = [
  { colorAccent: '#3366ee', colorBg: '#eef4ff', colorText: '#3366ee', icon: 'pi-heart-fill' },
  { colorAccent: '#10b981', colorBg: '#ecfdf5', colorText: '#059669', icon: 'pi-bolt' },
  { colorAccent: '#8b5cf6', colorBg: '#f5f3ff', colorText: '#7c3aed', icon: 'pi-heart' },
  { colorAccent: '#f59e0b', colorBg: '#fffbeb', colorText: '#d97706', icon: 'pi-star-fill' },
  { colorAccent: '#ef4444', colorBg: '#fff1f2', colorText: '#dc2626', icon: 'pi-exclamation-circle' },
]


const cargarTomasProgramadas = async () => {
  const service = TomaProgramadaService.getInstance()
  const medicamentoService = MedicamentoService.getInstance()
  const ahora = new Date()

  const [tomasResponse, medicamentos] = await Promise.all([
    service.getTomas(),
    medicamentoService.getMedicamentos(),
  ])
  const tomas = tomasResponse.items

  cantidadDeMedicamentos.value = medicamentos.length
  tomasRealizadas.value = tomas.filter(t => t.estadoToma.toString() === EstadoToma.Tomada).length
  tomasPendientes.value = tomas.filter(t => t.estadoToma.toString() === EstadoToma.Pendiente).length
  tomasOmitidas.value = tomas.filter(t => t.estadoToma.toString() === EstadoToma.Cancelada).length
  dosisDeHoy.value = tomas.filter(t => new Date(t.fechaHoraProgramada).toDateString() === ahora.toDateString()).length

  const siguiente = tomas
    .map(t => new Date(t.fechaHoraProgramada))
    .filter(f => f.toDateString() === ahora.toDateString() && f > ahora)
    .sort((a, b) => a.getTime() - b.getTime())[0]
  proximaToma.value = siguiente
    ? siguiente.toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' })
    : '--:--'
}

onMounted(() => {
  cargarTomasProgramadas()
})

</script>

<template>
  <div class="flex flex-col gap-6">

    <!-- ── Page Header ──────────────────────────────────── -->
    <div class="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
      <div>
        <p class="inline-flex items-center gap-1.5 text-[11px] font-bold uppercase tracking-[0.1em] text-[#8a97b4] mb-1.5 m-0">
          <i class="pi pi-sun" style="color: #f59e0b; font-size: 11px"></i>
          Buenos días
        </p>
        <h1 class="text-[clamp(1.5rem,3vw,1.875rem)] font-extrabold text-[#0d1b3e] mb-1 m-0 tracking-[-0.035em] leading-[1.1]">
          Panel de Control
        </h1>
        <p class="text-[13px] text-[#8a97b4] m-0">Resumen clínico del día — 18 de febrero, 2026</p>
      </div>
      <div class="flex gap-2.5 shrink-0">
        <Button label="Exportar" icon="pi pi-download" class="inline-flex items-center gap-2 px-[1.125rem] py-2.5 border border-[#e1e8f5] rounded-[10px] bg-gradient-to-br from-[#3366ee] to-[#1e4fd8] text-white text-[13px] font-semibold cursor-pointer transition-all duration-200 tracking-[-0.01em] hover:border-[#8eb5ff] hover:text-[#1e4fd8] hover:bg-[#eef4ff]"/>
      </div>
    </div>

    <!-- ── Stats Grid ───────────────────────────────────── -->
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
    <Stat title="Medicamentos"  :value="cantidadDeMedicamentos" trend="+12%" :trendUp="true" icon="pi pi-list" iconBg="#eef4ff" iconColor="#3366ee" orbColor="rgba(51,102,238,0.1)" desc="Activos en el sistema" />
    <Stat title="Dosis de hoy"  :value="dosisDeHoy" trend="-2 hoy" :trendUp="false" icon="pi pi-calendar" iconBg="#fff7ed" iconColor="#d97706" orbColor="rgba(245,158,11,0.1)" desc="Programadas para hoy" />
    <Stat title="Dosis tomadas" :value="tomasRealizadas" trend="+3 hoy" :trendUp="true" icon="pi pi-check-circle" iconBg="#ecfdf5" iconColor="#059669" orbColor="rgba(16,185,129,0.1)" desc="Completadas hoy" />
    <Stat title="Próxima Toma"  :value="proximaToma" trend="+8%" :trendUp="true" icon="pi pi-clock" iconBg="#e0f2fe" iconColor="#0891b2" orbColor="rgba(8,145,178,0.1)" desc="Próxima dosis del día" />
    </div>

    <!-- ── Tomas Programadas ─────────────────────────────── -->
    <TomaProgramadaSection 
      :medicamentos="medicamentosConTomasApi"
      :countTomadas="tomasRealizadas"
      :countPendientes="tomasPendientes"
      :countOmitidas="tomasOmitidas"
    />
  </div>
</template>
