<template>
  <div class="container-fluid">
    <div v-if="isAuthenticated">
      Welcome to the User Panel!
      <br/>
      <br/>
      <user-prop-edit />
    </div>
    <div v-else>
      Sorry you must be logged to use User Panel!
    </div>
  </div>
</template>

<script>
import UserPropEdit from './../components/User/UserPropEdit.vue'
import { mapState } from 'vuex';

export default {
  name: 'app',
  components: {
    UserPropEdit
  },
  computed: {
    ...mapState({ 
      isAuthenticated: state => state.identity.isAuthenticated
    })
  }
}
</script>

<style lang="scss">
</style>