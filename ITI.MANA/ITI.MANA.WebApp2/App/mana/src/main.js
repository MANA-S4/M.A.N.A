import 'babel-polyfill'
import $ from 'jquery'
import 'bootstrap/dist/js/bootstrap'
import Vue from 'vue'
import store from './vuex/store'
import VueRouter from 'vue-router'

import App from './components/App.vue'
import Home from './components/Home.vue'
import Login from './components/Login.vue'
import Logout from './components/Logout.vue'

import UserAccountEdit from './components/Users/UserAccountEdit.vue'
import UserAccountList from './components/Users/UserAccountList.vue'

import ContactEdit from './components/Contacts/ContactEdit.vue'
import ContactList from './components/Contacts/ContactList.vue'

import Calendars from './components/Calendars/CalendarsList.vue'
import CalendarsEdit from './components/Calendars/CalendarsEdit.vue'

import TaskEdit from './components/Tasks/TaskEdit.vue'
import TaskList from './components/Tasks/TaskList.vue' 

import AuthService from './services/AuthService'

Vue.use(VueRouter)

function requireAuth (to, from, next)  {
  if (!AuthService.isConnected) {
    next({
      path: '/login',
      query: { redirect: to.fullPath }
    });

    return;
  }

var requiredProviders = to.meta.requiredProviders;

  if(requiredProviders && !AuthService.isBoundToProvider(requiredProviders)) {
    next( false )
  };

  next();
}

const router = new VueRouter({
  mode: 'history',
  base: '/Home',
  routes: [
    { path: '/login', component: Login },
    { path: '/logout', component: Logout, beforeEnter: requireAuth },
    
    { path: '', component: Home, beforeEnter: requireAuth },

    { path: '/users', component: UserAccountList, beforeEnter: requireAuth },
    { path: '/users/:mode([create|edit]+)/:id?', component: UserAccountEdit, beforeEnter: requireAuth },

    { path: '/contacts', component: ContactList, beforeEnter: requireAuth },
    { path: '/contacts/:mode([create|edit]+)/:id?', component: ContactEdit, beforeEnter: requireAuth },
    
    { path: '/calendars', component: Calendars, beforeEnter: requireAuth },
    { path: '/calendars/:mode([create|edit]+)/:id?', component: CalendarsEdit, beforeEnter: requireAuth },
    


    { path: '/tasks', component: TaskList, beforeEnter: requireAuth },
    { path: '/tasks/:mode([create|edit]+)/:id?', component: TaskEdit, beforeEnter : requireAuth }   
    
  ]
})

AuthService.allowedOrigins = ['http://localhost:5000'];

AuthService.logoutEndpoint = '/Account/LogOff';

AuthService.providers = {
  'Base': {
    endpoint: '/Account/Login'
  },
  'Google': {
    endpoint: '/Account/ExternalLogin?provider=Google'
  },
  'Microsoft': {
    endpoint: '/Account/ExternalLogin?provider=Microsoft'
  }
};

AuthService.appRedirect = () => router.replace('/');

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})