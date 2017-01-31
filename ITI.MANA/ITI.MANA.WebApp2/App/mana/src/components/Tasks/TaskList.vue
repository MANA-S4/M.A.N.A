<template>
    <div>
        <div class="page-header">
            <h1>Gestion des tâches</h1>
        </div>

        <div>
            <div class="panel-body text-right">
                <router-link class="btn btn-success" :to="`tasks/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter une tâche</router-link>
            </div>
        </div>

        <!-- For the search, take a v-model -->
        <div class="panel">
            <input type="text" name="search" v-model="search" id="search" placeholder="Rechercher" /> <i class="glyphicon glyphicon-search"></i>
        </div>
        <!-- End of search div -->

        <table class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th>Nom de la tâche</th>
                    <th>Date</th>
                    <th>Attribué à </th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="taskList.length == 0">
                    <td colspan="6" class="text-center">Il n'y a actuellement aucune tâche.</td>
                </tr>

                <tr v-for="i of list">
                    <td>{{ i.taskName }}</td>
                    <td>{{ i.taskDate }}</td>
                    <td>{{ i.email }}</td>
                    <td>
                        <router-link :to="`tasks/edit/${i.taskId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#"><i class="glyphicon glyphicon-remove" id="show-modal" @click="openDeleteTaskPrompt(i.taskId)"></i></a>
                        <!-- To open the popup-->
                    </td>
                </tr>

            </tbody>
        </table>

        <!-- If user click on "Non" popup close -->
        <delete-task-prompt v-if="showModal" @close="showModal = false" v-bind:taskId="deletingTaskId">
            <h3 slot="header">Suppression</h3>
        </delete-task-prompt>
        <!-- End -->

    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'
    import DeleteTaskPrompt from './DeleteTaskPrompt.vue' // Import the vue DeleteTaskPrompt

    export default {
        data() {
            return {
                // Define popup false to default
                template: '#modal-template',
                showModal: false,
                deletingTaskId: 0,
                // End popup
                search: '',
                list: []
            }
        },

        // Call vue DeletePrompt
        components: {
            DeleteTaskPrompt
        },
        // End

        created() {
            this.refreshTaskList();
            this.refreshContactList();
        },

        computed: {
            ...mapGetters(['taskList','contactList']),

            list: function () {
                let task = [];
                let i = 0;

                for (i = 0; i < this.taskList.length; i++) {
                    if (this.taskList[i].taskName.includes(this.search)) {
                        task.push(this.taskList[i]);
                    }
                }
                return task;
            }
        },

        methods: {
            ...mapActions(['refreshTaskList' ,'deleteTask','refreshContactList']),

            openDeleteTaskPrompt(taskId) {
                this.deletingTaskId = taskId;
                this.showModal = true;
            }
        }
    }
</script>

<style lang="less">
@import "~bootstrap/dist/css/bootstrap.min.css";
.panel-body text-right {
    text-align: left;
    background-color: #bdc3c7; 
    }
.table table-striped table-hover table-bordered { background-color: #00b050;}
th {
    color: black;
}
td {
    color: black;
    text-align: left;
}
</style>