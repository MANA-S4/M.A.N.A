<template>
    <div>
        <div class="page-header">
            <h1>Profil</h1>
        </div>

        <div class="panel">
            <thead>
                <h4><p>Nom :</p></h4>
                <p>{{userAccountList.lastName}}</p><br>
                <h4><p>Prénom :</p></h4>
                <p>{{userAccountList.firstName}}</p><br>
                <h4><p>Email :</p></h4>
                <p>{{userAccountList.email}}</p><br>
                <h4><p>Date de Naissance :</p></h4>
                <p>{{userAccountList.birthDate}}</p><br>
                <router-link :to="`users/edit`"><button type="button" class="btn btn-success">Modifier</button></router-link>
            </thead>

            <tbody>
                <tr v-if="userAccountList.length == 0">
                    <td colspan="6" class="text-center">Il n'y a actuellement aucune information.</td>
                </tr>
            </tbody>
        </div>
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
a {
    text-align: left;
    color: black;
}
</style>