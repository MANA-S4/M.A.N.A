import * as types from './mutation-types'

import EventApi from '../services/CalendarApiService'

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

//CalendarApiService
export async function createEvent({ commit }, model) {
    var result = await wrapAsyncApiCall(commit, () => EventApi.createEventsAsync(model));
    if(result) commit(types.ADD_EVENT, result);
    return result;
}

export async function updateEvent({ commit }, model) {
   var result = await wrapAsyncApiCall(commit, () => EventApi.updateEventsAsync(model));
   if(result) commit(types.EDIT_EVENT, result);
   return result;
}

export async function deleteEvent({ commit }, eventId) {
    var result = await wrapAsyncApiCall(commit, () => EventApi.deleteEventsAsync(eventId));
    if(result) commit(types.REMOVE_EVENT, result);
    return result;
}

export async function refreshEventsList({ commit }) {
    var result = await wrapAsyncApiCall(commit, () => EventApi.getEventsListAsync());
    if(result) commit(types.REFRESH_EVENT_LIST, result);
    return result;
}