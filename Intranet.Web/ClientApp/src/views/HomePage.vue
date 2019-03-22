<template>
  <div class="container-fluid">
    <Header />
     
    Home Page for logged users
  </div>
</template>

<script>
import Header from '@/components/Header.vue';
import { mapState } from 'vuex';
import { IDENTITY_AUTHENTICATION } from '@/store/actions.js';

export default {
  name: 'HomePage',
   components: {
    Header,
  },
 mounted() {
    this.$store.dispatch(IDENTITY_AUTHENTICATION);
  }
}

</script>

<style lang="scss">

</style>

