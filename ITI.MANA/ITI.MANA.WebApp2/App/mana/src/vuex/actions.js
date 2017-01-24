import * as types from './mutation-types'

import ContactApi from '../services/ContactApiService'
import TaskApi from '../services/TaskApiService'
import EventApi from '../services/CalendarApiService'
import UserAccountApi from '../services/UserAccountApiService'


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

// Tasks
export async function createTask({ commit }, model) {
    var result = await wrapAsyncApiCall(commit, () => TaskApi.createTaskAsync(model));
    if(result) commit(types.ADD_TASK, result);
    return result;
}

export async function updateTask({ commit }, model) {
    var result = await wrapAsyncApiCall(commit, () => TaskApi.updateTaskAsync(model));
    if(result) commit(types.EDIT_TASK, result);
    return result;
}

export async function deleteTask({ commit }, taskId) {
    var result = await wrapAsyncApiCall(commit, () => TaskApi.deleteTaskAsync(taskId));
    if(result) commit(types.REMOVE_TASK, result)
    return result;
}

export async function refreshTaskList({ commit }) {
    var result = await wrapAsyncApiCall(commit, () => TaskApi.getTaskListAsync());
    if(result) commit(types.REFRESH_TASK_LIST, result);
    return result;
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

// UserAccountApi
export async function updateUser({ commit }, model) {
   var result = await wrapAsyncApiCall(commit, () => UserAccountApi.updateUsersAsync(model));
   if(result) commit(types.EDIT_USER, result);
   return result;
}

export async function refreshUserAccountList({ commit }) {
    var result = await wrapAsyncApiCall(commit, () => UserAccountApi.getUserAccountListAsync());
    if(result) commit(types.REFRESH_USER_LIST, result);
    return result;
}

export async function exportFromGoogle({ commit }) {
    var result = await wrapAsyncApiCall(commit, () => EventApi.exportFromGoogleAsync());
    if(result) commit(types.EXPORT_FROM_GOOGLE, result);
    return result;
}