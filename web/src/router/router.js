import { createRouter, createWebHistory } from 'vue-router';
import RevenueView from '../views/RevenueView.vue';
import AddRevenueItem from '../views/AddRevenueItem.vue';

const routes = [
  {
    path: '/',
    name: 'RevenueView',
    component: RevenueView
  },
  {
    path: '/add-revenue',
    name: 'AddRevenueItem',
    component: AddRevenueItem
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.VITE_BASE_URL),
  routes
});

export default router;
