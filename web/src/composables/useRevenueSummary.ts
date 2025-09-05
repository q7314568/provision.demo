// 對應後端 PagedResult 型別
interface RevenueSummaryPagedResult {
  items: RevenueRecord[];
  pageNumber: number;
  pageSize: number;
  totalRecords: number;
}

import { ref, computed, watch } from 'vue';
import debounce from 'lodash/debounce';
import { getRevenueSummary } from '../services/api';

export interface RevenueRecord {
  reportDate: string;
  dataYearMonth: string;
  companyCode: string;
  companyName: string;
  industry: string;
  currentRevenue: number;
  lastMonthRevenue: number;
  lastYearSameMonthRevenue: number;
  monthlyChangePercent?: number;
  yearlyChangePercent?: number;
  cumulativeCurrentRevenue: number;
  cumulativeLastYearRevenue: number;
  cumulativeChangePercent?: number;
  remarks?: string;
}

export function useRevenueSummary() {
  const revenueData = ref<RevenueRecord[]>([]);
  const filterCompany = ref('');
  const filterYYmm = ref('');
  const totalRecords = ref(0);
  const loading = ref(false);
  const error = ref<string | null>(null);
  const currentPage = ref(1);
  const pageSize = ref(10);
  const totalPages = computed(() => Math.ceil(totalRecords.value / pageSize.value) || 1);

  const fetchRevenueData = async () => {
    loading.value = true;
    error.value = null;
    try {
      const response = await getRevenueSummary({
        companyCode: filterCompany.value || undefined,
        dataYearMonth: filterYYmm.value || undefined,
        pageNumber: currentPage.value,
        pageSize: pageSize.value
      });
      const resp = response.data as RevenueSummaryPagedResult;
      revenueData.value = Array.isArray(resp.items) ? resp.items : [];
      totalRecords.value = resp.totalRecords || 0;
      if (typeof resp.pageNumber === 'number') currentPage.value = resp.pageNumber;
      if (typeof resp.pageSize === 'number') pageSize.value = resp.pageSize;
    } catch (err: any) {
      error.value = err.message || '發生未知錯誤';
    } finally {
      loading.value = false;
    }
  };

  // debounce 包裝
  const debouncedFetch = debounce(fetchRevenueData, 400);

  // 監聽 input 與分頁變動，自動查詢
  watch([filterCompany, filterYYmm], () => {
    currentPage.value = 1; // 查詢條件變動時重設頁碼
    debouncedFetch();
  });
  watch(currentPage, () => {
    debouncedFetch();
  });

  const changePage = (page: number) => {
    if (page >= 1 && page <= totalPages.value && page !== currentPage.value) {
      currentPage.value = page;
    }
  };

  const getFormattedChange = (val?: number) => {
    return val !== undefined && val !== null ? val.toFixed(2) + '%' : '-';
  };

  // 分頁顯示
  // 由後端分頁，直接顯示 revenueData
  const pagedRevenueData = computed(() => revenueData.value);

  return {
    revenueData: pagedRevenueData,
    filterCompany,
    filterYYmm,
    totalRecords,
    loading,
    currentPage,
    pageSize,
    totalPages,
    fetchRevenueData,
    changePage,
    getFormattedChange,
    error
  };
}
