<script setup lang="ts">
import type { Medicamento } from '@/types'

defineProps<{ product: Medicamento }>()
defineEmits<{
  (e: 'edit', product: Medicamento): void
  (e: 'delete', product: Medicamento): void
}>()
</script>

<template>
  <div v-if="product" class="group relative bg-white rounded-[18px] border border-[#e1e8f5] shadow-[0_2px_12px_rgba(13,27,62,0.08)] overflow-hidden flex flex-col transition-all duration-[250ms] ease-[cubic-bezier(0.4,0,0.2,1)] hover:-translate-y-1 hover:shadow-[0_16px_40px_rgba(13,27,62,0.12)] hover:border-[rgba(51,102,238,0.18)]">

    <!-- Accent bar -->
    <div class="h-[3px] w-full bg-gradient-to-r from-[#3366ee] to-[#10b981] shrink-0"></div>

    <!-- Header -->
    <div class="flex items-start gap-3 px-5 pt-5">
      <div class="w-11 h-11 rounded-[12px] flex items-center justify-center shrink-0 transition-all duration-[250ms] ease-[cubic-bezier(0.4,0,0.2,1)] bg-gradient-to-br from-[#eef4ff] to-[#e0ecff] text-[#3366ee] group-hover:from-[#3366ee] group-hover:to-[#1e4fd8] group-hover:text-white group-hover:shadow-[0_4px_14px_rgba(51,102,238,0.35)] group-hover:-rotate-6 group-hover:scale-[1.08]">
        <i class="pi pi-heart-fill text-lg"></i>
      </div>
      <div class="flex flex-col gap-1.5 pt-0.5">
        <span class="inline-flex items-center gap-[0.3rem] text-[10px] font-bold uppercase tracking-[0.06em] px-2 py-1 rounded-[6px] leading-none bg-[#fffbeb] text-[#b45309]">
          <i class="pi pi-clock text-[9px]"></i>
          {{ product.frecuencia }}
        </span>
        <span class="inline-flex items-center gap-[0.3rem] text-[10px] font-bold uppercase tracking-[0.06em] px-2 py-1 rounded-[6px] leading-none bg-[#eef4ff] text-[#3366ee]">
          <i class="pi pi-chart-bar text-[9px]"></i>
          {{ product.dosis }} mg
        </span>
      </div>
    </div>

    <!-- Body -->
    <div class="px-5 pt-3.5 flex-1">
      <h3
        class="text-[14px] font-bold text-[#0d1b3e] tracking-[-0.02em] mb-1.5 truncate"
        :title="product.nombre"
      >
        {{ product.nombre }}
      </h3>
      <p class="text-[12px] text-[#8a97b4] leading-relaxed line-clamp-2 min-h-[37px]">
        {{ product.descripcion }}
      </p>
    </div>

    <!-- Status -->
    <div class="px-5 pt-3">
      <span
        class="inline-flex items-center gap-[0.375rem] text-[10px] font-bold uppercase tracking-[0.08em] px-[0.625rem] py-[0.3rem] rounded-full"
        :class="product.estado ? 'bg-[#ecfdf5] text-[#059669]' : 'bg-[#f1f5f9] text-[#64748b]'"
      >
        <span
          class="w-[5px] h-[5px] rounded-full"
          :class="product.estado ? 'bg-[#10b981]' : 'bg-[#94a3b8]'"
        ></span>
        {{ product.estado ? 'Activo' : 'Inactivo' }}
      </span>
    </div>

    <!-- Date range -->
    <div class="mx-5 mt-3.5 pt-3.5 border-t border-dashed border-[#e1e8f5] flex items-center gap-2">
      <div class="flex flex-col flex-1">
        <span class="text-[9px] font-bold uppercase tracking-[0.1em] text-[#8a97b4]">Inicio</span>
        <span class="text-[11px] font-semibold text-[#4a5878] mt-0.5">
          {{
            new Date(product.fechaInicio).toLocaleDateString('es', {
              day: '2-digit',
              month: 'short',
              year: 'numeric',
            })
          }}
        </span>
      </div>
      <div class="flex items-center gap-1 text-[#e1e8f5] text-[9px] shrink-0">
        <div class="w-3 h-px bg-current"></div>
        <i class="pi pi-arrow-right"></i>
        <div class="w-3 h-px bg-current"></div>
      </div>
      <div class="flex flex-col flex-1 items-end">
        <span class="text-[9px] font-bold uppercase tracking-[0.1em] text-[#8a97b4]">Fin</span>
        <span class="text-[11px] font-semibold text-[#4a5878] mt-0.5">
          {{
            new Date(product.fechaFin).toLocaleDateString('es', {
              day: '2-digit',
              month: 'short',
              year: 'numeric',
            })
          }}
        </span>
      </div>
    </div>

    <!-- Actions -->
    <div class="flex gap-2 px-5 pb-5 pt-3.5">
      <button 
        @click="$emit('edit', product)"
        class="flex-1 flex items-center justify-center gap-[0.375rem] text-[11px] font-semibold px-3 py-2 rounded-[9px] border border-[#e1e8f5] bg-white text-[#4a5878] cursor-pointer transition-all duration-[180ms] tracking-[-0.005em] hover:bg-[#eef4ff] hover:border-[#bcd3ff] hover:text-[#3366ee]"
      >
        <i class="pi pi-pencil text-[10px]"></i>
        Editar
      </button>
      <button 
        @click="$emit('delete', product)"
        class="flex-1 flex items-center justify-center gap-[0.375rem] text-[11px] font-semibold px-3 py-2 rounded-[9px] border border-[#e1e8f5] bg-white text-[#8a97b4] cursor-pointer transition-all duration-[180ms] tracking-[-0.005em] hover:bg-[#fff1f2] hover:border-[#fecdd3] hover:text-[#e11d48]"
      >
        <i class="pi pi-trash text-[10px]"></i>
        Eliminar
      </button>
    </div>
  </div>
</template>
