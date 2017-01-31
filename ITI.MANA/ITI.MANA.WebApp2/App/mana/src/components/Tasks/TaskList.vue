<template>
    <div>
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

            <div class="card" v-for="i of list">
                <header class="card-header">
                    <p class="card-header-title">
                        {{i.taskName}}
                    </p>
                    <a class="card-header-icon">
                        <span class="icon">
                    <i class="fa fa-angle-down"></i>
                </span>
                    </a>
                </header>
                <div class="card-content">
                    <div class="content">
                        {{i.email}}
                        <br><br>
                        <small>{{i.taskDate}}</small>
                    </div>
                </div>
                <footer class="card-footer">
                    <a class="card-footer-item">
                        <router-link :to="`tasks/edit/${i.taskId}`">Edit</a>
                    </router-link>
                    <a class="card-footer-item" id="show-modal" @click="openDeleteTaskPrompt(i.taskId)">Delete</a>
                </footer>

                <delete-task-prompt v-if="showModal" @close="showModal = false" v-bind:taskId="deletingTaskId">
                    <h3 slot="header">Suppression</h3>
                </delete-task-prompt>
            </div>
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