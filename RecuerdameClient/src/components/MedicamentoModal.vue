<script setup lang="ts">
import { ref, watch, onMounted } from 'vue'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import Select from 'primevue/select'
import type { Medicamento, CategoriaMedicamento } from '@/types'
import { CategoryService } from '@/services/categoryService'

const props = defineProps<{
  visible: boolean
  medicamento: Partial<Medicamento> | null
  mode: 'add' | 'edit'
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'save', medicamento: Partial<Medicamento>): void
}>()

const formData = ref<Partial<Medicamento>>({
  nombre: '',
  descripcion: '',
  dosis: 0,
  frecuenciaHora: 0,
  fechaInicio: new Date(),
  fechaFinal: new Date(),
  estado: true,
  categoriaId: 0,
  nota: ''
})

const errors = ref<Record<string, string>>({})
const categorias = ref<CategoriaMedicamento[]>([])

onMounted(async () => {
  categorias.value = await CategoryService.getInstance().getCategories()
})

watch(() => props.visible, (newVal) => {
  if (newVal) {
    errors.value = {}
    if (props.mode === 'edit' && props.medicamento) {
      formData.value = { ...props.medicamento }
    } else {
      formData.value = {
        nombre: '',
        descripcion: '',
        dosis: 0,
        frecuenciaHora: 0,
        fechaInicio: new Date(),
        fechaFinal: new Date(),
        estado: true,
        categoriaId: 0,
        nota: ''
      }
    }
  }
})

const close = () => {
  emit('update:visible', false)
}

const save = () => {
  errors.value = {}
  let isValid = true

  if (!formData.value.nombre?.trim()) {
    errors.value.nombre = 'El nombre es obligatorio.'
    isValid = false
  }

  if (!formData.value.dosis || formData.value.dosis <= 0) {
    errors.value.dosis = 'La dosis debe ser mayor a 0.'
    isValid = false
  }

  if (!formData.value.frecuenciaHora || formData.value.frecuenciaHora <= 0) {
    errors.value.frecuenciaHora = 'La frecuencia es obligatoria.'
    isValid = false
  }

  if (!formData.value.fechaInicio) {
    errors.value.fechaInicio = 'La fecha de inicio es obligatoria.'
    isValid = false
  }

  if (!formData.value.fechaFinal) {
    errors.value.fechaFinal = 'La fecha de fin es obligatoria.'
    isValid = false
  } else if (formData.value.fechaInicio && new Date(formData.value.fechaFinal) < new Date(formData.value.fechaInicio)) {
    errors.value.fechaFinal = 'La fecha de fin no puede ser anterior a la de inicio.'
    isValid = false
  }

  if (isValid) {
    emit('save', formData.value)
    close()
  }
}
</script>

