<template>
  <div>
    <div v-if="!firstName" class="dropdown">
      <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Sign In 
      </button>
      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <a class="dropdown-item" href="/api/account/sign-in">Microsoft</a>
      </div>
    </div>

    <div v-else>
      <span class="navbar-text">
        Hello {{ firstName }}!
      </span>
      &nbsp;
      <button  v-on:click="logout()" class="btn btn-secondary" type="button" >
       Log out
      </button>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import { IDENTITY_FETCH, IDENTITY_LOGOUT } from '@/store/actions.js';

export default {
  name: 'SignIn',
  methods:{
    logout() {
      this.$store.dispatch(IDENTITY_LOGOUT);
    }
  },
  created() {
    if (localStorage.token) {
      this.$store.dispatch(IDENTITY_FETCH);
    }
  },
  computed: {
    ...mapState({ 
      firstName: state => state.identity.firstName ? state.identity.firstName : state.identity.email
    })
  }
}
</script>

<style>

</style>
