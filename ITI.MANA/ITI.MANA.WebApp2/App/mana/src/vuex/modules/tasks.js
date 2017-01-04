import * as types from '../mutation-types'

const state = {
    taskList: []
}

const mutations = {
    [types.ADD_TASK](state, model) {
        state.taskList.push(model)
    },

    [types.EDIT_TASK](state, model) {
        let idx = state.taskList.findIndex( x => x.taskId == model.taskId);

        state.taskList[idx] = model;
    },

    [types.REMOVE_TASK](state, taskId) {
        let idx = state.taskList.findIndex( x => x.taskId == taskId);

        state.taskList.splice(idx, 1);
    },

    [types.REFRESH_TASK_LIST](state, list) {
        state.taskList = list;
    }
}

export default {
    state,
    mutations
}