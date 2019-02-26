<template>
</template>

<script>

export default {
  name: 'PostLogin',
  created() {
    this.$store.commit('auth_login')
    this.$router.push({ name: 'home' });
  }
}
</script>

<style>

</style>
