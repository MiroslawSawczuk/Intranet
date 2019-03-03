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
import { mapState, mapGetters } from 'vuex';
import { IDENTITY_FETCH, IDENTITY_LOGOUT } from '@/store/mutations.js';

export default {
  name: 'SignIn',
  methods:{
    logout() {
      this.$store.commit(IDENTITY_LOGOUT);
    }
  },
  created() {
    if (localStorage.token) {
      this.$store.commit(IDENTITY_FETCH);
    }
  },
  computed: {
    ...mapState({ // TODO: refactor to getters
      firstName: state => state.identity.firstName
    })
  }
}
</script>

<style>

</style>
