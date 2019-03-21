<template>
</template>

<script>
import { IDENTITY_LOGIN } from '@/store/actions.js';

export default {
  name: 'PostLogin',
  created() {
    this.$store.dispatch(IDENTITY_LOGIN).then(()=> { 
      this.$router.push({ name: 'home' });
    });
  }
}
</script>

<style lang="scss">

</style>
