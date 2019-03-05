<template>
  <div class="container-fluid">
     <Header />
     <router-view></router-view>
  </div>
</template>

<script>
import bootstrap from 'bootstrap/dist/js/bootstrap.js';
import Header from './components/Header.vue';
import Admin from './views/Admin.vue';

export default {
  name: 'app',
  components: {
    Header,
    Admin
  }
}
</script>

<style lang="scss">
  @import 'bootstrap/scss/bootstrap.scss';
</style>
