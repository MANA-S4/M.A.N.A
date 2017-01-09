<template>
    <div>
        <div class="page-header">
            <h1>Gestion des tâches</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`tasks/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter une tâche</router-link>
            </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Date</th>
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
                    <td>
                        <router-link :to="`tasks/edit/${i.taskId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteTask(i.taskId)"><i class="glyphicon glyphicon-remove"></i></a>          
                    </td>
                </tr>

            </tbody>
        </table>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {
        //-------------------------------
        data() {
            return {
                search: '',
                list: []
            }
        },
        //-------------------------------

        created() {
            this.refreshTaskList();
        },

        computed: {
            ...mapGetters(['taskList']),

            /// ---------------------- Question 4 -------------------------------------
            list: function() {
                let task =  [];
                let i = 0;
                
                for(i = 0; i < this.taskList.length; i++) {
                    if (this.taskList[i].taskName.includes(this.search)) {
                        task.push(this.taskList[i]);
                    }
                }
                return task;
            }
            /// ------------------------------------------------------------------------   
        },

        methods: {
            ...mapActions(['refreshTaskList' ,'deleteTask'])
        }
    }
</script>

<style lang="less">

</style>