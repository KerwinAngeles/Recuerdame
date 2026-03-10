<script setup lang="ts">
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
import { ref } from 'vue'
import type { TomaProgramada } from '@/types'
import { EstadoToma } from '@/enums/enums'
import { TomaProgramadaService } from '@/services/tomaProgramadaService'

const props = defineProps<{
  visible: boolean
  toma: TomaProgramada | null
}>()

const emit = defineEmits<{
  (e: 'update:visible', value: boolean): void
  (e: 'updated'): void
}>()

const loading = ref(false)

const close = () => emit('update:visible', false)

const registrar = async (estado: EstadoToma) => {
  if (!props.toma) return
  loading.value = true
  try {
    const service = TomaProgramadaService.getInstance()
    await service.updateToma(props.toma.id, {
      medicamentoId: props.toma.medicamentoId,
      fechaHora: new Date(props.toma.fechaHoraProgramada),
      estadoToma: estado,
    })
    emit('updated')
    close()
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <Dialog
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
    modal
    :style="{ width: '26rem' }"
    :closable="!loading"
    :pt="{
      root: { class: 'p-0 shadow-[0_16px_40px_rgba(13,27,62,0.14)] border border-[#e1e8f5] rounded-3xl bg-white overflow-hidden' },
      header: { class: 'hidden' },
      content: { class: 'p-0 bg-white' }
    }"
  >
    <div v-if="toma" class="flex flex-col">
      <!-- Header -->
      <div class="bg-gradient-to-br from-[#0d1b3e] to-[#1e4fd8] px-6 pt-6 pb-5 relative overflow-hidden">
        <div class="absolute -right-8 -top-8 w-36 h-36 rounded-full bg-white/5 pointer-events-none"></div>
        <div class="relative">
          <p class="text-[10px] font-bold uppercase tracking-[0.12em] text-white/50 m-0 mb-1">Registrar Toma</p>
          <h3 class="text-[17px] font-extrabold text-white m-0 tracking-[-0.02em] leading-tight">
            {{ toma.medicamentoNombre }}
          </h3>
          <p class="text-[12px] text-white/60 m-0 mt-1">{{ toma.categoriaNombre }}</p>
        </div>
      </div>

      <!-- Info pills -->
      <div class="flex gap-3 px-6 py-4 border-b border-[#f0f4fc]">
        <div class="flex items-center gap-1.5 bg-[#eef4ff] rounded-lg px-3 py-1.5">
          <i class="pi pi-clock text-[11px] text-[#3366ee]"></i>
          <span class="text-[12px] font-semibold text-[#3366ee]">
            {{ new Date(toma.fechaHoraProgramada).toLocaleTimeString('es-DO', { hour: '2-digit', minute: '2-digit' }) }}
          </span>
        </div>
        <div class="flex items-center gap-1.5 bg-[#f5f3ff] rounded-lg px-3 py-1.5">
          <i class="pi pi-box text-[11px] text-[#7c3aed]"></i>
          <span class="text-[12px] font-semibold text-[#7c3aed]">{{ toma.dosis }} mg</span>
        </div>
        <div class="flex items-center gap-1.5 bg-[#f0fdf4] rounded-lg px-3 py-1.5">
          <i class="pi pi-refresh text-[11px] text-[#059669]"></i>
          <span class="text-[12px] font-semibold text-[#059669]">Cada {{ toma.frecuenciaHora }}h</span>
        </div>
      </div>

      <!-- Actions -->
      <div class="px-6 py-5 flex flex-col gap-3">
        <p class="text-[13px] text-[#8a97b4] m-0 mb-1">¿Cómo deseas registrar esta toma?</p>

        <button
          :disabled="loading"
          @click="registrar(EstadoToma.Tomada)"
          class="w-full flex items-center gap-3 px-4 py-3.5 rounded-[14px] bg-gradient-to-r from-[#059669] to-[#10b981] text-white font-semibold text-[14px] border-none cursor-pointer transition-all duration-200 hover:-translate-y-0.5 hover:shadow-lg hover:shadow-emerald-500/25 disabled:opacity-60 disabled:cursor-not-allowed disabled:transform-none"
        >
          <span class="w-8 h-8 rounded-[10px] bg-white/20 flex items-center justify-center shrink-0">
            <i class="pi pi-check text-white text-sm"></i>
          </span>
          <span>Marcar como Tomada</span>
          <i v-if="loading" class="pi pi-spin pi-spinner ml-auto"></i>
        </button>

        <button
          :disabled="loading"
          @click="registrar(EstadoToma.Cancelada)"
          class="w-full flex items-center gap-3 px-4 py-3.5 rounded-[14px] bg-[#fff1f2] text-[#e11d48] font-semibold text-[14px] border border-[#fecdd3] cursor-pointer transition-all duration-200 hover:bg-[#ffe4e6] hover:border-[#fda4af] disabled:opacity-60 disabled:cursor-not-allowed"
        >
          <span class="w-8 h-8 rounded-[10px] bg-[#fecdd3] flex items-center justify-center shrink-0">
            <i class="pi pi-times text-[#e11d48] text-sm"></i>
          </span>
          <span>Cancelar Toma</span>
        </button>

        <button
          :disabled="loading"
          @click="close"
          class="w-full py-2.5 text-[13px] font-semibold text-[#8a97b4] bg-transparent border-none cursor-pointer hover:text-[#4a5878] transition-colors disabled:opacity-60 disabled:cursor-not-allowed"
        >
          Cerrar
        </button>
      </div>
    </div>
  </Dialog>
</template>
