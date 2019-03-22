<template>
</template>

<script>
import { IDENTITY_LOGIN } from '@/store/actions.js';

export default {
  name: 'PostLogin',
  created() {
    this.$store.dispatch(IDENTITY_LOGIN).then(()=> { 

      setTimeout(() => {
        this.$router.push({ name: 'home' });
      },1000)
    });
  }
}
</script>

<style lang="scss">

</style>
