<script setup lang="ts">
// User profile displayed at the bottom of the sidebar
</script>

<template>
  <div class="px-3 py-3 border-t border-white/[0.05] shrink-0">

    <!-- Status bar -->
    <div class="flex items-center gap-2 px-1 pb-2.5">
      <span class="w-1.5 h-1.5 rounded-full bg-[#10b981] shrink-0 pulse-dot"></span>
      <span class="text-[10px] font-semibold text-[rgba(180,200,255,0.32)] uppercase tracking-[0.1em]">
        Sistema activo
      </span>
    </div>

    <!-- User card -->
    <div class="flex items-center gap-3 p-2.5 rounded-[12px] cursor-pointer transition-all duration-200 border border-transparent hover:bg-[rgba(51,102,238,0.1)] hover:border-[rgba(51,102,238,0.12)]">

      <!-- Avatar -->
      <div class="relative w-[34px] h-[34px] rounded-full bg-gradient-to-br from-[#3366ee] to-[#10b981] flex items-center justify-center text-white shrink-0 shadow-[0_2px_8px_rgba(51,102,238,0.35)]">
        <i class="pi pi-user text-[13px]"></i>
        <span class="absolute bottom-[1px] right-[1px] w-2 h-2 rounded-full bg-[#10b981] border-2 border-[#0d1b3e]"></span>
      </div>

      <!-- Info -->
      <div class="min-w-0 flex-1">
        <p class="text-[13px] font-semibold text-white m-0 leading-none tracking-[-0.01em] truncate">Dr. Usuario</p>
        <p class="text-[10px] text-[rgba(180,200,255,0.38)] mt-[3px] mb-0 truncate tracking-[0.02em]">Administrador</p>
      </div>

      <!-- Menu -->
      <button class="w-[26px] h-[26px] border-none rounded-[7px] bg-white/[0.05] text-[rgba(180,200,255,0.38)] cursor-pointer flex items-center justify-center transition-all duration-200 shrink-0 hover:bg-white/[0.1] hover:text-[rgba(180,200,255,0.8)]" title="Opciones">
        <i class="pi pi-ellipsis-h text-[11px]"></i>
      </button>
    </div>
  </div>
</template>
