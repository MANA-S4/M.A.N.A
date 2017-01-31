<template>
    <div>
        <div class="page-header">
            <h1>Gestion des contacts</h1>
        </div>

        <div>
            <div class="panel-body text-right">
                <router-link class="btn btn-success" :to="`contacts/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un contact</router-link>
            </div>
        </div>

        <!-- For the search, take a v-model -->
        <div class="panel">
            <input type="text" name="search" v-model="search" id="search" placeholder="Rechercher" /> <i class="glyphicon glyphicon-search"></i> 
        </div>
        <!-- End of search div -->

        <div class="box" v-for="i of list">
            <article class="media">
                <div class="media-left">
                    <figure class="image is-64x64">
                        <img src="../../pictures/user.png">
                    </figure>
                </div>
                <div class="media-content">
                    <div class="content">
                        <p>
                            <strong>{{i.email}}</strong><br>
                            <br> {{i.relationType}}
                        </p>
                    </div>
                </div>
            </article>
        </div>
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

            list: function () {
                let contact = [];
                let i = 0;

                for (i = 0; i < this.contactList.length; i++) {
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