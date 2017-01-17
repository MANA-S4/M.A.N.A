// App
export const isLoading = ({ app }) => app.isLoading // eq. export const isLoading = state => state.app.isLoading

// Contacts
export const eventsList = ({ calendars }) => calendars.eventsList
export const contactList = ({ contacts }) => contacts.contactList

// Tasks
export const taskList = ({ tasks }) => tasks.taskList