import IdentityService from '@/services/identity.service';
import { AUTH_LOGIN, AUTH_FETCH } from '@/store/actions';
import { SET_AUTH, SET_NAME } from '@/store/mutations';

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
  }
};

const mutations = {
  [SET_AUTH] (state, name) {
    state.isAuthenticated = true;
    state.userName = name;
  },
  [SET_NAME] (state, name) {
    state.userName = name;
  }
};

export default {
  state,
  actions,
  mutations,
  getters
};
