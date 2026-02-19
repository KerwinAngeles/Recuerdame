<script setup lang="ts">
import { ref } from 'vue'
import Card from '../components/Card.vue'
const categories = ref([
  {
    id: 1,
    code: 'CAT-001',
    name: 'Analgésicos',
    description: 'Medicamentos para aliviar el dolor leve a moderado.',
    count: 15,
    status: 'ACTIVE',
  },
  {
    id: 2,
    code: 'CAT-002',
    name: 'Antibióticos',
    description: 'Agentes antimicrobianos para tratar infecciones bacterianas.',
    count: 22,
    status: 'ACTIVE',
  },
  {
    id: 3,
    code: 'CAT-003',
    name: 'Antiinflamatorios',
    description: 'Reducen la inflamación y alivian el dolor muscular y articular.',
    count: 10,
    status: 'INACTIVE',
  },
])
</script>

<template>
  <div class="space-y-5">

    <!-- ── Header ─────────────────────────────────────────── -->
    <div
      class="flex flex-col md:flex-row md:items-end justify-between gap-4 bg-white border border-[#e1e8f5] rounded-2xl p-6 shadow-sm"
    >
      <div>
        <p class="inline-flex items-center gap-1.5 text-[10px] font-bold uppercase tracking-widest text-[#3366ee] mb-1.5">
          <i class="pi pi-list text-[9px]"></i>
          Panel de Administración
        </p>
        <h1 class="text-[1.75rem] font-extrabold text-[#0d1b3e] tracking-tight leading-tight mb-1">
          Categorías Terapéuticas
        </h1>
        <p class="text-[13px] text-[#8a97b4]">
          Clasificación y organización de medicamentos por familia terapéutica.
        </p>
      </div>
      <button
        class="self-start md:self-center inline-flex items-center gap-2 px-4 py-2.5 text-[13px] font-semibold text-white bg-gradient-to-r from-[#3366ee] to-[#1e4fd8] rounded-xl shadow-md shadow-blue-500/25 hover:-translate-y-0.5 hover:shadow-lg hover:shadow-blue-500/30 transition-all duration-200"
      >
        <i class="pi pi-plus text-xs"></i>
        Nueva Categoría
      </button>
    </div>

    <!-- ── Grid ───────────────────────────────────────────── -->
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">

      <div
        v-for="category in categories"
        :key="category.id"
        class="group relative bg-white rounded-2xl border border-[#e1e8f5] shadow-sm hover:shadow-xl hover:shadow-[#0d1b3e]/10 hover:-translate-y-1.5 transition-all duration-300 overflow-hidden flex flex-col"
      >
        <!-- Accent bar -->
        <div
          class="h-[3px] w-full flex-shrink-0 transition-all duration-300"
          :class="
            category.status === 'ACTIVE'
              ? 'bg-gradient-to-r from-[#3366ee] to-[#10b981]'
              : 'bg-[#e1e8f5]'
          "
        />

        <!-- Content -->
        <div class="p-5 flex-1 flex flex-col">

          <!-- Icon + Status -->
          <div class="flex items-start justify-between mb-4">
            <div
              class="w-11 h-11 rounded-xl flex items-center justify-center flex-shrink-0 transition-all duration-300"
              :class="
                category.status === 'ACTIVE'
                  ? 'bg-[#eef4ff] group-hover:scale-110 group-hover:-rotate-6'
                  : 'bg-[#f1f5f9] group-hover:scale-110'
              "
            >
              <i
                class="pi pi-bookmark text-lg transition-colors"
                :class="
                  category.status === 'ACTIVE'
                    ? 'text-[#3366ee]'
                    : 'text-[#94a3b8]'
                "
              ></i>
            </div>

            <span
              class="inline-flex items-center gap-1.5 text-[10px] font-bold uppercase tracking-widest px-2.5 py-1 rounded-full leading-none"
              :class="
                category.status === 'ACTIVE'
                  ? 'bg-[#ecfdf5] text-[#059669]'
                  : 'bg-[#f1f5f9] text-[#64748b]'
              "
            >
              <span
                class="w-1.5 h-1.5 rounded-full"
                :class="category.status === 'ACTIVE' ? 'bg-[#10b981]' : 'bg-[#94a3b8]'"
              ></span>
              {{ category.status === 'ACTIVE' ? 'Activa' : 'Inactiva' }}
            </span>
          </div>

          <!-- Code -->
          <span class="text-[10px] font-mono font-semibold tracking-widest text-[#8a97b4] uppercase mb-1">
            {{ category.code }}
          </span>

          <!-- Name -->
          <h3 class="text-[15px] font-bold text-[#0d1b3e] tracking-tight mb-2 line-clamp-1">
            {{ category.name }}
          </h3>

          <!-- Description -->
          <p class="text-[13px] text-[#8a97b4] line-clamp-2 leading-relaxed flex-1 min-h-[40px]">
            {{ category.description }}
          </p>

          <!-- Footer stat -->
          <div
            class="flex items-center gap-2 pt-3.5 mt-3.5 border-t border-dashed border-[#e1e8f5]"
          >
            <div
              class="w-7 h-7 rounded-lg flex items-center justify-center flex-shrink-0"
              :class="category.status === 'ACTIVE' ? 'bg-[#eef4ff]' : 'bg-[#f1f5f9]'"
            >
              <i
                class="pi pi-box text-[11px]"
                :class="category.status === 'ACTIVE' ? 'text-[#3366ee]' : 'text-[#94a3b8]'"
              ></i>
            </div>
            <span class="text-[12px] font-semibold text-[#4a5878]">
              {{ category.count }} Medicamentos
            </span>
          </div>
        </div>

        <!-- Slide-up action overlay -->
        <div
          class="absolute inset-x-0 bottom-0 p-3 bg-white/95 backdrop-blur-sm translate-y-full group-hover:translate-y-0 transition-transform duration-300 flex gap-2 border-t border-[#e1e8f5]"
        >
          <Button
            label="Editar"
            icon="pi pi-pencil"
            class="flex-1 flex items-center justify-center gap-1.5 text-[11px] font-semibold px-3 py-2.5 rounded-xl border border-[#e1e8f5] bg-white text-[#4a5878] hover:bg-[#eef4ff] hover:border-[#bcd3ff] hover:text-[#3366ee] transition-all duration-200"
          />

          <Button
            label="Eliminar"
            icon="pi pi-trash"
            class="flex-1 flex items-center justify-center gap-1.5 text-[11px] font-semibold px-3 py-2.5 rounded-xl border border-[#e1e8f5] bg-white text-[#8a97b4] hover:bg-[#fff1f2] hover:border-[#fecdd3] hover:text-[#e11d48] transition-all duration-200"
          />
        </div>
      </div>

      <!-- Add New Card -->
      <Card title="Nueva Categoría" description="Agregar clasificación" icon="pi pi-plus"/> 
    </div>
  </div>
</template>
