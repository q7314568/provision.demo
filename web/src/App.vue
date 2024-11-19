<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
import RevenueView from './views/RevenueView.vue';

export default {
  name: 'App',
  components: {
    RevenueView
  }
};
</script>

<style scoped>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #55728f;
  margin-top: 0;
}
header {
  background-color: #42b983;
  padding: 20px;
  color: white;
}
main {
  padding: 20px;
}
</style>
