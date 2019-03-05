import IdentityService from '@/services/identity.service';
import AccountService from '@/services/account.service';
import { IDENTITY_FETCH, IDENTITY_UPDATE, IDENTITY_LOGIN, IDENTITY_LOGOUT } from '@/store/mutations.js';

const state = {
  email: '',
  firstName: '',
  lastName: '',
  isAuthenticated: !!localStorage.token
};

const mutations = {
  [IDENTITY_LOGIN] (state) {
    AccountService.loginCallback().then(response => {
      localStorage.token = response.body;
      IdentityService.userProps().then(response2 => {
        state.isAuthenticated = true;
        state.email = response2.body.email;
        state.firstName = response2.body.firstName;
        state.lastName = response2.body.lastName;
      });
    });
  },
  [IDENTITY_LOGOUT] (state) {
    localStorage.removeItem('token');
    state.firstName = '';
    state.isAuthenticated = false;
  },
  [IDENTITY_FETCH] (state) {
    IdentityService.userProps().then(response => {
      state.email = response.body.email;
      state.firstName = response.body.firstName;
      state.lastName = response.body.lastName;
    });
  },
  [IDENTITY_UPDATE] (state, userProps) {
    IdentityService.updateUserProps(userProps.firstName, userProps.lastName).then(() => {
      state.firstName = userProps.firstName;
      state.lastName = userProps.lastName;

      swal({
        title: 'Success',
        text: 'The data has been correctly saved',
        icon: "success",
      });
    });
  }
};

// const getters = {
//   email: state => state.identity.email,
//   firstName: state => state.identity.firstName,
//   lastName: state => state.identity.lastName,
//   isAuthenticated: state => state.identity.isAuthenticated
// };

export default {
  state,
  mutations
  // getters
};
