import IdentityService from '@/services/identity.service';

const state = {
  userName: '',
  isAuthenticated: !!localStorage.token
};

const actions = {
 
};

const mutations = {
  auth_login (state) {
    IdentityService.loginCallback().then(response => {
      localStorage.token = response.body;
      IdentityService.name().then(response2 => {
        state.isAuthenticated = true;
        state.userName = response2.body;
      });
    });
  },
  auth_fetch (state) {
    IdentityService.name().then(response => {
      state.userName = response.body;
    });
  },
  auth_logout (state) {
    localStorage.removeItem('token');
    state.userName = '';
    state.isAuthenticated = false;
  }
};

const getters = {

};

export default {
  state,
  actions,
  mutations,
  getters
};
