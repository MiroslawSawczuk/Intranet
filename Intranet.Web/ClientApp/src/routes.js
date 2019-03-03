import HomePage from './views/HomePage.vue';
import PostLogin from './views/PostLogin.vue';
import Admin from './views/Admin.vue';

export default [
  { 
    name: 'home',
    path: '/',
    component: HomePage 
  },
  { 
    name: 'postlogin',
    path: '/login-post',
    component: PostLogin
  },
  { 
   name: 'admin', // TODO: add router guard with permissions check
   path: '/admin',
   component: Admin
  }
]
