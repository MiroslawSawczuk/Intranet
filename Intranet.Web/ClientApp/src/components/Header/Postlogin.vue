<template>
</template>

<script>
import EventBus from '../../event-bus.js';

export default {
  name: 'Postlogin',
  created() {
    this.$http.get('/api/account/external-login-callback').then(response => {
      localStorage.token = response.body;
      EventBus.$emit('user-logged');
      this.$router.push('/');
    });
  }
}
</script>

<style>

</style>
