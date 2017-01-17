import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'
import AuthService from './AuthService'

const endpoint = "/api/calendars";

class CalendarApiService {
    constructor() {

        }

    async getEventsListAsync() {
        return await getAsync(endpoint, '', AuthService.accessToken);
    }

    async ExportFromGoogleAsync(userId) {
        return await getAsync(endpoint, '', AuthService.accessToken);
    }

    async createEventsAsync(model) {
        return await postAsync(endpoint, '', AuthService.accessToken, model);
    }

    async updateEventsAsync(model) {
        return await putAsync(endpoint, model.eventId, AuthService.accessToken, model);
    }

    async deleteEventsAsync(eventId) {
        return await deleteAsync(endpoint, eventId, AuthService.accessToken);
    }
}

export default new CalendarApiService()
