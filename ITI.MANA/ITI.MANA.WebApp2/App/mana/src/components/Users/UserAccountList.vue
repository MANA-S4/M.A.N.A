<template>
    <div>
        <div class="page-header">
            <h1>Mon compte</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-warning" :to="`users/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter une tâche</router-link>
            </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Nom</th>
                    <th>Prénom</th>
                    <th>Email</th>
                    <th>Date de Naissance</th>
                    <th>Options</th>       
                </tr>
            </thead>

            <tbody>
                <tr v-if="userAccountList.length == 0">
                    <td colspan="6" class="text-center">Il n'y a actuellement aucune tâche.</td>
                </tr>

                <tr v-for="i of list">
                    <td>{{ i.lastName }}</td>
                    <td>{{ i.firstName }}</td>
                    <td>{{ i.email }}</td>
                    <td>{{ i.birthDate }}</td>
                    <td>
                        <router-link :to="`users/edit/${i.userId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>    
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
                list: []
            }
        },

        created() {
            this.refreshUserAccountList();
        },

        computed: {
            ...mapGetters(['userAccountList']),

            list: function() {
                let user =  [];
                let i = 0;
                
                for(i = 0; i < this.userAccountList.length; i++) {
                    if (this.userAccountList[i].userName.includes(this.search)) {
                        user.push(this.userAccountList[i]);
                    }
                }
                return user;
            }
        },

        methods: {
            ...mapActions(['refreshUserAccountList'])
        }
    }
</script>

<style lang="less">
.panel-body text-right {
    text-align: left;
    background-color: #00b050; 
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