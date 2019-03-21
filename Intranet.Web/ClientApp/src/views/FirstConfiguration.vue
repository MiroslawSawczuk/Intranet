<template>
  <div>
    <div>This is Configuration Panel. Enter your data below to use Intranet.</div>
    
    <div class="user-prop-form col-md-4">
      <div class="form-group">
        <label for="email">Email</label><br/>
        <input v-model="email" class="form-control" placeholder="Enter email" type="text" disabled/>
      </div>
       <div class="form-group">
        <label for="firstName">Tenant</label><br/>
        <input v-model="tenantId" class="form-control" placeholder="Enter tenant number" type="text" required/>
      </div>
      <div class="form-group">
        <label for="firstName">Name</label><br/>
        <input v-model="firstName"  class="form-control" placeholder="Enter first name" type="text" required/>
      </div>
      <div class="form-group">
        <label for="lastName">Last name</label><br/>
        <input v-model="lastName" class="form-control" placeholder="Enter last name" type="text" required />
      </div>

      <button @click="save"  class="btn btn-primary btn-sm">Save changes</button>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import { router } from '@/main.js';
import { ACCOUNT_SAVEUSER, IDENTITY_FETCH } from '@/store/actions.js';

export default {
  name: 'FirstConfiguration',
  data(){
    return{
      tenantId: '',
      firstName: '',
      lastName: '',
    }
  },
  methods:{
      save() {
      if (this.email, this.tenantId, this.firstName && this.lastName) {
        this.$store.dispatch(ACCOUNT_SAVEUSER,
          { 
            email: this.email,
            tenantId: this.tenantId,
            firstName: this.firstName, 
            lastName: this.lastName
          })
          .then(response=> { 
              this.$store.dispatch(IDENTITY_FETCH);
              
              swal({
                title: 'Success!',
                text: 'Successed saved new user. You wiil be redirected to the Home Page!',
                icon: 'success'
              }).then(function() {
                router.push({ name: 'home' });
              });
          }, function(error) {
					    swal({
                title: 'Error',
                text: error.body.toString(),
                icon: 'error',
              });
				  });
      }
      else{
        swal({
          title: 'Warning',
          text: 'You must complete all your data',
          icon: 'warning',
        });
      }
    }
  },
  computed: {
    ...mapState({ 
      email: state => state.identity.email
    })
  }
}
</script>

<style lang="scss">

</style>
