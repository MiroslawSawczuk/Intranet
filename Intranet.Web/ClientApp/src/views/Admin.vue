<template>
  <div class="container-fluid">
    Welcome to the Administrator Panel!
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