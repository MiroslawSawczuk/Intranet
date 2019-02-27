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
   name: 'admin',
   path: '/admin',
   component: Admin
  }
]
