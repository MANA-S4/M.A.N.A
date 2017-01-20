import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/user";

class UserAccountApiService {
    constructor() {

        }

    async getUserAccountListAsync() {
        return await getAsync(endpoint, '', AuthService.accessToken);
    }

    async getUserAccountListAsync(userId) {
        return await getAsync(endpoint, userId, AuthService.accessToken);
    }

    async updateUserAccountAsync(model) {
        return await putAsync(endpoint, model.userId, AuthService.accessToken, model);
    }
}

export default new UserAccountApiService()
