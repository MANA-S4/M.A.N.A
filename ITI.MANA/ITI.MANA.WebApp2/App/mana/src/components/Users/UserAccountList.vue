<template>
    <div>
        <div class="page-header">
            <h1>Mon compte</h1>
        </div>

        <div class="panel panel-default">
            <table class="table table-striped table-hover table-bordered">
                <thead>
                    <a>Nom : {{userAccountList.lastName}}</a><br><br>
                    <a>Prénom : {{userAccountList.firstName}}</a><br><br>
                    <a>Email : {{userAccountList.email}}</a><br><br>
                    <a>Date de Naissance : {{userAccountList.birthDate}}</a><br><br>
                    <router-link :to="`users/edit`"><button type="button" class="btn btn-success">Modifier</button></router-link>
                </thead>

                <tbody>
                    <tr v-if="userAccountList.length == 0">
                        <td colspan="6" class="text-center">Il n'y a actuellement aucune information.</td>
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

            list: function () {
                let user = [];
                let i = 0;

                for (i = 0; i < this.userAccountList.length; i++) {
                    if (this.userAccountList[i].lastName.includes(this.search)) {
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