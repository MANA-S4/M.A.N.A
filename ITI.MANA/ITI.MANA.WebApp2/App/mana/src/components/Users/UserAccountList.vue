<template>
    <div>
        <div class="page-header">
            <h1>Profil</h1>
        </div>

        <div class="panel">
            <thead>
                <h3><p>Nom :</p></h3>
                <p v-if="userAccountList.lastName == null">NC</p>
                <p v-else>{{userAccountList.lastName}}</p><br>
                <h3><p>Prénom :</p></h3>
                <p v-if="userAccountList.firstName == null">NC</p>
                <p v-else>{{userAccountList.firstName}}</p><br>
                <h3><p>Email :</p></h3>
                <p>{{userAccountList.email}}</p><br>
                <h3><p>Date de Naissance :</p></h3>
                <p v-if="userAccountList.birthDate == '0001-01-01T00:00:00'"> NC </p>
                <p v-else>{{userAccountList.birthDate | showdate}}</p><br>
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
    let showdate = function(value){
                return value.slice(0,10)
            }

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

        filters:{
            showdate
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