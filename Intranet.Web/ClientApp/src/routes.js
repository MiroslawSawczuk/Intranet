import HomePage from './views/HomePage.vue';
import PostLogin from './views/PostLogin.vue';
import User from './views/User.vue';
import FirstConfiguration from './views/FirstConfiguration.vue';
import PageNotFound from './views/Errors/PageNotFound.vue';
import StartPage from './views/StartPage.vue';


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
  },
  { 
    name: 'pagenotfound',
    path: '/page-not-found',
    component: PageNotFound
  },
  { 
    name: 'startpage',
    path: '/start-page',
    component: StartPage
  }
]
