<template>
    <div>
        <div class="page-header">
            <h1>Gestion des tâches</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`tasks/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter une tâche</router-link>
            </div>

        </div>
            <div class="card is-fullwidth">
                <header class="card-header">
                    <p class="card-header-title">
                        Component
                    </p>
                    <a class="card-header-icon">
                        <i class="fa fa-angle-down"></i>
                    </a>
                </header>

                <div class="card-content">
                    <div class="content">
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus nec iaculis mauris.
                    <br>
                    <small>11:09 PM - 1 Jan 2016</small>
                </div>
            </div>
                <footer class="card-footer">
                <a class="card-footer-item">Save</a>
                <a class="card-footer-item">Edit</a>
                <a class="card-footer-item">Delete</a>
                </footer>
        </div>


        <!--<table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>ContactId</th>
                    <th>Email</th>
                    <th>RelationType</th>
                    <th>Options</th>       
                </tr>
            </thead>-->

            <tbody>
                <!--<tr v-if="contactList.length == 0">
                    <td colspan="5" class="text-center">Il n'y a actuellement aucun contact.</td>
                </tr>-->

                <!--<tr v-for="i of list">
                    <td>{{ i.contactId }}</td>
                    <td>{{ i.email }}</td>
                    <td>{{ i.relationType }}</td>
                    <td>
                        <router-link :to="`contacts/edit/${i.contactId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteContact(i.contactId)"><i class="glyphicon glyphicon-remove"></i></a>          
                    </td>
                </tr>

                <tr>
                    <a href="#" @click="previousList(i.contactId)"><i class="glyphicon glyphicon-menu-left"></i></a> 
                    <a href="#" @click="nextList(i.contactId)"><i class="glyphicon glyphicon-menu-right"></i></a>
                </tr>-->
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

</style>