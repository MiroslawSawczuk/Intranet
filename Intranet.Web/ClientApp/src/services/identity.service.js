import Vue from 'vue';

const url = '/api/identity';

export default {
  name() {
    return Vue.http.get(`${url}/name`);
  },
  userProps() {
    return Vue.http.get(`${url}/user-props`);
  },
  updateUserProps(firstName, lastName) {
    return Vue.http.patch(`${url}/update-user-props`, { firstName, lastName });
  }
}
