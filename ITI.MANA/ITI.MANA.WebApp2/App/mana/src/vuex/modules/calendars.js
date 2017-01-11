import * as types from '../mutation-types'

const state = {
    eventsList: []
}

const mutations = {
    [types.ADD_EVENT](state, model) {
        state.eventsList.push(model)
    },

    [types.EDIT_EVENT](state, model) {
        let idx = state.eventsList.findIndex( x => x.eventId == model.eventId);

        state.eventsList[idx] = model;
    },

    [types.REMOVE_EVENT](state, eventId) {
        let idx = state.eventsList.findIndex( x => x.eventId == eventId);

        state.eventsList.splice(idx, 1);
    },

    [types.REFRESH_EVENT_LIST](state, list) {
        state.eventsList = list;
    }
}

export default {
    state,
    mutations
}