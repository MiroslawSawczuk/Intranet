import IdentityService from '@/services/identity.service';
import { AUTH_LOGIN, AUTH_FETCH, LOGOUT } from '@/store/actions';
import { SET_AUTH, SET_NAME, REMOVE_TOKEN } from '@/store/mutations';

const state = {
  userName: '',
  isAuthenticated: !!localStorage.token
};

const getters = {

};

const actions = {
  [AUTH_LOGIN] (context) {
    return IdentityService.loginCallback().then(response => {
      localStorage.token = response.body;
      IdentityService.name().then(response2 => {
        context.commit(SET_AUTH, response2.body);
      });
    });
  },
  [AUTH_FETCH] (context) {
    return IdentityService.name().then(response => {
      context.commit(SET_NAME, response.body);
    });
  },
  [LOGOUT] (context) {
    return context.commit(REMOVE_TOKEN);
  }
};

const mutations = {
  [SET_AUTH] (state, name) {
    state.isAuthenticated = true;
    state.userName = name;
  },
  [SET_NAME] (state, name) {
    state.userName = name;
  },
  [REMOVE_TOKEN] (state) {
    localStorage.removeItem("token");
    state.userName = '';
    state.isAuthenticated = false;
  }
};

export default {
  state,
  actions,
  mutations,
  getters
};
