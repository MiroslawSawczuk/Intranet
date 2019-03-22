import IdentityService from '@/services/identity.service';
import AccountService from '@/services/account.service';
import { IDENTITY_SAVEUSERPROPS, IDENTITY_REMOVEUSERPROPS, IDENTITY_UPDATEUSERPROPS, IDENTITY_SAVETENANTID } from '@/store/mutations.js';
import { TEST, IDENTITY_FETCH, REDIRECT_NOTFOUND, IDENTITY_LOGIN, IDENTITY_LOGOUT, IDENTITY_UPDATE, DECODE_TOKEN, IDENTITY_AUTHENTICATION } from '@/store/actions.js';
import { router } from '@/main.js';

const state = {
  isAuthenticated: !!localStorage.token,
  email: '',
  firstName: '',
  lastName: '',
  tenantId: ''
};

const mutations = {
  [IDENTITY_SAVEUSERPROPS] (state , userProps) {
    state.isAuthenticated = userProps.isAuthenticated;
    state.firstName = userProps.firstName;
    state.lastName = userProps.lastName;
    state.email = userProps.email;
  },
  [IDENTITY_REMOVEUSERPROPS] (state) {
    state.isAuthenticated = false;
    state.firstName = '';
    state.lastName = '';
    state.email = '';
  },
  [IDENTITY_UPDATEUSERPROPS] (state, userProps) {
    state.firstName = userProps.firstName;
    state.lastName = userProps.lastName;
  },
  [IDENTITY_SAVETENANTID] (state, decodedToken) 
  {
    state.tenantId = decodedToken.TenantId;
  }
};

const actions = {
  [REDIRECT_NOTFOUND] () {
    router.push({ name: 'pagenotfound' });
  },
  [IDENTITY_LOGIN] (context) {
       AccountService.loginCallback().then(response => {
        
        var token= response.body.token;

        context.commit(IDENTITY_SAVEUSERPROPS, 
        {
          email: response.body.email,
          isAuthenticated: response.body.token ? true : false
        });

        if (token != null) {
          localStorage.token = token;
          context.dispatch(DECODE_TOKEN, token);

        }
        else {
          router.push({ name: 'firstconfiguration' });
        }
    });
  },
  [IDENTITY_LOGOUT] (context) {
    localStorage.removeItem('token');
    context.commit(IDENTITY_REMOVEUSERPROPS);
      router.push({ name: 'startpage' });
    
  },
  [IDENTITY_FETCH] (context) {
      IdentityService.userProps().then(response => {

       context.commit(IDENTITY_SAVEUSERPROPS, 
          {
            isAuthenticated : true,
            email : response.body.email,
            firstName : response.body.firstName,
            lastName : response.body.lastName,
            tenantId: response.body.tenantId
          })
    })
  },
  [IDENTITY_UPDATE] (context, userProps) {
    IdentityService.updateUserProps(userProps.firstName, userProps.lastName).then(() => {
      context.commit(IDENTITY_UPDATEUSERPROPS, userProps);

      swal({
        title: 'Success',
        text: 'The data has been correctly saved',
        icon: "success",
      });
    });
  },
  [DECODE_TOKEN] (context, token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var decodedToken = JSON.parse(window.atob(base64));

    context.commit(IDENTITY_SAVETENANTID, decodedToken)
  },
  [IDENTITY_AUTHENTICATION] (context) {
    if(!context.getters.isAuthenticated) {
      router.push({ name: 'startpage' });
    }
  }
};

const getters = {
  tenantId: (state) => {
    return state.tenantId;
  },
  email: (state) => {
    return state.email;
  },
  isAuthenticated: (state) => {
    return state.isAuthenticated;
  }
}

export default {
  state,
  mutations,
  actions,
  getters
};
