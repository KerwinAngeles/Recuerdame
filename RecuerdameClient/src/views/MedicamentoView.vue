<script setup lang="ts">
import { ref } from 'vue'
import ProductCard from '@/components/ProductCard.vue'
import Card from '@/components/Card.vue'
import MedicamentoModal from '@/components/MedicamentoModal.vue'
import ConfirmDeleteModal from '@/components/ConfirmDeleteModal.vue'
import { medicamentos } from '@/data/medicamento'
import type { Medicamento, MedicamentoRequest } from '@/types'
import { MedicamentoService } from '@/services/medicamentoService'

const showModal = ref(false)
const showDeleteModal = ref(false)
const modalMode = ref<'add' | 'edit'>('add')
const selectedMedicamento = ref<Partial<Medicamento> | null>(null)

const openAddModal = () => {
  modalMode.value = 'add'
  selectedMedicamento.value = null
  showModal.value = true
}

const openEditModal = (medicamento: Medicamento) => {
  modalMode.value = 'edit'
  selectedMedicamento.value = { ...medicamento }
  showModal.value = true
}

const openDeleteModal = (medicamento: Medicamento) => {
  selectedMedicamento.value = medicamento
  showDeleteModal.value = true
}

const isMedicamentoRequest = (data: Partial<MedicamentoRequest>): data is MedicamentoRequest => {
  return !!data.id && !!data.nombre && !!data.descripcion
}

const handleSave = (data: Partial<MedicamentoRequest>) => {
  if (modalMode.value === 'add') {
    const newItem = {...data } as MedicamentoRequest
    MedicamentoService.getInstance().createMedicamento(newItem);
  } else {
    if(isMedicamentoRequest(data)){
      const index = medicamentos.value.findIndex(m => m.id === data.id)
      if (index !== -1) {
        if(data.id){
          MedicamentoService.getInstance().updateMedicamento(data.id, data)
        } 
      }
    }
  }
}

const handleDeleteConfirm = () => {
  if (selectedMedicamento.value?.id) {
    const index = medicamentos.value.findIndex(m => m.id === selectedMedicamento.value?.id)
    if (index !== -1) {
      medicamentos.value.splice(index, 1)
    }
  }
}
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
        <Button
          label="Filtrar"
          icon="pi pi-filter"
          class="inline-flex items-center gap-2 px-4 py-2.5 text-[13px] font-semibold text-[#4a5878] bg-white border border-[#e1e8f5] rounded-xl hover:border-[#bcd3ff] hover:text-[#3366ee] hover:bg-[#eef4ff] transition-all duration-200"
        />
        <Button
          label="Nuevo Medicamento"
          icon="pi pi-plus"
          @click="openAddModal"
          class="inline-flex items-center gap-2 px-4 py-2.5 text-[13px] font-semibold text-white bg-gradient-to-r from-[#3366ee] to-[#1e4fd8] rounded-xl shadow-md shadow-blue-500/25 hover:-translate-y-0.5 hover:shadow-lg hover:shadow-blue-500/30 transition-all duration-200"
        />
      </div>
    </div>

    <!-- ── Summary Strip ─────────────────────────────────── -->
    <div
      class="inline-flex items-center gap-4 bg-white border border-[#e1e8f5] rounded-[14px] px-5 py-3.5 shadow-sm"
    >
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#0d1b3e] tracking-tight leading-none">
          {{ medicamentos.length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Total</span>
      </div>
      <div class="w-px h-7 bg-[#e1e8f5]"></div>
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#059669] tracking-tight leading-none">
          {{ medicamentos.filter((p) => p.estado).length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Activos</span>
      </div>
      <div class="w-px h-7 bg-[#e1e8f5]"></div>
      <div class="flex items-baseline gap-2">
        <span class="text-[22px] font-extrabold text-[#94a3b8] tracking-tight leading-none">
          {{ medicamentos.filter((p) => !p.estado).length }}
        </span>
        <span class="text-[11px] font-semibold text-[#8a97b4] uppercase tracking-wide">Inactivos</span>
      </div>
    </div>

    <!-- ── Cards Grid ─────────────────────────────────────── -->
    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
      <ProductCard 
        v-for="medicamento in medicamentos" 
        :key="medicamento.id" 
        :product="medicamento" 
        @edit="openEditModal"
        @delete="openDeleteModal"
      />
      <div @click="openAddModal" class="cursor-pointer h-full">
        <Card title="Agregar Medicamento" description="Registra un nuevo tratamiento" icon="pi pi-plus" class="h-full border-dashed" />
      </div>
    </div>

    <!-- Modals -->
    <MedicamentoModal
      v-model:visible="showModal"
      :medicamento="selectedMedicamento"
      :mode="modalMode"
      @save="handleSave"
    />

    <ConfirmDeleteModal
      v-model:visible="showDeleteModal"
      title="Eliminar Medicamento"
      message="¿Estás seguro de que quieres eliminar este medicamento?"
      :item-name="selectedMedicamento?.nombre"
      @confirm="handleDeleteConfirm"
    />
  </div>
</template>
