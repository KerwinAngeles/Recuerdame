<script setup lang="ts">
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'

const props = defineProps<{
  visible: boolean
  title?: string
  message?: string
  itemName?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'confirm'): void
}>()

const close = () => {
  emit('update:visible', false)
}

const confirm = () => {
  emit('confirm')
  close()
}
</script>

<template>
  <Dialog 
    :visible="visible" 
    @update:visible="$emit('update:visible', $event)" 
    modal 
    :style="{ width: '28rem' }"
    :closable="false"
    :pt="{
      root: { class: 'p-0 shadow-[0_16px_40px_rgba(13,27,62,0.12)] border border-[#e1e8f5] rounded-3xl bg-white overflow-hidden' },
      header: { class: 'hidden' },
      content: { class: 'p-6 bg-white text-center' }
    }"
  >
    <div class="flex flex-col items-center gap-4 pt-4">
      <div class="w-16 h-16 rounded-2xl flex items-center justify-center bg-[#fff1f2] text-[#e11d48] shadow-sm shrink-0 mb-2">
        <i class="pi pi-exclamation-triangle text-3xl"></i>
      </div>
      
      <div>
        <h3 class="text-[1.125rem] font-bold text-[#0d1b3e] mb-2 tracking-tight">
            {{ title || '¿Estás seguro?' }}
        </h3>
        <p class="text-[14px] text-[#8a97b4] leading-relaxed">
            {{ message || 'Esta acción no se puede deshacer.' }}
            <span v-if="itemName" class="block font-semibold mt-1 text-[#4a5878]">«{{ itemName }}»</span>
        </p>
      </div>

      <div class="flex gap-3 w-full mt-4 justify-center">
        <Button 
            label="Cancelar" 
            @click="close" 
            class="flex-1 max-w-[140px] px-4 py-[0.625rem] text-[13px] font-semibold text-[#4a5878] bg-white border border-[#e1e8f5] rounded-xl hover:bg-[#f1f5f9] transition-all tracking-wide" 
            unstyled 
        />
        <Button 
            label="Eliminar" 
            @click="confirm" 
            class="flex-1 max-w-[140px] px-4 py-[0.625rem] text-[13px] font-semibold text-white bg-gradient-to-r from-[#e11d48] to-[#be123c] border border-transparent rounded-xl shadow-md shadow-rose-500/25 hover:-translate-y-0.5 hover:shadow-lg transition-all tracking-wide" 
            unstyled 
        />
      </div>
    </div>
  </Dialog>
</template>
