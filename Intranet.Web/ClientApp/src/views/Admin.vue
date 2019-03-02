<template>
<div class="container-fluid">
    <div v-if="isAuthenticated">
      Welcome to the Administrator Panel!
      <br/>
      <br/>
      <user-prop-edit />
    </div>
    <div v-else>
      Sorry you must be logged to use Administrator Panel!
    </div>
  </div>
</template>

<script>
import UserPropEdit from './../components/Admin/UserPropEdit.vue'
import IdentityService from '@/services/identity.service';

export default {
  name: 'app',
  data() {
    return {
     
    }
  },
  components: {
    UserPropEdit
  },
  computed: {
    isAuthenticated : IdentityService.isAuthenticated
  }
}
</script>

<style lang="scss">
</style>