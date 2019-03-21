import Vue from 'vue';

const url = '/api/account';

export default {
  loginCallback() {
    return Vue.http.get(`${url}/external-login-callback`);
  },
  saveUser(email, tenantId, firstName, lastName) {
    return Vue.http.post(`${url}/save-user`, {email, tenantId, firstName, lastName});
  }
}
