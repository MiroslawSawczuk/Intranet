<template>
</template>

<script>
import { AUTH_LOGIN } from './../store/mutations.js';

export default {
  name: 'PostLogin',
  created() {
    this.$store.commit(AUTH_LOGIN)
    this.$router.push({ name: 'home' });
  }
}
</script>

<style>

</style>
