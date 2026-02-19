<script setup lang="ts">
import { ref } from 'vue'
import { RouterView, useRouter, useRoute } from 'vue-router'
import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import Navbar from './components/Navbar.vue'
import Profile from './components/Profile.vue'

const sidebarVisible = ref(false)
const route = useRoute()
const router = useRouter()

const items = ref([
  {
    label: 'Dashboard',
    icon: 'pi pi-home',
    to: '/',
    command: () => router.push('/'),
  },
  {
    label: 'Medicamentos',
    icon: 'pi pi-heart-fill',
    to: '/medicamentos',
    command: () => router.push('/medicamentos'),
  },
  {
    label: 'Categorías',
    icon: 'pi pi-list',
    to: '/categorias',
    command: () => router.push('/categorias'),
  },
])

const pageTitle = () => {
  const name = route.name ? String(route.name) : 'Dashboard'
  return name.charAt(0).toUpperCase() + name.slice(1)
}

const today = new Date().toLocaleDateString('es-ES', {
  weekday: 'long',
  day: 'numeric',
  month: 'long',
  year: 'numeric',
})
</script>

<template>
  <div class="flex h-screen overflow-hidden bg-[#f0f4fb]">

    <!-- ── Sidebar Desktop ──────────────────────────────── -->
    <aside
      class="hidden md:flex flex-col w-[272px] shrink-0 [background:linear-gradient(175deg,#0d1b3e_0%,#112254_55%,#0e1e47_100%)] border-r border-white/[0.05] z-20 relative overflow-hidden
             before:content-[''] before:absolute before:-top-20 before:-right-20 before:w-[220px] before:h-[220px] before:rounded-full before:[background:radial-gradient(circle,rgba(51,102,238,0.16)_0%,transparent_70%)] before:pointer-events-none
             after:content-[''] after:absolute after:bottom-[100px] after:-left-[60px] after:w-[180px] after:h-[180px] after:rounded-full after:[background:radial-gradient(circle,rgba(16,185,129,0.08)_0%,transparent_70%)] after:pointer-events-none"
    >
      <Navbar :items="items" />
      <Profile />
    </aside>

    <!-- ── Main Area ────────────────────────────────────── -->
    <div class="flex flex-col flex-1 min-w-0 h-full">

      <!-- ── Topbar ─────────────────────────────────────── -->
      <header class="h-16 flex items-center justify-between px-6 bg-white/[0.94] backdrop-blur-[20px] backdrop-saturate-[180%] border-b border-[#e1e8f5] shrink-0 sticky top-0 z-10 shadow-[0_1px_0_rgba(13,27,62,0.06),0_4px_16px_rgba(13,27,62,0.04)]">
        <div class="flex items-center gap-3.5">
          <!-- Mobile menu -->
          <button
            class="md:hidden relative flex items-center justify-center w-9 h-9 border-none rounded-[10px] bg-transparent text-[#8a97b4] cursor-pointer transition-all text-[14px] hover:bg-[#eef4ff] hover:text-[#1e4fd8]"
            @click="sidebarVisible = true"
            title="Menú"
          >
            <i class="pi pi-bars"></i>
          </button>

          <!-- Breadcrumb -->
          <div class="hidden sm:flex items-center gap-2">
            <span class="flex items-center gap-1.5 text-[11px] font-bold tracking-[0.06em] uppercase text-[#8a97b4]">
              <svg viewBox="0 0 14 14" fill="none" class="w-3 h-3 text-[#3366ee] shrink-0">
                <path d="M7 2v10M2 7h10" stroke="currentColor" stroke-width="2" stroke-linecap="round" />
              </svg>
              Recuérdame
            </span>
            <i class="pi pi-angle-right text-[11px] text-[#e1e8f5]"></i>
            <span class="text-[13px] font-bold text-[#0d1b3e] tracking-[-0.01em]">{{ pageTitle() }}</span>
          </div>
        </div>

        <!-- Right actions -->
        <div class="flex items-center gap-1.5">
          <!-- Date (desktop) -->
          <span class="hidden lg:block text-[11px] font-medium text-[#8a97b4] tracking-[-0.01em] whitespace-nowrap">{{ today }}</span>

          <div class="w-px h-6 bg-[#e1e8f5] mx-1"></div>

          <button class="relative flex items-center justify-center w-9 h-9 border-none rounded-[10px] bg-transparent text-[#8a97b4] cursor-pointer transition-all text-[14px] hover:bg-[#eef4ff] hover:text-[#1e4fd8]" title="Buscar">
            <i class="pi pi-search"></i>
          </button>

          <button class="relative flex items-center justify-center w-9 h-9 border-none rounded-[10px] bg-transparent text-[#8a97b4] cursor-pointer transition-all text-[14px] hover:bg-[#eef4ff] hover:text-[#1e4fd8]" title="Notificaciones">
            <i class="pi pi-bell"></i>
            <span class="absolute top-[7px] right-[7px] w-[7px] h-[7px] rounded-full bg-[#10b981] border-2 border-white pulse-dot"></span>
          </button>

          <div class="w-px h-6 bg-[#e1e8f5] mx-1"></div>

          <!-- User chip -->
          <div class="flex items-center gap-2 py-1 pl-1 pr-3 rounded-full border border-[#e1e8f5] cursor-pointer transition-all hover:border-[#8eb5ff] hover:bg-[#eef4ff]" title="Dr. Usuario">
            <div class="w-7 h-7 rounded-full bg-gradient-to-br from-[#3366ee] to-[#10b981] flex items-center justify-center text-white text-[11px] shrink-0">
              <i class="pi pi-user"></i>
            </div>
            <span class="hidden lg:block text-[12px] font-semibold text-[#4a5878] whitespace-nowrap">Dr. Usuario</span>
          </div>
        </div>
      </header>

      <!-- ── Content ─────────────────────────────────────── -->
      <main class="flex-1 overflow-y-auto p-7 bg-[#f0f4fb]">
        <div class="max-w-[1280px] mx-auto relative">
          <RouterView v-slot="{ Component }">
            <Transition name="fade">
              <component :is="Component" :key="route.fullPath" />
            </Transition>
          </RouterView>
        </div>
      </main>
    </div>

    <!-- ── Mobile Drawer ────────────────────────────────── -->
    <Teleport to="body">
      <Transition name="drawer">
        <div
          v-if="sidebarVisible"
          class="fixed inset-0 z-50 flex bg-[rgba(13,27,62,0.55)] backdrop-blur-[4px] md:hidden"
          @click.self="sidebarVisible = false"
        >
          <aside class="drawer-panel flex flex-col w-[280px] h-full [background:linear-gradient(175deg,#0d1b3e_0%,#112254_100%)] shadow-[4px_0_32px_rgba(13,27,62,0.4)]">

            <!-- Header -->
            <div class="flex items-center justify-between p-5 border-b border-white/[0.06]">
              <div class="flex items-center gap-3">
                <div class="w-9 h-9 rounded-[10px] bg-gradient-to-br from-[#3366ee] to-[#10b981] flex items-center justify-center shadow-[0_4px_12px_rgba(51,102,238,0.4)] shrink-0">
                  <svg viewBox="0 0 20 20" fill="currentColor" class="w-4 h-4 text-white">
                    <path d="M10 3a1 1 0 011 1v5h5a1 1 0 110 2h-5v5a1 1 0 11-2 0v-5H4a1 1 0 110-2h5V4a1 1 0 011-1z" />
                  </svg>
                </div>
                <div>
                  <h1 class="text-[15px] font-extrabold text-white m-0 tracking-[-0.025em]">Recuérdame</h1>
                  <p class="text-[9px] font-semibold text-[rgba(180,200,255,0.4)] uppercase tracking-[0.12em] mt-0.5 mb-0">Sistema Hospitalario</p>
                </div>
              </div>
              <button
                class="w-[30px] h-[30px] border-none rounded-[8px] bg-white/[0.06] text-[rgba(180,200,255,0.6)] cursor-pointer flex items-center justify-center text-[12px] transition-all hover:bg-white/[0.12] hover:text-white"
                @click="sidebarVisible = false"
              >
                <i class="pi pi-times"></i>
              </button>
            </div>

            <!-- Nav -->
            <nav class="flex-1 overflow-y-auto px-3 py-4">
              <p class="text-[9px] font-bold uppercase tracking-[0.14em] text-[rgba(180,200,255,0.28)] px-2.5 pb-2 mb-1 m-0">Principal</p>
              <RouterLink
                v-for="item in items"
                :key="item.label"
                :to="item.to"
                class="flex items-center gap-3 px-3 py-[0.625rem] rounded-[10px] no-underline text-[rgba(180,200,255,0.65)] text-[13px] font-medium transition-all duration-200 mb-0.5 border border-transparent hover:bg-[rgba(51,102,238,0.1)] hover:text-[rgba(200,220,255,0.9)] [&.router-link-exact-active]:!bg-[rgba(51,102,238,0.18)] [&.router-link-exact-active]:!text-[#93b4ff] [&.router-link-exact-active]:!border-[rgba(51,102,238,0.2)]"
                @click="sidebarVisible = false"
              >
                <div class="w-[30px] h-[30px] rounded-[8px] bg-white/[0.06] flex items-center justify-center text-[13px] shrink-0 transition-all [.router-link-exact-active_&]:bg-[rgba(51,102,238,0.22)]">
                  <i :class="item.icon"></i>
                </div>
                <span>{{ item.label }}</span>
              </RouterLink>
            </nav>

            <!-- User -->
            <div class="flex items-center gap-3 px-4 py-[0.875rem] border-t border-white/[0.06]">
              <div class="w-[34px] h-[34px] rounded-full bg-gradient-to-br from-[#3366ee] to-[#10b981] flex items-center justify-center text-white text-[13px] shrink-0">
                <i class="pi pi-user"></i>
              </div>
              <div>
                <p class="text-[13px] font-semibold text-white m-0">Dr. Usuario</p>
                <p class="text-[11px] text-[rgba(180,200,255,0.4)] mt-0.5 m-0">Administrador</p>
              </div>
            </div>
          </aside>
        </div>
      </Transition>
    </Teleport>

    <!-- Global -->
    <Toast position="top-right" />
    <ConfirmDialog />
  </div>
</template>

<style scoped>
/* ── Page Transitions ──────────────────────────────────────── */
.fade-enter-active {
  transition: opacity 0.18s ease, transform 0.18s ease;
}
.fade-leave-active {
  transition: opacity 0.12s ease;
  position: absolute;
  width: 100%;
}
.fade-enter-from {
  opacity: 0;
  transform: translateY(6px);
}
.fade-leave-to {
  opacity: 0;
}

/* ── Drawer Transitions ────────────────────────────────────── */
.drawer-enter-active,
.drawer-leave-active {
  transition: opacity 0.25s ease;
}
.drawer-enter-active .drawer-panel,
.drawer-leave-active .drawer-panel {
  transition: transform 0.28s cubic-bezier(0.4, 0, 0.2, 1);
}
.drawer-enter-from,
.drawer-leave-to {
  opacity: 0;
}
.drawer-enter-from .drawer-panel {
  transform: translateX(-100%);
}
.drawer-leave-to .drawer-panel {
  transform: translateX(-100%);
}
</style>
