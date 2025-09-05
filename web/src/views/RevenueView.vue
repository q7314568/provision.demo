<template>
  <div>
    <h1>每月營收總結</h1>
    <button @click="navigateToAddRevenueItem" style="margin-bottom: 20px;">新增營收項目</button>
    <div>
      <label>
        公司編號：
        <input type="text" v-model="filterCompany" />
      </label>
      <label>
        年月：
        <input type="text" v-model="filterYYmm" />
      </label>
    </div>
    <div v-if="loading">
      <p>資料加載中...</p>
    </div>
    <div v-else>
      <div v-if="totalRecords > 0">
        <p>總筆數：{{ totalRecords }}</p>
        <table>
          <thead>
            <tr>
              <th>報告日期</th>
              <th>公司代號</th>
              <th>公司名稱</th>
              <th>產業</th>
              <th>當月營收</th>
              <th>上月營收</th>
              <th>去年同月營收</th>
              <th>月營收增減(%)</th>
              <th>去年同月增減(%)</th>
              <th>當月累計營收</th>
              <th>去年累計營收</th>
              <th>累計增減(%)</th>
              <th>備註</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(record, index) in revenueData" :key="index">
              <td>{{ record.reportDate }}</td>
              <td>{{ record.companyCode }}</td>
              <td>{{ record.companyName }}</td>
              <td>{{ record.industry }}</td>
              <td>{{ record.currentRevenue }}</td>
              <td>{{ record.lastMonthRevenue }}</td>
              <td>{{ record.lastYearSameMonthRevenue }}</td>
              <td>{{ getFormattedChange(record.monthlyChangePercent) }}</td>
              <td>{{ getFormattedChange(record.yearlyChangePercent) }}</td>
              <td>{{ record.cumulativeCurrentRevenue }}</td>
              <td>{{ record.cumulativeLastYearRevenue }}</td>
              <td>{{ getFormattedChange(record.cumulativeChangePercent) }}</td>
              <td>{{ record.remarks }}</td>
            </tr>
          </tbody>
        </table>
        <div class="pagination">
          <button :disabled="currentPage === 1" @click="changePage(currentPage - 1)">上一頁</button>
          <span>第 {{ currentPage }} 頁，共 {{ totalPages }} 頁</span>
          <button :disabled="currentPage === totalPages" @click="changePage(currentPage + 1)">下一頁</button>
        </div>
      </div>
      <div v-else>
        <p>沒有找到符合條件的記錄...</p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">

import { onMounted } from 'vue';
import { useRevenueSummary } from '../composables/useRevenueSummary';
import { useRouter } from 'vue-router';

const router = useRouter();
const {
  revenueData,
  filterCompany,
  filterYYmm,
  totalRecords,
  loading,
  currentPage,
  pageSize,
  totalPages,
  fetchRevenueData,
  changePage,
  getFormattedChange
} = useRevenueSummary();

function navigateToAddRevenueItem() {
  router.push({ name: 'AddRevenueItem' });
}

onMounted(() => {
  fetchRevenueData();
});
</script>

<style scoped>
h1 {
  font-size: 1.5em;
  margin-bottom: 20px;
}
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}
th, td {
  padding: 12px;
  border: 1px solid #ddd;
  text-align: left;
}
th {
  background-color: #f4f4f4;
}
tr:nth-child(even) {
  background-color: #f9f9f9;
}
input[type="text"] {
  margin: 10px;
  padding: 5px;
  font-size: 1em;
}
.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}
button {
  margin: 0 10px;
  padding: 10px;
  font-size: 1em;
}
</style>
