<template>
    <div>
        <div class="page-header">
            <h1>Gestion des évènements</h1>
        </div>

        <div>
            <div class="panel-body text-right">
                <button type="button" @click="exportFromGoogleAsync" class="btn btn-success"><i class="glyphicon glyphicon-download-alt"></i> Importer votre calendrier Google</button>
                <router-link class="btn btn-success" :to="`calendars/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un évènement</router-link>
            </div>
        </div>

        <div class="panel">
            <input type="text" name="search" v-model="search" placeholder="Rechercher"> <i class="glyphicon glyphicon-search"></i>
        </div>

        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th>Évènements</th>
                    <th>Date</th>
                    <th>Visibilité</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="List.length == 0">
                    <td colspan="7" class="text-center">Il n'y a actuellement aucun évènement à afficher.</td>
                </tr>

                <tr v-for="i of List">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.eventDate }}</td>
                    <td v-if="i.isPrivate == true">Privé</td>
                    <td v-else>Public</td>
                    <td>
                        <router-link :to="`calendars/edit/${i.eventId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#"><i class="glyphicon glyphicon-remove" id="show-modal" @click="openDeleteEventPrompt(i.eventId)"></i></a>
                        <!-- To open the popup-->
                    </td>
                </tr>
            </tbody>
        </table>

        <!-- If user click on "Non" popup close -->
        <delete-calendar-prompt v-if="showModal" @close="showModal = false" v-bind:eventId="deletingEventId">
            <h3 slot="header">Suppression</h3>
        </delete-calendar-prompt>
        <!-- End -->

    </div>
</template>

<script>
    import CalendarApiService from "../../services/CalendarApiService"
    import { mapGetters, mapActions } from 'vuex'
    import DeleteCalendarPrompt from './DeleteCalendarPrompt.vue'

    export default {

        data() {
            return {
                // Define popup false to default
                template: '#modal-template',
                showModal: false,
                deletingEventId: 0,
                // End popup
                search: '',
                List: []
            }
        },

        // Call vue DeleteCalendarPrompt
        components: {
            DeleteCalendarPrompt
        },
        // End

        created() {
            this.refreshEventsList();
        },

        computed: {
            ...mapGetters(['eventsList']),

            List: function () {
                let calendars = [];
                let i = 0;

                for (i = 0; i < this.eventsList.length; i++) {
                    if (this.eventsList[i].eventName.includes(this.search)) {
                        calendars.push(this.eventsList[i]);
                    }
                }
                return calendars;
            }
        },

        methods: {
            updateResource(data) {
                this.List = data
            },

            exportFromGoogleAsync() {
                CalendarApiService.exportFromGoogleAsync();
                location.reload();
            },

            ...mapActions(['refreshEventsList' ,'deleteEvent']),

            openDeleteEventPrompt(eventId) {
                this.deletingEventId = eventId;
                this.showModal = true;
            }
        }
    }
</script>

<style lang="less">
tr {
    text-align: left;
}
.panel {
    text-align: left;
    background-color: #bdc3c7;
}
.panel-body {
    background-color: #bdc3c7;
}
.table {
    background-color: rgba(17,42,13,.5);
}
.glyphicon-remove {
    color: black;
}
.glyphicon-pencil {
    color: black;
}
button {
    width: 181,28px;
    height: 32,9px;
}
</style>