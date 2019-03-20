<template>
  <div>
    <div>This is Configuration Panel. Enter your data below to use Intranet.</div>
    
    <div class="user-prop-form col-md-4">
      <div class="form-group">
        <label for="email">Email</label><br/>
        <input :value="email" class="form-control" placeholder="Enter email" type="text" disabled/>
      </div>
       <div class="form-group">
        <label for="firstName">Tenant</label><br/>
        <input v-model="tenantId" class="form-control" placeholder="Enter tenant number" type="text" required/>
      </div>
      <div class="form-group">
        <label for="firstName">Name</label><br/>
        <input v-model="firstName" class="form-control" placeholder="Enter first name" type="text" required/>
      </div>
      <div class="form-group">
        <label for="lastName">Surname</label><br/>
        <input v-model="lastName" class="form-control" placeholder="Enter last name" type="text" required />
      </div>

      <button @click="save" class="btn btn-primary btn-sm">Save changes</button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'FirstConfiguration',
  data(){
    return{
      firstName: '',
      lastName: ''
    }
  }
}
</script>

<style lang="scss">

</style>
