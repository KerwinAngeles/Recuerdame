<script setup lang="ts">
import { type RecentActivity } from "../types";
defineProps<{ recentActivity: RecentActivity[] }>()
</script>

<template>
    <div class="flex items-start justify-between px-[1.375rem] pt-[1.375rem] mb-5">
          <div>
            <h3 class="text-[15px] font-bold text-[#0d1b3e] mb-1 m-0 tracking-[-0.02em]">Actividad Reciente</h3>
            <p class="text-[12px] text-[#8a97b4] m-0">Últimas tomas realizadas</p>
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
</template>