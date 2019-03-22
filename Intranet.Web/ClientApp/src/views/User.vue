<template>
  <div class="container-fluid">
    <div v-if="isAuthenticated">
      Welcome to the User Panel!
      <br/>
      <br/>
      <user-prop-edit />
    </div>
    <div class="text-block" v-else>
      Sorry you must be logged to use User Panel!
    </div>
  </div>
</template>

<script>
import UserPropEdit from './../components/User/UserPropEdit.vue'
import { mapState } from 'vuex';

export default {
  name: 'User',
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
.text-block{
    font-size: 5rem;
    padding: 1em;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, -50%);
  }
</style>