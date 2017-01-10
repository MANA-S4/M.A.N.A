<template>
    <div>
        <div class="page-header">
            <h1>Gestion des contacts</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-warning" :to="`contacts/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un contact</router-link>
            </div>
        </div>

        <!-- For the search, take a v-model -->
        <div class="panel panel-default">
            <input type="text" name="search" v-model="search" id="search" placeholder="Rechercher"/>
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

                <tr v-for="i of list"> <!-- modification de contactList en list pour afficher les contacts en fonction de ma recherche -->
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.email }}</td>
                    <td>{{ i.relationType }}</td>
                    <td>
                        <router-link :to="`contacts/edit/${i.contactId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
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
            ...mapGetters(['contactList']),

            /// ---------------------- Question 4 -------------------------------------
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
            /// ------------------------------------------------------------------------   
        },

        methods: {
            ...mapActions(['refreshContactList' ,'deleteContact'])
        }
    }
</script>

<style lang="less">
tr {
    text-align: left;
}
.panel {
    text-align: left;
    background-color: #00b050;
}
.panel-body {
    background-color: #00b050;
}
.table {
    background-color: rgba(17,42,13,.5);
}
.glyphicon-remove {
    color: black;
}
.glyphicon-pencil {
    color: black;
}
</style>