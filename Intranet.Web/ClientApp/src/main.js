import Vue from 'vue';
import VueRouter from 'vue-router';
import App from './App.vue';
import routes from './routes.js';
import VueResource from 'vue-resource';
import store from './store/store.js';
import swal from 'sweetalert';

Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(VueResource);

Vue.http.interceptors.push(request => {
  if (localStorage.token) {
    request.headers.set('Authorization', `Bearer ${localStorage.token}`);
  }
});

const router = new VueRouter({
  routes: routes,
  mode: 'history'
});

new Vue({
  render: h => h(App),
  router: router,
  store: store,
}).$mount('#app');
