import HomePage from './views/HomePage.vue';
import PostLogin from './views/PostLogin.vue';
import User from './views/User.vue';
import FirstConfiguration from './views/FirstConfiguration.vue';

export default [
  { 
    name: 'home',
    path: '/',
    component: HomePage 
  },
  { 
    name: 'postlogin',
    path: '/post-login',
    component: PostLogin
  },
  { 
   name: 'user', // TODO: add router guard with permissions check
   path: '/user',
   component: User
  },
  { 
    name: 'firstconfiguration',
    path: '/first-configuration',
    component: FirstConfiguration
  }
]
