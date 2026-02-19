<script setup lang="ts">
import { ref } from 'vue';
import PrimeChart from 'primevue/chart';
import type { ChartData, ChartOptions } from '../type';

    defineProps<{
    chartData: ChartData;
    chartOptions: ChartOptions;
}>();

const activeChip = ref('7D');
</script>

<template>
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
      <PrimeChart type="line" :data="chartData" :options="chartOptions" class="h-full" />
    </div>
  </div>
</template>