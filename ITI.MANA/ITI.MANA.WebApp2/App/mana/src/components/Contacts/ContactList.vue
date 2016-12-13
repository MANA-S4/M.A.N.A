<template>
    <div>
        <div class="page-header">
            <h1>Gestion des contacts</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`contacts/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un contact</router-link>
            </div>
        </div>

        <!-- Pour la recherche, prend en compte un v-model -->
        <div class="panel panel-default">
            <input type="text" name="search" v-model="search" id="search" placeholder="Rechercher"/>
        </div>
        <!-- Fin de la div -->

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nom</th>
                    <th>Prénom</th>
                    <th>Date de naissance</th>
                    <!--<th>Classe</th> permet de faire un tableau-->
                    <th>Options</th>       
                </tr>
            </thead>

            <tbody>
                <tr v-if="contactList.length == 0">
                    <td colspan="5" class="text-center">Il n'y a actuellement aucun contact.</td>
                </tr>

                <tr v-for="i of list"> <!-- modification de contactList en list pour afficher les contacts en fonction de ma recherche -->
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.lastName }}</td>
                    <td>{{ i.firstName }}</td>
                    <td>{{ i.birthDate }}</td>
                    <!-- <td>{{ i.classId }}</td> ajout class id -->
                    <td>
                        <router-link :to="`contacts/edit/${i.contactId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="followContact(i.contactId)"><i class="glyphicon glyphicon-plus"></i></a> <!-- rajouter followContact et je pense que ça va être un nouveau routeur-->
                        <a href="#" @click="deleteContact(i.contactId)"><i class="glyphicon glyphicon-remove"></i></a>          
                    </td>
                </tr>

                <tr>
                    <a href="#" @click="previousList(i.contactId)"><i class="glyphicon glyphicon-menu-left"></i></a> <!--Creer previous -->
                    <a href="#" @click="nextList(i.contactId)"><i class="glyphicon glyphicon-menu-right"></i></a> <!--Creer next-->
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
            this.refreshContactList();
        },

        computed: {
            ...mapGetters(['ContactList']),

            /// ---------------------- Question 4 -------------------------------------
            list: function() {
                let contact =  [];
                let i = 0;
                
                for(i = 0; i < this.contactList.length; i++) {
                    if (this.contactList[i].firstName.includes(this.search) || this.contactList[i].lastName.includes(this.search)) {
                        contact.push(this.contactList[i]);
                    }
                }
                return contact;
            }
            /// ------------------------------------------------------------------------   
        },

        methods: {
            ...mapActions(['refreshContactList' ,'deleteContact'])
        }
    }
</script>

<style lang="less">

</style>