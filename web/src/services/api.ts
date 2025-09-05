// TypeScript 版本的 api.js

import axios from 'axios';
interface ImportMetaEnv {
  VITE_API_BASE_URL: string;
}

const apiBaseUrl = (import.meta as ImportMeta & { env: ImportMetaEnv }).env.VITE_API_BASE_URL;

if (!apiBaseUrl) {
  throw new Error('VITE_API_BASE_URL is not defined in the environment variables.');
}

// 設定 axios 的 baseURL 為後端 API 伺服器的 URL
const api = axios.create({
  baseURL: apiBaseUrl, // 若有 https 請改為 https://localhost:7234
});



// 查詢營收摘要 (POST)
export const getRevenueSummary = async (query: any) => {
  return api.post('/MonthlyRevenue/query', query);
};



// 新增營收項目 (POST)
export const addRevenueItem = async (item: any) => {
  return api.post('/MonthlyRevenue/add', item);
};
