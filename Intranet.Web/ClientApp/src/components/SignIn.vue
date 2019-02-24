<template>
  <div>
    <div v-if="!name" class="dropdown">
      <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Zaloguj się
      </button>
      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <a class="dropdown-item" href="/api/account/sign-in">Microsoft</a>
      </div>
    </div>

    <div v-else>
      <span class="navbar-text">
        Witaj {{ name }}!
      </span>
      &nbsp;
      <button  v-on:click="logout()" class="btn btn-secondary" type="button" >
       Wyloguj
      </button>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import { AUTH_FETCH } from '@/store/actions';

export default {
  name: 'SignIn',
  methods:{
    logout() {
      // usunięcie tokena w local storage
      // usunięcie tokena na backendzie
      // przekierownie w button na <a href='homepage'>
    }
  },
  created() {
    if (localStorage.token) {
      this.$store.dispatch(AUTH_FETCH);
    }
  },
  computed: {
    ...mapState({
      name: state => state.auth.userName
    })
  }
}
</script>

<style>

</style>
