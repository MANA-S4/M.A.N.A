import Vue from 'vue'
import Vuex from 'vuex'

import * as actions from './actions'
import * as getters from './getters'

import app from './modules/app'
import contacts from './modules/contacts'
import calendars from './modules/calendars'
import tasks from './modules/tasks'
import users from './modules/users'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  actions,
  getters,
  modules: {
      app,
      contacts,
      calendars,
      tasks,
      users
  },
  strict: debug
})