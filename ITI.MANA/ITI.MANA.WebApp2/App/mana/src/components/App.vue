<template>
    <div id="app">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">

                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#iti-navbar-collapse" aria-expanded="false">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <router-link class="navbar-brand" to="/">M.A.N.A</router-link>
                </div>

                <div class="collapse navbar-collapse" id="iti-navbar-collapse" v-if="auth.isConnected">
                    <ul class="nav navbar-nav">
                        <li><router-link to="/calendars"><i class="glyphicon glyphicon-calendar"></i> Gestion du calendrier</router-link></li>
                        <li><router-link to="/tasks"><i class="glyphicon glyphicon-list-alt"></i> Gestion des tâches</router-link></li>
                        <li><router-link to="/contacts"><i class="glyphicon glyphicon-user"></i> Gestion des contacts</router-link></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">{{ auth.email }} <span class="caret"></span></a>
                            <ul class="dropdown-menu">
                                <li><router-link to="/users">Profil</router-link></li>
                                <li><router-link to="/logout">Se déconnecter</router-link></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="progress" v-show="isLoading">
                <div class="progress-bar progress-bar-striped active" role="progressbar" style="width: 100%"></div>
            </div>
        </nav>

        <div class="container">
            <router-view class="child"></router-view>
        </div>

  </div>
</template>

<script>

import AuthService from '../services/AuthService'
import {mapGetters, mapActions } from 'vuex'
import '../directives/requiredProviders'
import $ from 'jquery'

export default {
    computed: {
        auth: () => AuthService,
        ...mapGetters(['isLoading'])
    }
}

</script>

<style lang="less" scoped>
    .progress {
        margin: 0px;
        padding: 0px;
        height: 5px;
    }

    a.router-link-active {
        font-weight: bold;
    }
</style>

<style lang="less">
@import "~bootstrap/dist/css/bootstrap.min.css";
@import "../styles/cover.css";
@import "../styles/one-page-wonder.css";
</style>