import HomePage from './views/HomePage.vue';
import PostLogin from './views/PostLogin.vue';
import User from './views/User.vue';

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
   name: 'user', // TODO: add router guard with permissions check
   path: '/user',
   component: User
  }
]
