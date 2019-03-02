import IdentityService from '@/services/identity.service';
import { AUTH_LOGIN, AUTH_FETCH, AUTH_LOGOUT } from './mutations.js';

const state = {
  userName: '',
  isAuthenticated: !!localStorage.token
};

const actions = {
 
};

const mutations = {
  [AUTH_LOGIN] (state) {
    IdentityService.loginCallback().then(response => {
      localStorage.token = response.body;
      IdentityService.name().then(response2 => {
        state.isAuthenticated = true;
        state.userName = response2.body;
      });
    });
  },
  [AUTH_FETCH] (state) {
      IdentityService.name().then(response => {
      state.userName = response.body;
    });
  },
  [AUTH_LOGOUT] (state) {
    localStorage.removeItem('token');
    state.userName = '';
    state.isAuthenticated = false;
  }
};

const getters = {
  name: state => state.auth.userName,
  isAuthenticated: state => state.auth.isAuthenticated
};

export default {
  state,
  actions,
  mutations,
  getters
};
