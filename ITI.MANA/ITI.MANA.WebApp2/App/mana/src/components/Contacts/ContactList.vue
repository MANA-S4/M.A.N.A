<template>
    <div>
        <div class="page-header">
            <h1>Gestion des contacts</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-success" :to="`contacts/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un contact</router-link>
            </div>
        </div>

        <!-- For the search, take a v-model -->
        <div class="panel panel-default">
            <input type="text" name="search" v-model="search" id="search" placeholder="Rechercher" /> <i class="glyphicon glyphicon-search"></i> 
        </div>
        <!-- End of search div -->

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>ContactId</th>
                    <th>Email</th>
                    <th>RelationType</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="contactList.length == 0">
                    <td colspan="5" class="text-center">Il n'y a actuellement aucun contact.</td>
                </tr>

                <tr v-for="i of list">
                    <!-- modification de contactList en list pour afficher les contacts en fonction de ma recherche -->
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.email }}</td>
                    <td>{{ i.relationType }}</td>
                    <td>
                        <router-link :to="`contacts/edit/${i.contactId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#"><i class="glyphicon glyphicon-remove" id="show-modal" @click="openDeletePrompt(i.contactId)"></i></a> <!-- To open the popup-->
                    </td>
                </tr>

                <tr>
                    <a href="#" @click="previousList(i.contactId)"><i class="glyphicon glyphicon-menu-left"></i></a>
                    <a href="#" @click="nextList(i.contactId)"><i class="glyphicon glyphicon-menu-right"></i></a>
                </tr>

            </tbody>         
        </table>
        
        <!-- If user click on "Non" popup close -->
        <delete-prompt v-if="showModal" @close="showModal = false" v-bind:contactId="deletingContactId">
            <h3 slot="header">Suppression</h3>
        </delete-prompt>
        <!-- End -->

    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'
    import DeletePrompt from './DeletePrompt.vue' // Import the vue DeletePrompt

    export default {
        data() {
            return {
                // Define popup false to default
                template: '#modal-template',
                showModal: false,
                deletingContactId: 0,
                // End popup
                search: '',
                list: []
            }
        },

        // Call vue DeletePrompt
        components: {
            DeletePrompt
        },
        // End

        created() {
            this.refreshContactList();
        },

        computed: {
            ...mapGetters(['contactList']),

            list: function() {
                let contact =  [];
                let i = 0;
                
                for(i = 0; i < this.contactList.length; i++) {
                    if (this.contactList[i].email.includes(this.search)) {
                        contact.push(this.contactList[i]);
                    }
                }
                return contact;
            }   
        },

        methods: {
            ...mapActions(['refreshContactList' ,'deleteContact']),

            openDeletePrompt(contactId) {
                this.deletingContactId = contactId;
                this.showModal = true;
            }
        }
    }
</script>

<style lang="less">
    tr {
        text-align: left;
    }
    
    .panel {
        text-align: left;
        background-color: #bdc3c7;
    }
    
    .panel-body {
        background-color: #bdc3c7;
    }
    
    .table {
        background-color: rgba(17, 42, 13, .5);
    }
    
    .glyphicon-remove {
        color: black;
    }
    
    .glyphicon-pencil {
        color: black;
    }

    header {
        color: #27ae60,
    }
</style>