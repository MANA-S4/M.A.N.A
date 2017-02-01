<template>
    <div>
        <div class="page-header">
            <h1 v-if="mode == 'create'">Créer un contact</h1>
            <h1 v-else>Editer un contact</h1>
        </div>

        <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group" v-if="mode == 'create'">
                <label class="required">Email</label>
                <input type="text" v-model="item.email" class="form-control" required>
            </div>
            
            <div class="form-group" v-else><p>{{ item.email }}</p></div>

            <div class="form-group">
                <label class="required">Lien</label>
                <select v-model="item.relationType" class="form-control" required>
                    <option>Père</option>
                    <option>Mère</option>
                    <option>Enfant</option>
                    <option>Frère/Soeur</option>
                    <option>Colocataire</option>
                </select>
            </div>

            <button type="submit" class="btn btn-warning">Sauvegarder</button>
        </form>
    </div>
</template>

<script>
    import { mapGetters, mapActions } from 'vuex'

    export default {
        data () {
            return {
                item: {},
                mode: null,
                id: null,
                errors: []
            }
        },

        computed: {
            ...mapGetters(['contactList'])
        },

        created() {
            this.item = {};
            this.mode = this.$route.params.mode;
            this.id = this.$route.params.id;

            if(this.mode == 'edit') {
                let item = this.contactList.find(x => x.contactId == this.id);

                if(!item) this.$router.replace('/contacts');

                this.item = { ...item }
            }
        },

        methods: {
            ...mapActions(['createContact', 'updateContact']),

            onSubmit: async function(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.email) errors.push("Email")
                if(!this.item.relationType) errors.push("RelationType")

                this.errors = errors;

                if(errors.length == 0) {
                    var result = null;

                    if(this.mode == 'create') {
                        result = await this.createContact(this.item);
                    }
                    else {
                        result = await this.updateContact(this.item);
                    }

                    if(result != null) this.$router.replace('/contacts');
                }
            }
        }
    }
</script>

<style lang="less">

</style>