import Vue from 'vue';

export default {
  loginCallback() {
    return Vue.http.get('/api/account/external-login-callback');
  },
  name() {
    return Vue.http.get('api/identity/name');
  }
}
