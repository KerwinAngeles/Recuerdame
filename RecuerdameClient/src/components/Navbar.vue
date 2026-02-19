<script setup lang="ts">
interface NavItem {
  label: string
  to: string
  icon: string
}

defineProps<{
  items: NavItem[]
}>()
</script>

<template>
  <!-- ── Brand ─────────────────────────────────────────────── -->
  <div class="px-5 pt-6 pb-4 shrink-0">
    <div class="flex items-center gap-3 mb-5">
      <!-- Medical cross icon -->
      <div class="w-[38px] h-[38px] rounded-[11px] bg-gradient-to-br from-[#3366ee] to-[#10b981] flex items-center justify-center shadow-[0_4px_14px_rgba(51,102,238,0.45)] shrink-0">
        <svg viewBox="0 0 20 20" fill="currentColor" class="w-[18px] h-[18px] text-white">
          <path d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
        </svg>
      </div>
      <div>
        <h1 class="text-[15px] font-extrabold text-white m-0 tracking-[-0.025em] leading-none">Recuérdame</h1>
        <p class="text-[9px] font-semibold text-[rgba(180,200,255,0.4)] uppercase tracking-[0.14em] mt-[3px] mb-0">Sistema Hospitalario</p>
      </div>
    </div>
    <div class="h-px bg-gradient-to-r from-[rgba(51,102,238,0.45)] to-transparent"></div>
  </div>

  <!-- ── Navigation ───────────────────────────────────────── -->
  <nav class="flex-1 overflow-y-auto px-3 py-2">

    <p class="text-[9px] font-bold uppercase tracking-[0.14em] text-[rgba(180,200,255,0.28)] px-2.5 pt-2.5 pb-1.5 m-0">Principal</p>

    <RouterLink
      v-for="item in items"
      :key="item.label"
      :to="item.to"
      class="flex items-center gap-3 py-[0.5625rem] px-3 rounded-[10px] no-underline text-[rgba(180,200,255,0.6)] text-[13px] font-medium tracking-[-0.01em] transition-all duration-200 mb-[1px] border border-transparent bg-transparent w-full hover:bg-[rgba(51,102,238,0.1)] hover:text-[rgba(210,225,255,0.9)] hover:border-[rgba(51,102,238,0.12)] [&.router-link-exact-active]:bg-[rgba(51,102,238,0.18)] [&.router-link-exact-active]:!text-[#93b4ff] [&.router-link-exact-active]:border-[rgba(51,102,238,0.22)]"
    >
      <span class="w-[30px] h-[30px] rounded-[8px] bg-white/[0.05] flex items-center justify-center text-[13px] shrink-0 transition-all duration-200 [.router-link-exact-active_&]:bg-[rgba(51,102,238,0.3)] [.router-link-exact-active_&]:!text-[#93b4ff]">
        <i :class="item.icon"></i>
      </span>
      <span class="flex-1">{{ item.label }}</span>
      <span class="w-[5px] h-[5px] rounded-full bg-[#3366ee] opacity-0 transition-opacity duration-200 shrink-0 [.router-link-exact-active_&]:opacity-100"></span>
    </RouterLink>

    <!-- Separator -->
    <div class="h-px bg-white/[0.05] mx-2.5 my-2"></div>

    <p class="text-[9px] font-bold uppercase tracking-[0.14em] text-[rgba(180,200,255,0.28)] px-2.5 pt-2.5 pb-1.5 m-0">Sistema</p>
  </nav>
</template>