<template>
  <Dialog 
    :visible="visible" 
    @update:visible="$emit('update:visible', $event)" 
    modal 
    :header="mode === 'add' ? 'Nuevo Medicamento' : 'Editar Medicamento'"
    :style="{ width: '35rem' }"
    :pt="{
      root: { class: 'p-0 shadow-lg border border-[#e1e8f5] rounded-2xl bg-white overflow-hidden' },
      header: { class: 'bg-[#f8fafc] border-b border-[#e1e8f5] p-5 py-4' },
      title: { class: 'text-lg font-bold text-[#0d1b3e]' },
      content: { class: 'p-6 bg-white' },
      footer: { class: 'bg-[#f8fafc] border-t border-[#e1e8f5] p-4 flex justify-end gap-3' },
      closeButton: { class: 'hover:bg-[#e1e8f5] transition-colors rounded-full text-[#8a97b4] hover:text-[#0d1b3e]' }
    }"
  >
    <div class="flex flex-col gap-4">
      <!-- Nombre -->
      <div class="flex flex-col gap-2">
        <label class="text-[13px] font-semibold text-[#4a5878]">Nombre del medicamento <span class="text-rose-500">*</span></label>
        <InputText v-model="formData.nombre" placeholder="Ej. Paracetamol" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 transition-colors shadow-sm" :class="errors.nombre ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
        <span v-if="errors.nombre" class="text-rose-500 text-[11px] font-medium">{{ errors.nombre }}</span>
      </div>

      <!-- Descripción -->
      <div class="flex flex-col gap-2">
        <label class="text-[13px] font-semibold text-[#4a5878]">Descripción</label>
        <textarea v-model="formData.descripcion" rows="3" placeholder="Instrucciones especiales..." class="w-full text-sm border border-[#e1e8f5] focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none transition-colors shadow-sm resize-none"></textarea>
      </div>

        <div class="flex flex-col gap-2">
        <label class="text-[13px] font-semibold text-[#4a5878]">Categoria</label>
        <Select v-model="formData.categoriaId" :options="categorias" optionLabel="nombre" optionValue="id" placeholder="Seleccione una categoria" class="w-full text-sm border border-[#e1e8f5] focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none transition-colors shadow-sm resize-none"></Select>
      </div>

      <div class="grid grid-cols-2 gap-4">
        <!-- Dosis -->
        <div class="flex flex-col gap-2">
            <label class="text-[13px] font-semibold text-[#4a5878]">Dosis (mg) <span class="text-rose-500">*</span></label>
            <input type="number" v-model.number="formData.dosis" placeholder="Ej. 500" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none transition-colors shadow-sm" :class="errors.dosis ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
            <span v-if="errors.dosis" class="text-rose-500 text-[11px] font-medium">{{ errors.dosis }}</span>
        </div>

        <!-- Frecuencia -->
        <div class="flex flex-col gap-2">
            <label class="text-[13px] font-semibold text-[#4a5878]">Frecuencia <span class="text-rose-500">*</span></label>
            <input type="number" v-model.number="formData.frecuenciaHora" placeholder="Ej. Cada 8 horas" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 transition-colors shadow-sm" :class="errors.frecuenciaHora ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
            <span v-if="errors.frecuenciaHora" class="text-rose-500 text-[11px] font-medium">{{ errors.frecuenciaHora }}</span>
        </div>
      </div>

      <div class="grid grid-cols-2 gap-4">
        <!-- Fecha Inicio -->
        <div class="flex flex-col gap-2">
            <label class="text-[13px] font-semibold text-[#4a5878]">Fecha Inicio <span class="text-rose-500">*</span></label>
            <input type="date" :value="formData.fechaInicio ? new Date(formData.fechaInicio).toISOString().split('T')[0] : ''" @input="formData.fechaInicio = new Date(($event.target as HTMLInputElement).value)" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 focus:outline-none transition-colors shadow-sm" :class="errors.fechaInicio ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
            <span v-if="errors.fechaInicio" class="text-rose-500 text-[11px] font-medium">{{ errors.fechaInicio }}</span>
        </div>

        <!-- Fecha Fin -->
        <div class="flex flex-col gap-2">
            <label class="text-[13px] font-semibold text-[#4a5878]">Fecha Fin <span class="text-rose-500">*</span></label>
            <input type="date" :value="formData.fechaFinal ? new Date(formData.fechaFinal).toISOString().split('T')[0] : ''" @input="formData.fechaFinal = new Date(($event.target as HTMLInputElement).value)" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none focus:outline-none transition-colors shadow-sm" :class="errors.fechaFinal ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
            <span v-if="errors.fechaFinal" class="text-rose-500 text-[11px] font-medium">{{ errors.fechaFinal }}</span>
        </div>
      </div>

      <div class="flex items-center gap-3 mt-2" v-if="mode === 'edit'">
        <input type="checkbox" id="estado" v-model="formData.estado" class="w-4 h-4 rounded text-[#3366ee] focus:ring-[#3366ee] border-[#e1e8f5]" />
        <label for="estado" class="text-[14px] font-medium text-[#0d1b3e] cursor-pointer">Tratamiento Activo</label>
      </div>
        <!-- Descripción -->
      <div class="flex flex-col gap-2">
        <label class="text-[13px] font-semibold text-[#4a5878]">Notas</label>
        <textarea v-model="formData.nota" rows="3" placeholder="Instrucciones especiales..." class="w-full text-sm border border-[#e1e8f5] focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none transition-colors shadow-sm resize-none"></textarea>
      </div>

    </div>

    <template #footer>
      <div class="flex gap-2 w-full justify-end">
          <Button 
            label="Cancelar" 
            @click="close" 
            class="px-4 py-2.5 text-[13px] font-semibold text-[#4a5878] bg-white border border-[#e1e8f5] rounded-xl hover:bg-[#f1f5f9] transition-all" 
            unstyled 
          />
          <Button 
            :label="mode === 'add' ? 'Registrar' : 'Guardar Cambios'" 
            @click="save" 
            class="px-4 py-2.5 text-[13px] font-semibold text-white bg-[#3366ee] border border-transparent rounded-xl hover:bg-[#1e4fd8] shadow-sm transition-all" 
            unstyled 
          />
      </div>
    </template>
  </Dialog>
</template>
