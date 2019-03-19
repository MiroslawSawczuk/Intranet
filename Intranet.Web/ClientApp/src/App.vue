<template>
  <div class="container-fluid">
     <Header />
     <router-view></router-view>
  </div>
</template>

<script>
import bootstrap from 'bootstrap/dist/js/bootstrap.js';
import Header from './components/Header.vue';
import User from './views/User.vue';
import FirstConfiguration from './views/FirstConfiguration.vue';

export default {
  name: 'app',
  components: {
    Header,
    User,
    FirstConfiguration
  }
}
</script>

<style lang="scss">
  @import 'bootstrap/scss/bootstrap.scss';
</style>
