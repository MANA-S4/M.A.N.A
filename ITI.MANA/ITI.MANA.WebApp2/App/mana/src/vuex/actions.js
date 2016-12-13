import * as types from './mutation-types'

import ContactApi from '../services/ContactApiService'

// Wraps the async call to an api service in order to handle loading, and errors.
async function wrapAsyncApiCall(commit, apiCall, rethrowError) {
    commit(types.SET_IS_LOADING, true);

    let result = null;

    try {
        return await apiCall();
    }
    catch (error) {
        commit(types.ERROR_HAPPENED, `${error.status}: ${error.responseText || error.statusText}`);
        
        if(rethrowError) throw error;
    }
    finally {
        commit(types.SET_IS_LOADING, false);
    }
}

// Generic request
export async function requestAsync({ commit }, action, rethrowError) {
    return await wrapAsyncApiCall(commit, action, rethrowError);
}

// Contacts
export async function createContact({ commit }, model) {
    var result = await wrapAsyncApiCall(commit, () => ContactApi.createContactAsync(model));
    if(result) commit(types.ADD_CONTACT, result);
    return result;
}

export async function updateContact({ commit }, model) {
    var result = await wrapAsyncApiCall(commit, () => ContactApi.updateContactAsync(model));
    if(result) commit(types.EDIT_CONTACT, result);
    return result;
}

export async function deleteContact({ commit }, contactId) {
    var result = await wrapAsyncApiCall(commit, () => ContactApi.deleteContactAsync(contactId));
    if(result) commit(types.REMOVE_CONTACT, result)
    return result;
}

export async function refreshContactList({ commit }) {
    var result = await wrapAsyncApiCall(commit, () => ContactApi.getContactListAsync());
    if(result) commit(types.REFRESH_CONTACT_LIST, result);
    return result;
}

