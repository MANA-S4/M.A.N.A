<template>
    <div>
        <div class="text-center">
            <div class="page-header">
                <h1>Bienvenue sur MANA</h1>
            </div>

          <button type="button" @click="login('Google')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Se connecter via Google</button>
          <button type="button" @click="login('Microsoft')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-microsoft" aria-hidden="true"></i> Se connecter via Microsoft</button>
          <button type="button" @click="login('Base')" class="btn btn-lg btn-block btn-default">Se connecter via M.A.N.A</button>

        </div>
    </div>
</template>

<script>
    import AuthService from "../services/AuthService"
    import Vue from 'vue'
    import $ from 'jquery'

    export default {
        data() {
            return {
                endpoint: null
            }
        },

        created() {
            AuthService.registerAuthenticatedCallback(this.onAuthenticated);
        }

        beforeDestroy() {
            AuthService.removeAuthenticatedCallback(this.onAuthenticated);
        }

        methods: {
            login(provider) {
                AuthService.login(provider);
            }

            onAuthenticated() {
                this.$router.replace('/');
            }
        }
    }

</script>

<style lang="less">
    iframe {
        width: 100%;
        height: 600px;
    }
</style>
