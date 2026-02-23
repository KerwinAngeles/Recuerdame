<script setup lang="ts">
import { ref, onMounted } from 'vue'
import Stat from '../components/Stat.vue'
import Button from '../components/Button.vue'
import TomaProgramadaSection from '../components/TomaProgramadaSection.vue'
import { medicamentosConTomas } from '../data/tomasProgramadas.ts'
import { TomaProgramadaService } from '../services/tomaProgramadaService.ts'
import {MedicamentoService} from '../services/medicamentoService.ts'

const tomasPendientes = ref(0)
const tomasRealizadas = ref(0)
const tomasOmitidas = ref(0)
const cantidadDeMedicamentos = ref(0)
const dosisDeHoy = ref(0)
const proximaToma = ref<string>("6:00")

const cargarTomasProgramadas = async () => {
    const service = TomaProgramadaService.getInstance()
    const medicamentoService = MedicamentoService.getInstance()
    const [
      Pendientes,
      Realizadas,
      Omitidas,
      DosisDeHoy,
      ProximaToma,
      CantidadDeMedicamentos
    ] = await Promise.all([
      service.getCantidadDeTomasPendientes(),
      service.getCantidadDeTomasRealizadas(),
      service.getCantidadDeTomasOmitidas(),
      service.getDosisDeHoy(),
      service.getProximaToma(),
      medicamentoService.getMedicamentos().then(m => m.length)
    ])

    tomasPendientes.value = Pendientes
    tomasRealizadas.value = Realizadas
    tomasOmitidas.value = Omitidas
    dosisDeHoy.value = DosisDeHoy
    proximaToma.value = ProximaToma
    cantidadDeMedicamentos.value = CantidadDeMedicamentos
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
      :medicamentos="medicamentosConTomas" 
      :countTomadas="tomasRealizadas"
      :countPendientes="tomasPendientes"
      :countOmitidas="tomasOmitidas"
    />
  </div>
</template>
