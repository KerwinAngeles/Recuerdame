<script setup lang="ts">
import { type Stat } from "../types";
defineProps<{ stat: Stat }>()
</script>

<template>
      <div
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
</template>
