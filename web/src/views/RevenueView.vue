<template>
  <div>
    <h1>每月營收總結</h1>
    <button @click="navigateToAddRevenueItem" style="margin-bottom: 20px;">新增營收項目</button>
    <div v-if="loading">
      <p>資料加載中...</p>
    </div>
    <div v-else>
      <div>
        <label>
          公司編號：
          <input type="text" v-model="filterCompany" @input="fetchRevenueData" />
        </label>
        <label>
          年月：
          <input type="text" v-model="filterYYmm" @input="fetchRevenueData" />
        </label>
      </div>
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

<script>
import * as api from '../services/api';

export default {
  data() {
    return {
      revenueData: [],
      filterCompany: '',
      filterYYmm: '',
      totalRecords: 0, // 用於存儲總筆數
      loading: true,
      currentPage: 1,
      pageSize: 10, // 每頁顯示的筆數
      totalPages: 0 // 總頁數
    };
  },
  mounted() {
    this.fetchRevenueData();
  },
  methods: {
    async fetchRevenueData() {
      try {
        const response = await api.getMonthlyRevenueSummary({
          dataYearMonth: this.filterYYmm,
          companyCode: this.filterCompany,
          pageNumber: this.currentPage,
          pageSize: this.pageSize
        });
        this.revenueData = response.items; // 更新為根據後端回傳的 PagedResult 結構
        this.totalRecords = response.totalRecords; // 獨立的 state 來存儲總筆數
        this.totalPages = Math.ceil(this.totalRecords / this.pageSize); // 計算總頁數
        this.loading = false;
      } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        this.loading = false;
      }
    },
    changePage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
        this.fetchRevenueData();
      }
    },
    getFormattedChange(changePercent) {
      return changePercent != null ? changePercent.toFixed(2) : 'N/A';
    },
    navigateToAddRevenueItem() {
      this.$router.push({ name: 'AddRevenueItem' });
    }
  }
};
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
