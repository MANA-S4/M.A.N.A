<template>
    <div>
        <div class="page-header">
            <h1>Gestion des évènements</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`events/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un évènements</router-link>
            </div>
        </div>

        <div class="panel panel-default">
            <input type="text" name="search" v-model="search">
        </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Évènements</th>
                    <th>Date</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="List.length == 0">
                    <td colspan="7" class="text-center">Il n'y a actuellement aucun évènements à afficher.</td>
                </tr>

                <tr v-for="i of List">
                    <td>{{ i.eventName }}</td>
                    <td>{{ i.eventDate }}</td>
                    <td>
                        <router-link :to="``"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click=""><i class="glyphicon glyphicon-remove"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {

        data() {
            return {
                search: '',
                List: []
            }
        },

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

            ...mapActions(['refreshEventsList' ,'deleteEvents'])
        }
    }
</script>

<style lang="less">
tr {
    text-align: left;
}
.panel {
    text-align: left;
    background-color: #00b050;
}
.panel-body {
    background-color: #00b050;
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
</style>