import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/users";

class UserAccountApiService {
    constructor() {

        }

    async getUserAccountListAsync() {
        return await getAsync(endpoint, 'get', AuthService.accessToken);
    }

    async updateUserAccountAsync(model) {
        return await putAsync(endpoint, model.userId, AuthService.accessToken, model);
    }
}

export default new UserAccountApiService()
