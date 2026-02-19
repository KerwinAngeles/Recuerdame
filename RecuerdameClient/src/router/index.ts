import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import MedicamentoView from '../views/MedicamentoView.vue'
import CategoriaView from '../views/CategoriaView.vue'

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
  ],
})

export default router
