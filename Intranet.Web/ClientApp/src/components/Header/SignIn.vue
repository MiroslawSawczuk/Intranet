<template>
  <div>
    <div v-if="!firstName" class="dropdown">
      <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
        Zaloguj się
      </button>
      <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
        <a class="dropdown-item" href="/api/account/sign-in">Microsoft</a>
      </div>
    </div>
    <span v-else class="navbar-text">
      Witaj {{ firstName }}!
    </span>
  </div>
</template>

<script>
import EventBus from '../../event-bus.js';

export default {
  name: 'SignIn',
  data() {
    return {
      firstName: null
    } 
  },
  created() {
    if (localStorage.token) {
      this.$http.get('api/identity/name').then(response => {
        this.firstName = response.body;
      });
    }

    EventBus.$on('user-logged', () => {
      this.$http.get('api/identity/name').then(response => {
        this.firstName = response.body;
      });
    });
  }
}
</script>

<style>

</style>
