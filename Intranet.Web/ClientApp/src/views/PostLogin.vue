<template>
</template>

<script>
import { AUTH_LOGIN } from '@/store/actions';

export default {
  name: 'PostLogin',
  created() {
    this.$store
      .dispatch(AUTH_LOGIN).then(() => this.$router.push({ name: 'home' }));
  }
}
</script>

<style>

</style>
