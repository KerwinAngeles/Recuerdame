import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MedicamentoView from '../views/MedicamentoView.vue'
import CategoriaView from '../views/CategoriaView.vue'
import HistorialView from '../views/HistorialView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: HomeView,
      meta: { title: 'Dashboard' },
    },
    {
      path: '/medicamentos',
      name: 'Medicamento',
      component: MedicamentoView,
      meta: { title: 'Medicamentos' },
    },
    {
      path: '/categorias',
      name: 'Categoria',
      component: CategoriaView,
      meta: { title: 'Categorías' },
    },
    {
      path: '/historial',
      name: 'Historial',
      component: HistorialView,
      meta: { title: 'Historial' },
    },
  ],
})

router.afterEach((to) => {
  const title = to.meta.title as string | undefined
  document.title = title ? `Recordatorio Medico - ${title}` : 'Recordatorio Medico'
})

export default router
