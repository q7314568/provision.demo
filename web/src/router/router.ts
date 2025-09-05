import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';
import RevenueView from '../views/RevenueView.vue';
import AddRevenueItem from '../views/AddRevenueItem.vue';

const routes: Array<RouteRecordRaw> = [
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


// 補充 import.meta.env 型別
interface ImportMeta {
  env: {
    VITE_BASE_URL: string;
  };
}

const router = createRouter({
  history: createWebHistory((import.meta as any).env.VITE_BASE_URL),
  routes
});

export default router;
