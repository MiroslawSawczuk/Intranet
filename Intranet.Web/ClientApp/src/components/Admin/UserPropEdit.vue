<template>
  <div>
     <button v-on:click="fetch" class="btn btn-secondary btn-sm">
      Change your data
    </button>
    <br/>
    <br/>

    <!-- <form v-if="showUserProp" class="user-prop-form, col-md-4" v-bind:class="[formValid ? 'was-validated' : '', 'user-prop-form', 'col-md-4']"> -->
    <div v-if="showUserProp" class="user-prop-form, col-md-4">
      <div class="form-group">
        <label for="email">email</label><br/>
        <input v-model="email" class="form-control" type="text" disabled/>
      </div>
      <div class="form-group">
        <label for="firstName">name</label><br/>
        <input v-model="firstName" class="form-control" placeholder="Enter first name" type="text" required/>
      </div>
      <div class="form-group">
        <label for="lastName">surname</label><br/>
        <input v-model="lastName" class="form-control" placeholder="Enter last name" type="text" required />
      </div>

      <button v-on:click="save" class="btn btn-primary btn-sm">Save changes</button>
    </div>
  </div>
</template>

<script>
import { AUTH_FETCH } from './../../store/mutations.js';

export default {
  name: 'UserPropEdit',
  data() {
    return {
      showUserProp: false,
      email: '',
      firstName:'',
      lastName:'',
    }
  },
  methods: {
    fetch: function() {
      if (!this.showUserProp) {
        this.$http.get('api/identity/userProp').then(response => 
        {
          this.email = response.body.email;
          this.firstName = response.body.firstName;
          this.lastName = response.body.lastName;

          this.showUserProp = true;
        }, response => {
            swal({
              title: 'Ups',
              text: 'An error occurs with fetching the data',
              icon: "error",
            });
        });
      }
      else{
        this.showUserProp = false;
      }
    },
    save: function() {
      if (this.firstName && this.lastName) 
      {
        this.$http.post('api/identity/updateUserProp', 
        {
          FirstName: this.firstName,
          LastName: this.lastName
        })
        .then(response => {
          this.$store.commit(AUTH_FETCH);
          swal({
              title: 'Success',
              text: 'The data has been correctly saved',
              icon: "success",
            });

          this.showUserProp = false;
        }, response => {
            swal({
              title: 'Ups',
              text: 'An error occurs with with saving the data',
              icon: "error",
            });
        });
      }
    }
  }
}
</script>

<style>
</style>