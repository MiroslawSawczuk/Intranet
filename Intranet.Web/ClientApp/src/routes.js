import HomePage from './views/HomePage.vue';
import PostLogin from './views/PostLogin.vue';

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
   }
]
