<template>
</template>

<script>
//import { IDENTITY_LOGIN } from '@/store/mutations.js';
import { IDENTITY_LOGIN } from '@/store/actions.js';

export default {
  name: 'PostLogin',
  created() {
    //this.$store.commit(IDENTITY_LOGIN);
    this.$store.dispatch(IDENTITY_LOGIN);
    this.$router.push({ name: 'home' });

  }
}
</script>

<style>

</style>
