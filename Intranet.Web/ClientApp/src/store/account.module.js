import AccountService from '@/services/account.service';
import { ACCOUNT_SAVEUSER } from '@/store/actions.js';

const state = {
  isAuthenticated: !!localStorage.token,
  email: '',
  firstName: '',
  lastName: '',
  tenantId: ''
};

const actions = {
  [ACCOUNT_SAVEUSER] (context, userProps) {
    return new Promise((resolve, reject) => {
        AccountService.saveUser(userProps.email, userProps.tenantId, userProps.firstName, userProps.lastName)
        .then(response => {

          localStorage.token = response.body;
          resolve(response);

        }, error => {
          reject(error);
        })
    })
  }
};


export default {
  state,
  actions
};
