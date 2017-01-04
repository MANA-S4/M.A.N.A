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
                    <th>TaskId</th>
                    <th>ToDo</th>
                    <th>User</th>
                    <th>Options</th>       
                </tr>
            </thead>

            <tbody>
                <tr v-for="i of list">
                    <td>{{ i.taskId }}</td>
                    <td>{{ i.toDo }}</td>
                    <td>{{ i.user }}</td>
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
            /*list: function() {
                let task =  [];
                let i = 0;
                
                for(i = 0; i < this.taskList.length; i++) {
                    if (this.taskList[i].toDo.includes(this.search)) {
                        task.push(this.taskList[i]);
                    }
                }
                return task;
            }*/
            /// ------------------------------------------------------------------------   
        },

        methods: {
            ...mapActions(['refreshTaskList' ,'deleteTask'])
        }
    }
</script>

<style lang="less">

</style>