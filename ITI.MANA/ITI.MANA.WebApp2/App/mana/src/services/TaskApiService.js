import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/task";

class TaskApiService {
    constructor() {

        }

    async getTaskListAsync() {
        return await getAsync(endpoint, '', AuthService.accessToken);
    }

    async getTaskAsync(taskId) {
        return await getAsync(endpoint, taskId, AuthService.accessToken);
    }

    async createTaskAsync(model) {
        return await postAsync(endpoint, '', AuthService.accessToken, model);
    }

    async updateTaskAsync(model) {
        return await putAsync(endpoint, model.taskId, AuthService.accessToken, model);
    }

    async deleteTaskAsync(taskId) {
        return await deleteAsync(endpoint, taskId, AuthService.accessToken);
    }
}

export default new TaskApiService()
