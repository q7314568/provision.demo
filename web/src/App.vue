<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>


<script setup lang="ts">
// 無需註冊元件，router-view 會自動渲染
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
