<script setup lang="ts">
import { ref, watch } from 'vue'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
import type { CategoriaMedicamento } from '@/types'

const props = defineProps<{
  visible: boolean
  categoria: Partial<CategoriaMedicamento> | null
  mode: 'add' | 'edit'
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'save', categoria: Partial<CategoriaMedicamento>): void
}>()

const formData = ref<Partial<CategoriaMedicamento>>({
  nombre: '',
  descripcion: '',
  estado: true
})

const errors = ref<Record<string, string>>({})

watch(() => props.visible, (newVal) => {
  if (newVal) {
    errors.value = {}
    if (props.mode === 'edit' && props.categoria) {
      formData.value = { ...props.categoria }
    } else {
      formData.value = {
        nombre: '',
        descripcion: '',
        estado: true
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
    errors.value.nombre = 'El nombre de la categoría es obligatorio.'
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
    :header="mode === 'add' ? 'Nueva Categoría' : 'Editar Categoría'"
    :style="{ width: '30rem' }"
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
        <label class="text-[13px] font-semibold text-[#4a5878]">Nombre de la categoría <span class="text-rose-500">*</span></label>
        <InputText v-model="formData.nombre" placeholder="Ej. Analgésicos" class="w-full text-sm border focus:border-[#3366ee] rounded-xl px-3 py-2.5 transition-colors shadow-sm" :class="errors.nombre ? 'border-rose-500 focus:border-rose-500' : 'border-[#e1e8f5]'" />
        <span v-if="errors.nombre" class="text-rose-500 text-[11px] font-medium">{{ errors.nombre }}</span>
      </div>

      <!-- Descripción -->
      <div class="flex flex-col gap-2">
        <label class="text-[13px] font-semibold text-[#4a5878]">Descripción</label>
        <textarea v-model="formData.descripcion" rows="3" placeholder="Ingresa una breve descripción detallando la familia terapéutica..." class="w-full text-sm border border-[#e1e8f5] focus:border-[#3366ee] rounded-xl px-3 py-2.5 outline-none transition-colors shadow-sm resize-none"></textarea>
      </div>

      <div class="flex items-center gap-3 mt-2" v-if="mode === 'edit'">
        <input 
          type="checkbox" 
          id="status" 
          :checked="formData.estado === true"
          @change="formData.estado = ($event.target as HTMLInputElement).checked ? true : false" 
          class="w-4 h-4 rounded text-[#3366ee] focus:ring-[#3366ee] border-[#e1e8f5]" 
        />
        <label for="status" class="text-[14px] font-medium text-[#0d1b3e] cursor-pointer">Categoría Activa</label>
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
