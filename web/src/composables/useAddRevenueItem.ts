import { ref } from 'vue';
import { addRevenueItem as apiAddRevenueItem } from '../services/api';
import type { Router } from 'vue-router';

export function useAddRevenueItem(router?: Router) {
  const reportDate = ref('');
  const dataYearMonth = ref('');
  const companyCode = ref('');
  const companyName = ref('');
  const industry = ref('');
  const currentRevenue = ref<number | null>(null);
  const lastMonthRevenue = ref<number | null>(null);
  const lastYearSameMonthRevenue = ref<number | null>(null);
  const monthlyChangePercent = ref<number | null>(null);
  const yearlyChangePercent = ref<number | null>(null);
  const cumulativeCurrentRevenue = ref<number | null>(null);
  const cumulativeLastYearRevenue = ref<number | null>(null);
  const cumulativeChangePercent = ref<number | null>(null);
  const remarks = ref('');
  const loading = ref(false);
  const error = ref<string | null>(null);

  const addRevenueItem = async () => {
    loading.value = true;
    error.value = null;
    try {
      const item = {
        reportDate: reportDate.value,
        dataYearMonth: dataYearMonth.value,
        companyCode: companyCode.value,
        companyName: companyName.value,
        industry: industry.value,
        currentRevenue: currentRevenue.value,
        lastMonthRevenue: lastMonthRevenue.value,
        lastYearSameMonthRevenue: lastYearSameMonthRevenue.value,
        monthlyChangePercent: monthlyChangePercent.value,
        yearlyChangePercent: yearlyChangePercent.value,
        cumulativeCurrentRevenue: cumulativeCurrentRevenue.value,
        cumulativeLastYearRevenue: cumulativeLastYearRevenue.value,
        cumulativeChangePercent: cumulativeChangePercent.value,
        remarks: remarks.value
      };
      await apiAddRevenueItem(item);
      if (router) router.push({ name: 'RevenueView' });
      resetForm();
    } catch (err: any) {
      error.value = err.message || '發生未知錯誤';
    } finally {
      loading.value = false;
    }
  };

  const resetForm = () => {
    reportDate.value = '';
    dataYearMonth.value = '';
    companyCode.value = '';
    companyName.value = '';
    industry.value = '';
    currentRevenue.value = null;
    lastMonthRevenue.value = null;
    lastYearSameMonthRevenue.value = null;
    monthlyChangePercent.value = null;
    yearlyChangePercent.value = null;
    cumulativeCurrentRevenue.value = null;
    cumulativeLastYearRevenue.value = null;
    cumulativeChangePercent.value = null;
    remarks.value = '';
  };

  return {
    reportDate,
    dataYearMonth,
    companyCode,
    companyName,
    industry,
    currentRevenue,
    lastMonthRevenue,
    lastYearSameMonthRevenue,
    monthlyChangePercent,
    yearlyChangePercent,
    cumulativeCurrentRevenue,
    cumulativeLastYearRevenue,
    cumulativeChangePercent,
    remarks,
    addRevenueItem,
    resetForm,
    loading,
    error
  };
}
