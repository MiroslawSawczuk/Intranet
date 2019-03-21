<template>
  <div>
    Home Page for logged users
  </div>
</template>

<script>
import { mapState } from 'vuex';
import { IDENTITY_AUTHENTICATION } from '@/store/actions.js';

export default {
  name: 'HomePage',

 created() {
    this.$store.dispatch(IDENTITY_AUTHENTICATION);
  }
}

</script>

<style scoped>

</style>
