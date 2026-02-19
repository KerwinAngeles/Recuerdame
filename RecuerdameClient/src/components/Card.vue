
<script setup lang="ts">

defineProps({
    title: {
        type: String,
        required: true,
    },
    description: {
        type: String,
        required: true,
    },
    icon: {
        type: String,
        required: true,
    },
})
</script>
<template>
      <div
        class="group relative rounded-2xl border-2 border-dashed border-[#e1e8f5] min-h-[260px] flex flex-col items-center justify-center gap-3 cursor-pointer hover:border-[#3366ee] hover:-translate-y-1.5 transition-all duration-300 overflow-hidden"
      >
        <div
          class="absolute inset-0 bg-gradient-to-br from-[#3366ee]/5 to-[#10b981]/5 opacity-0 group-hover:opacity-100 transition-opacity duration-300 pointer-events-none"
        ></div>
        <div class="relative flex flex-col items-center gap-3">
          <div
            class="w-14 h-14 rounded-full border-2 border-dashed border-[#e1e8f5] flex items-center justify-center text-[#8a97b4] text-xl group-hover:border-[#3366ee] group-hover:bg-[#3366ee] group-hover:text-white group-hover:border-solid group-hover:shadow-lg group-hover:shadow-blue-500/25 group-hover:scale-110 transition-all duration-300"
          >
            <i :class="icon"></i>
          </div>
          <div class="text-center">
            <p
              class="text-[13px] font-bold text-[#8a97b4] group-hover:text-[#3366ee] tracking-tight transition-colors duration-200"
            >
              {{ title }}
            </p>
            <p
              class="text-[11px] text-[#cbd5e1] group-hover:text-[#8a97b4] mt-0.5 transition-colors duration-200"
            >
              {{ description }}
            </p>
          </div>
        </div>
      </div>
</template>
    