import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/contact";

class ContactApiService {
    constructor() {

        }

    async getContactListAsync() {
        return await getAsync(endpoint, '', AuthService.accessToken);
    }

    async getContactAsync(contactId) {
        return await getAsync(endpoint, contactId, AuthService.accessToken);
    }

    async createContactAsync(model) {
        return await postAsync(endpoint, '', AuthService.accessToken, model);
    }

    async updateContactAsync(model) {
        return await putAsync(endpoint, model.contactId, AuthService.accessToken, model);
    }

    async deleteContactAsync(contactId) {
        return await deleteAsync(endpoint, contactId, AuthService.accessToken);
    }
}

export default new ContactApiService()
