<template>
  <div>
     <button v-on:click="fetch">
      Zmień swoje dane
    </button>
    <br/>
     <br/>
    <div v-if="toggleUserProp" >
      <label name="email">email</label><br/>
      <input v-model="email"/>
       <br/>
      <label name="firstName">name</label><br/>
      <input v-model="firstName"/>
       <br/>
      <label name="lastName">surname</label><br/>
      <input v-model="lastName"/>

      <button v-on:click="save">Zapisz zmiany</button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'UserPropEdit',
  data() {
    return {
      toggleUserProp: false,
      email: '',
      firstName:'',
      lastName:'',
    }
  },
  methods: {
    fetch: function() {
      this.$http.get('api/identity/userProp').then(response => {
        
       this.email = response.body.email;
       this.firstName = response.body.firstName;
       this.lastName = response.body.lastName;
       this.toggleUserProp = !this.toggleUserProp;
      });
    },
    save: function() {
       this.$http.post('api/identity/updateUserProp', {
              firstName: this.firstName,
              lastName: this.lastName
            }).then(response => {
                //update danych z roota
            });
    }
  }
}
</script>

<style>
</style>