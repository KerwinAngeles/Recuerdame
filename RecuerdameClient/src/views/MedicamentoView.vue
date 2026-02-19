<script setup lang="ts">
import { ref } from 'vue'
import type { Product } from '@/type'
import ProductCard from '@/components/ProductCard.vue'

const products = ref<Product[]>([
  {
    id: '1000',
    nombre: 'Paracetamol 500mg',
    descripcion: 'Analgésico y antipirético de uso común para el alivio del dolor leve a moderado.',
    image: 'bamboo-watch.jpg',
    dosis: 500,
    frecuencia: '1 vez al día',
    fechaInicio: new Date(),
    fechaFin: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000),
    estado: true,
  },
])
</script>

<template>
  <div class="space-y-5">

    <!-- ── Page Header ─────────────────────────────────── -->
    <div
      class="flex flex-col md:flex-row md:items-end justify-between gap-4 bg-white border border-[#e1e8f5] rounded-2xl p-6 shadow-sm"
    >
      <div>
        <p class="inline-flex items-center gap-1.5 text-[10px] font-bold uppercase tracking-widest text-[#3366ee] mb-1.5">
          <i class="pi pi-heart-fill text-[9px]"></i>
          Gestión de Tratamientos
        </p>
        <h1 class="text-[1.75rem] font-extrabold text-[#0d1b3e] tracking-tight leading-tight mb-1">
          Medicamentos
        </h1>
        <p class="text-[13px] text-[#8a97b4]">
          Administra los medicamentos y tratamientos activos del sistema.
        </p>
      </div>
      <div class="flex gap-2.5 flex-shrink-0">
        <button
          class="inline-flex items-center gap-2 px-4 py-2.5 text-[13px] font-semibold text-[#4a5878] bg-white border border-[#e1e8f5] rounded-xl hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff] transition-all duration-200"
        >
          <i class="pi pi-filter text-xs"></i>
          Filtrar
        </button>
        <button
          class="inline-flex items-center gap-2 px-4 py-2.5 text-[13px] font-semibold text-white bg-gradient-to-r from-[#3366ee] to-[#1e4fd8] rounded-xl shadow-md shadow-blue-500/25 hover:-translate-y-0.5 hover:shadow-lg hover:shadow-blue-500/30 transition-all duration-200"
        >
          <i class="pi pi-plus text-xs"></i>
          Nuevo Medicamento
        </button>
      </div>
    </div>

    <!-- ── Summary Strip ─────────────────────────────────── -->
    <div
      class="inline-flex items-center gap-4 bg-white border border-[#e1e8f5] rounded-[14px] px-5 py-3.5 shadow-sm"
    >
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#0d1b3e] tracking-tight leading-none">
          {{ products.length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Total</span>
      </div>
      <div class="w-px h-7 bg-[#e1e8f5]"></div>
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#059669] tracking-tight leading-none">
          {{ products.filter((p) => p.estado).length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Activos</span>
      </div>
      <div class="w-px h-7 bg-[#e1e8f5]"></div>
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#94a3b8] tracking-tight leading-none">
          {{ products.filter((p) => !p.estado).length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Inactivos</span>
      </div>
    </div>

    <!-- ── Cards Grid ─────────────────────────────────────── -->
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">

      <ProductCard v-for="product in products" :key="product.id" :product="product" />

      <!-- Add new card -->
      <div
        class="group relative rounded-[18px] border-2 border-dashed border-[#e1e8f5] min-h-[280px] flex flex-col items-center justify-center gap-3 cursor-pointer hover:border-[#3366ee] hover:-translate-y-1 transition-all duration-300 overflow-hidden"
      >
        <!-- Hover glow -->
        <div
          class="absolute inset-0 bg-gradient-to-br from-[#3366ee]/5 to-[#10b981]/5 opacity-0 group-hover:opacity-100 transition-opacity duration-300 pointer-events-none"
        ></div>

        <div class="relative flex flex-col items-center gap-3 text-center">
          <div
            class="w-14 h-14 rounded-full border-2 border-dashed border-[#e1e8f5] flex items-center justify-center text-[#8a97b4] text-xl group-hover:border-[#3366ee] group-hover:bg-[#3366ee] group-hover:text-white group-hover:border-solid group-hover:shadow-lg group-hover:shadow-blue-500/30 group-hover:scale-110 transition-all duration-300"
          >
            <i class="pi pi-plus"></i>
          </div>
          <div>
            <p
              class="text-[13px] font-bold text-[#8a97b4] group-hover:text-[#3366ee] tracking-tight transition-colors duration-200"
            >
              Agregar Medicamento
            </p>
            <p
              class="text-[11px] text-[#cbd5e1] group-hover:text-[#8a97b4] mt-0.5 transition-colors duration-200"
            >
              Registra un nuevo tratamiento
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
