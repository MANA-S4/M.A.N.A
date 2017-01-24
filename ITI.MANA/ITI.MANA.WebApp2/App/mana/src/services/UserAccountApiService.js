import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/users";

class UserAccountApiService {
    constructor() {

        }

    async getUserAccountListAsync() {
        return await getAsync(endpoint, 'get', AuthService.accessToken);
    }

    async updateUsersAsync(model) {
        return await putAsync(endpoint,'', AuthService.accessToken, model);
    }
}

export default new UserAccountApiService()
