import * as types from '../mutation-types'

const state = {
    contactList: []
}

const mutations = {
    [types.ADD_CONTACT](state, model) {
        state.contactList.push(model)
    },

    [types.EDIT_CONTACT](state, model) {
        let idx = state.contactList.findIndex( x => x.contactId == model.contactId);

        state.contactList[idx] = model;
    },

    [types.REMOVE_CONTACT](state, contactId) {
        let idx = state.contactList.findIndex( x => x.contactId == contactId);

        state.contactList.splice(idx, 1);
    },

    [types.REFRESH_CONTACT_LIST](state, list) {
        state.contactList = list;
    }
}

export default {
    state,
    mutations
}