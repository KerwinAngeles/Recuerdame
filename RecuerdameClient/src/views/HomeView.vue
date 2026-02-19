<script setup lang="ts">
import Chart from 'primevue/chart'
import { ref, onMounted } from 'vue'

const chartData = ref<any>(null)
const chartOptions = ref<any>(null)

onMounted(() => {
  chartData.value = {
    labels: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul'],
    datasets: [
      {
        label: 'Consultas',
        data: [65, 59, 80, 81, 56, 55, 72],
        fill: true,
        borderColor: '#3366ee',
        backgroundColor: 'rgba(51, 102, 238, 0.07)',
        tension: 0.45,
        pointBackgroundColor: '#3366ee',
        pointBorderColor: '#fff',
        pointBorderWidth: 2,
        pointRadius: 4,
        pointHoverRadius: 6,
      },
      {
        label: 'Recetas',
        data: [28, 48, 40, 19, 86, 27, 60],
        fill: true,
        borderColor: '#10b981',
        backgroundColor: 'rgba(16, 185, 129, 0.05)',
        tension: 0.45,
        pointBackgroundColor: '#10b981',
        pointBorderColor: '#fff',
        pointBorderWidth: 2,
        pointRadius: 4,
        pointHoverRadius: 6,
      },
    ],
  }
  chartOptions.value = {
    maintainAspectRatio: false,
    plugins: {
      legend: {
        labels: {
          color: '#4a5878',
          font: { family: 'Inter', size: 12, weight: '500' },
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
        grid: { color: 'rgba(13,27,62,0.05)', drawBorder: false },
        border: { display: false },
      },
      y: {
        ticks: { color: '#8a97b4', font: { family: 'Inter', size: 11 } },
        grid: { color: 'rgba(13,27,62,0.05)', drawBorder: false },
        border: { display: false },
      },
    },
  }
})

const stats = [
  {
    title: 'Medicamentos',
    value: '124',
    icon: 'pi pi-box',
    trend: '+12%',
    trendUp: true,
    iconBg: '#eef4ff',
    iconColor: '#3366ee',
    orbColor: 'rgba(51,102,238,0.1)',
    desc: 'Activos en el sistema',
  },
  {
    title: 'Alertas Activas',
    value: '3',
    icon: 'pi pi-exclamation-triangle',
    trend: '-2 hoy',
    trendUp: false,
    iconBg: '#fffbeb',
    iconColor: '#d97706',
    orbColor: 'rgba(245,158,11,0.1)',
    desc: 'Requieren atención',
  },
  {
    title: 'Recetas Hoy',
    value: '18',
    icon: 'pi pi-file-edit',
    trend: '+8%',
    trendUp: true,
    iconBg: '#ecfdf5',
    iconColor: '#059669',
    orbColor: 'rgba(16,185,129,0.1)',
    desc: 'Emitidas esta jornada',
  },
]

const recentActivity = [
  {
    icon: 'pi pi-plus-circle',
    text: 'Nuevo medicamento agregado',
    sub: 'Ibuprofeno 400mg',
    time: 'Hace 5 min',
    iconBg: '#eef4ff',
    iconColor: '#3366ee',
  },
  {
    icon: 'pi pi-bell',
    text: 'Alerta de stock bajo',
    sub: 'Amoxicilina 500mg',
    time: 'Hace 23 min',
    iconBg: '#fffbeb',
    iconColor: '#d97706',
  },
  {
    icon: 'pi pi-check-circle',
    text: 'Receta completada',
    sub: 'Paciente #1042',
    time: 'Hace 1h',
    iconBg: '#ecfdf5',
    iconColor: '#059669',
  },
  {
    icon: 'pi pi-user-plus',
    text: 'Nuevo paciente registrado',
    sub: 'María González',
    time: 'Hace 2h',
    iconBg: '#eef4ff',
    iconColor: '#3366ee',
  },
  {
    icon: 'pi pi-refresh',
    text: 'Tratamiento renovado',
    sub: 'Metformina 850mg',
    time: 'Hace 3h',
    iconBg: '#ecfdf5',
    iconColor: '#059669',
  },
]

const activeChip = ref('7D')
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
        <button class="inline-flex items-center gap-2 px-[1.125rem] py-2.5 border border-[#e1e8f5] rounded-[10px] bg-white text-[#4a5878] text-[13px] font-semibold cursor-pointer transition-all duration-200 tracking-[-0.01em] hover:border-[#8eb5ff] hover:text-[#1e4fd8] hover:bg-[#eef4ff]">
          <i class="pi pi-download"></i>
          Exportar
        </button>
        <button class="inline-flex items-center gap-2 px-[1.125rem] py-2.5 border-none rounded-[10px] bg-gradient-to-br from-[#3366ee] to-[#1e4fd8] text-white text-[13px] font-semibold cursor-pointer transition-all duration-200 shadow-[0_4px_14px_rgba(51,102,238,0.3)] tracking-[-0.01em] hover:-translate-y-px hover:shadow-[0_6px_20px_rgba(51,102,238,0.42)]">
          <i class="pi pi-plus"></i>
          Nueva Medicación
        </button>
      </div>
    </div>

    <!-- ── Stats Grid ───────────────────────────────────── -->
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
      <div
        v-for="(stat, i) in stats"
        :key="i"
        class="relative bg-white rounded-[18px] p-[1.375rem] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden transition-all duration-[250ms] hover:-translate-y-[3px] hover:shadow-[0_12px_32px_rgba(13,27,62,0.12)]"
      >
        <!-- Glow orb -->
        <div
          class="absolute -top-10 -right-10 w-[130px] h-[130px] rounded-full pointer-events-none"
          :style="{ background: `radial-gradient(circle, ${stat.orbColor} 0%, transparent 70%)` }"
        ></div>

        <!-- Top row -->
        <div class="relative flex items-start justify-between mb-[1.125rem]">
          <div
            class="w-11 h-11 rounded-[12px] flex items-center justify-center text-[18px]"
            :style="{ background: stat.iconBg, color: stat.iconColor }"
          >
            <i :class="stat.icon"></i>
          </div>
          <span
            class="inline-flex items-center gap-1 text-[11px] font-bold px-[0.625rem] py-1 rounded-full"
            :class="stat.trendUp ? 'bg-[#ecfdf5] text-[#059669]' : 'bg-[#fff7ed] text-[#d97706]'"
          >
            <i :class="stat.trendUp ? 'pi pi-arrow-up' : 'pi pi-arrow-down'" class="text-[9px]"></i>
            {{ stat.trend }}
          </span>
        </div>

        <!-- Body -->
        <div class="relative">
          <p class="text-[2.5rem] font-extrabold text-[#0d1b3e] mb-0.5 m-0 tracking-[-0.045em] leading-none">{{ stat.value }}</p>
          <p class="text-[13px] font-semibold text-[#4a5878] mb-1 m-0">{{ stat.title }}</p>
          <p class="text-[11px] text-[#8a97b4] m-0">{{ stat.desc }}</p>
        </div>
      </div>
    </div>

    <!-- ── Bottom Grid: Chart + Activity ───────────────── -->
    <div class="grid grid-cols-1 gap-4 lg:grid-cols-[1fr_340px]">

      <!-- Chart Card -->
      <div class="bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col">
        <div class="flex items-start justify-between px-[1.375rem] pt-[1.375rem] mb-5">
          <div>
            <h3 class="text-[15px] font-bold text-[#0d1b3e] mb-1 m-0 tracking-[-0.02em]">Actividad Semanal</h3>
            <p class="text-[12px] text-[#8a97b4] m-0">Consultas y recetas emitidas</p>
          </div>
          <div class="flex gap-1">
            <button
              v-for="period in ['7D', '30D', '90D']"
              :key="period"
              class="px-[0.625rem] py-1 rounded-[7px] border text-[11px] font-semibold cursor-pointer transition-all duration-[150ms] hover:border-[#8eb5ff] hover:text-[#1e4fd8]"
              :class="activeChip === period
                ? 'bg-[#eef4ff] border-[#bcd3ff] text-[#1e4fd8]'
                : 'border-[#e1e8f5] bg-transparent text-[#8a97b4]'"
              @click="activeChip = period"
            >
              {{ period }}
            </button>
          </div>
        </div>
        <div class="h-[260px] px-[1.375rem] pb-[1.375rem] flex-1">
          <Chart type="line" :data="chartData" :options="chartOptions" class="h-full" />
        </div>
      </div>

      <!-- Activity Feed -->
      <div class="bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col">
        <div class="flex items-start justify-between px-[1.375rem] pt-[1.375rem] mb-5">
          <div>
            <h3 class="text-[15px] font-bold text-[#0d1b3e] mb-1 m-0 tracking-[-0.02em]">Actividad Reciente</h3>
            <p class="text-[12px] text-[#8a97b4] m-0">Últimas acciones del sistema</p>
          </div>
          <button class="border-none bg-transparent text-[12px] font-semibold text-[#3366ee] cursor-pointer p-0 transition-colors duration-[150ms] whitespace-nowrap mt-0.5 hover:text-[#1a3fb5]">
            Ver todo
          </button>
        </div>

        <div class="flex-1 px-[1.375rem] pb-[1.375rem] flex flex-col overflow-y-auto">
          <div
            v-for="(item, i) in recentActivity"
            :key="i"
            class="flex items-center gap-[0.875rem] py-[0.875rem] border-b border-[#e1e8f5] transition-all duration-[150ms] last:border-b-0 hover:bg-[#eef4ff] hover:-mx-[1.375rem] hover:px-[1.375rem]"
          >
            <div
              class="w-9 h-9 rounded-[10px] flex items-center justify-center text-[14px] shrink-0"
              :style="{ background: item.iconBg, color: item.iconColor }"
            >
              <i :class="item.icon"></i>
            </div>
            <div class="flex-1 min-w-0">
              <p class="text-[12px] font-semibold text-[#0d1b3e] mb-0.5 m-0 truncate">{{ item.text }}</p>
              <p class="text-[11px] text-[#8a97b4] m-0">{{ item.sub }}</p>
            </div>
            <span class="text-[10px] text-[#8a97b4] whitespace-nowrap shrink-0">{{ item.time }}</span>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>
