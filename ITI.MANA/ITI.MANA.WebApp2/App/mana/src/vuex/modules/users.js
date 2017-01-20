import * as types from '../mutation-types'

const state = {
    userAccountList: []
}

const mutations = {
    [types.EDIT_USER](state, model) {
        let idx = state.taskList.findIndex( x => x.userId == model.userId);

        state.userAccountList[idx] = model;
    },

    [types.REFRESH_USER_LIST](state, list) {
        state.userAccountList = list;
    }
}

export default {
    state,
    mutations
}