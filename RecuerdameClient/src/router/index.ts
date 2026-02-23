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
    },
    {
      path: '/medicamentos',
      name: 'Medicamento',
      component: MedicamentoView,
    },
    {
      path: '/categorias',
      name: 'Categoria',
      component: CategoriaView,
    },
    {
      path: '/historial',
      name: 'Historial',
      component: HistorialView,
    },
  ],
})

export default router
