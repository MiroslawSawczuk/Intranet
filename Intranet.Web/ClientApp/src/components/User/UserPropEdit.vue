<template>
  <div>
    <button @click="fetch" class="btn btn-secondary btn-sm">
      Change your data
    </button>
    <br/>
    <br/>

    <div v-if="showUserProp" class="user-prop-form col-md-4">
      <div class="form-group">
        <label for="email">Email</label><br/>
        <input :value="emailEdit" class="form-control" placeholder="Enter email" type="text" disabled/>
      </div>
      <div class="form-group">
        <label for="firstNameEdit">Name</label><br/>
        <input v-model="firstNameEdit" class="form-control" placeholder="Enter first name" type="text" required/>
      </div>
      <div class="form-group">
        <label for="lastNameEdit">Surname</label><br/>
        <input v-model="lastNameEdit" class="form-control" placeholder="Enter last name" type="text" required />
      </div>

      <button @click="save" class="btn btn-primary btn-sm">Save changes</button>
    </div>
  </div>
</template>

<script>
import { IDENTITY_UPDATE, IDENTITY_FETCH } from '@/store/mutations.js';
import { mapState } from 'vuex';

export default {
  name: 'UserPropEdit',
  data() {
    return {
      showUserProp: false,
      firstNameEdit: '',
      lastNameEdit: ''
    }
  },
  methods: {
    fetch() {
      if (!this.showUserProp) {
        if (!this.email || !this.firstName || !this.lastName){
          this.$store.commit(IDENTITY_FETCH);
        }
        this.emailEdit = this.email;
        this.firstNameEdit = this.firstName;
        this.lastNameEdit = this.lastName;
      }
      this.showUserProp = !this.showUserProp;
    },
    save() {
      if (this.firstNameEdit && this.lastNameEdit) {
        this.$store.commit(IDENTITY_UPDATE, { firstName: this.firstNameEdit, lastName: this.lastNameEdit });
        this.showUserProp = !this.showUserProp;
      }
    }
  },
  computed: {
    ...mapState({ 
      email: state => state.identity.email,
      firstName: state => state.identity.firstName,
      lastName: state => state.identity.lastName,
    })
  }
}
</script>

<style scoped lang="scss">
</style>