<template>
  <div class="container-fluid">
    Witamy w Panelu administratora!
    <br/>
    <br/>
    <user-prop-edit />
  </div>
</template>

<script>
import UserPropEdit from './../components/UserPropEdit.vue'
export default {
  name: 'app',
  data() {
    return {
     
    }
  },
  components: {
    UserPropEdit
  }
}
</script>

<style lang="scss">
</style>