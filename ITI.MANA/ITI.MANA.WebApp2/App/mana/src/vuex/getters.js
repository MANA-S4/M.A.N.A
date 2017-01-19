// App
export const isLoading = ({ app }) => app.isLoading // eq. export const isLoading = state => state.app.isLoading

// Contacts
export const contactList = ({ contacts }) => contacts.contactList

// Calendar
export const eventsList = ({ calendars }) => calendars.eventsList

// Tasks
export const taskList = ({ tasks }) => tasks.taskList