<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { ChartOptions } from 'chart.js'
import Stat from '../components/Stat.vue'
import { stats } from '../data/stat.ts'
import { recentActivities } from '../data/recentActivity.ts'
import RecentActivity from '../components/RecentActivity.vue'
import Button from '../components/Button.vue'
import { Bar } from 'vue-chartjs'
import { Chart as ChartJS, Title, Tooltip, Legend, CategoryScale, LinearScale, PointElement, LineElement, Filler, BarElement } from 'chart.js'
import { chartData } from '@/data/chart.ts'
import TomaProgramadaSection from '../components/TomaProgramadaSection.vue'
import { medicamentosConTomas } from '../data/tomasProgramadas.ts'

const chartOptions = ref<ChartOptions | null>(null)
  ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Filler,  
  Title,
  Tooltip,
  Legend,
  BarElement
)

onMounted(() => {
  chartOptions.value = {
    maintainAspectRatio: false,
    plugins: {
      legend: {
        labels: {
          color: '#4a5878',
          font: { family: 'Inter', size: 12, weight: 500 },
          usePointStyle: true,
          pointStyleWidth: 8,
        },
      },
      tooltip: {
        backgroundColor: '#0d1b3e',
        titleColor: '#fff',
        bodyColor: 'rgba(180,200,255,0.8)',
        borderColor: 'rgba(51,102,238,0.3)',
        borderWidth: 1,
        padding: 10,
        cornerRadius: 10,
      },
    },
    scales: {
      x: {
        ticks: { color: '#8a97b4', font: { family: 'Inter', size: 11 } },
        grid: { color: 'rgba(13,27,62,0.05)' },
        border: { display: false },
      },
      y: {
        ticks: { color: '#8a97b4', font: { family: 'Inter', size: 11 } },
        grid: { color: 'rgba(13,27,62,0.05)' },
        border: { display: false },
      },
    },
  }
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
        <Button label="Exportar" icon="pi pi-download" class="inline-flex items-center gap-2 px-[1.125rem] py-2.5 border border-[#e1e8f5] rounded-[10px] bg-white text-[#4a5878] text-[13px] font-semibold cursor-pointer transition-all duration-200 tracking-[-0.01em] hover:border-[#8eb5ff] hover:text-[#1e4fd8] hover:bg-[#eef4ff]"/>
        <Button label="Nueva Medicación" icon="pi pi-plus" class="inline-flex items-center gap-2 px-[1.125rem] py-2.5 border-none rounded-[10px] bg-gradient-to-br from-[#3366ee] to-[#1e4fd8] text-white text-[13px] font-semibold cursor-pointer transition-all duration-200 shadow-[0_4px_14px_rgba(51,102,238,0.3)] tracking-[-0.01em] hover:-translate-y-px hover:shadow-[0_6px_20px_rgba(51,102,238,0.42)]"/>
      </div>
    </div>

    <!-- ── Stats Grid ───────────────────────────────────── -->
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-4">
    <Stat v-for="stat in stats" :key="stat.title" :stat="stat" />
    </div>

    <!-- ── Bottom Grid: Chart + Activity ───────────────── -->
    <div class="grid grid-cols-1 gap-4 lg:grid-cols-[1fr_340px]">

      <div class="bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col">
        <Bar :data="chartData"/>
      </div>

      <!-- Activity Feed -->
      <div class="bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col">
        <RecentActivity :recentActivity="recentActivities" />
      </div>
    </div>

    <!-- ── Tomas Programadas ─────────────────────────────── -->
    <TomaProgramadaSection :medicamentos="medicamentosConTomas" />

  </div>
</template>
