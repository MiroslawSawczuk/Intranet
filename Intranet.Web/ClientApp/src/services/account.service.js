import Vue from 'vue';

const url = '/api/account';

export default {
  loginCallback() {
    return Vue.http.get(`${url}/external-login-callback`);
  }
}
