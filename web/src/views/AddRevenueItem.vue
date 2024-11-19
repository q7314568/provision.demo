<template>
  <div>
    <h1>新增每月營收項目</h1>
    <form @submit.prevent="addRevenueItem">
      <label>
        報告日期：
        <input type="text" v-model="reportDate" required />
      </label>
      <label>
        年月：
        <input type="text" v-model="dataYearMonth" required />
      </label>
      <label>
        公司代號：
        <input type="text" v-model="companyCode" required />
      </label>
      <label>
        公司名稱：
        <input type="text" v-model="companyName" required />
      </label>
      <label>
        產業別：
        <input type="text" v-model="industry" required />
      </label>
      <label>
        當月營收：
        <input type="number" v-model.number="currentRevenue" required />
      </label>
      <label>
        上月營收：
        <input type="number" v-model.number="lastMonthRevenue" required />
      </label>
      <label>
        去年同月營收：
        <input type="number" v-model.number="lastYearSameMonthRevenue" required />
      </label>
      <label>
        月營收變動百分比：
        <input type="number" v-model.number="monthlyChangePercent" />
      </label>
      <label>
        年營收變動百分比：
        <input type="number" v-model.number="yearlyChangePercent" />
      </label>
      <label>
        當月累計營收：
        <input type="number" v-model.number="cumulativeCurrentRevenue" required />
      </label>
      <label>
        去年累計營收：
        <input type="number" v-model.number="cumulativeLastYearRevenue" required />
      </label>
      <label>
        累計營收變動百分比：
        <input type="number" v-model.number="cumulativeChangePercent" />
      </label>
      <label>
        備註：
        <textarea v-model="remarks"></textarea>
      </label>
      <div style="display: flex; gap: 10px; justify-content: center;">
        <button type="button" @click="$router.go(-1)">回上一頁</button>
        <button type="submit">新增項目</button>
      </div>
    </form>
  </div>
</template>

<script>
import * as api from '../services/api';

export default {
  data() {
    return {
      reportDate: '',
      dataYearMonth: '',
      companyCode: '',
      companyName: '',
      industry: '',
      currentRevenue: 0,
      lastMonthRevenue: 0,
      lastYearSameMonthRevenue: 0,
      monthlyChangePercent: null,
      yearlyChangePercent: null,
      cumulativeCurrentRevenue: 0,
      cumulativeLastYearRevenue: 0,
      cumulativeChangePercent: null,
      remarks: ''
    };
  },
  methods: {
    async addRevenueItem() {
      try {
        const newItem = {
          reportDate: this.reportDate,
          dataYearMonth: this.dataYearMonth,
          companyCode: this.companyCode,
          companyName: this.companyName,
          industry: this.industry,
          currentRevenue: this.currentRevenue,
          lastMonthRevenue: this.lastMonthRevenue,
          lastYearSameMonthRevenue: this.lastYearSameMonthRevenue,
          monthlyChangePercent: this.monthlyChangePercent,
          yearlyChangePercent: this.yearlyChangePercent,
          cumulativeCurrentRevenue: this.cumulativeCurrentRevenue,
          cumulativeLastYearRevenue: this.cumulativeLastYearRevenue,
          cumulativeChangePercent: this.cumulativeChangePercent,
          remarks: this.remarks
        };
        await api.addMonthlyRevenue(newItem);
        this.$router.push({ name: 'RevenueView' });
        alert('新增成功！');
        this.resetForm();
      } catch (error) {
        console.error('There was a problem adding the revenue item:', error);
        alert('新增失敗，請重試。');
      }
    },
    resetForm() {
      this.reportDate = '';
      this.dataYearMonth = '';
      this.companyCode = '';
      this.companyName = '';
      this.industry = '';
      this.currentRevenue = 0;
      this.lastMonthRevenue = 0;
      this.lastYearSameMonthRevenue = 0;
      this.monthlyChangePercent = null;
      this.yearlyChangePercent = null;
      this.cumulativeCurrentRevenue = 0;
      this.cumulativeLastYearRevenue = 0;
      this.cumulativeChangePercent = null;
      this.remarks = '';
    }
  }
};
</script>

<style scoped>
h1 {
  font-size: 1.5em;
  margin-bottom: 20px;
}
form {
  display: flex;
  flex-direction: column;
}
label {
  margin-bottom: 10px;
}
input[type="text"], input[type="number"], textarea {
  padding: 5px;
  font-size: 1em;
  margin-top: 5px;
}
button {
  margin-top: 20px;
  padding: 10px;
  font-size: 1em;
  cursor: pointer;
}
</style>
