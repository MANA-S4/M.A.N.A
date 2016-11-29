import 'babel-polyfill'
import $ from 'jquery'
import 'bootstrap/dist/js/bootstrap'
import Vue from 'vue'
import store from './vuex/store'
import VueRouter from 'vue-router'

import App from './components/App.vue'
import Login from './components/Login.vue'
import Logout from './components/Logout.vue'
import Home from './components/Home.vue'

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
  }
};

AuthService.appRedirect = () => router.replace('/');

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})