<template>
    <div>
        <div class="page-header">
            <h1 v-if="mode == 'create'">Créer une tâche</h1>
            <h1 v-else>Editer mon compte</h1>
        </div>

        <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group">
                <label class="required">Nom</label>
                <input type="text" v-model="item.lastName" class="form-control" required>
            </div>

            <div class="form-group">
                <label class="required">Prénom</label>
                <input type="text" v-model="item.firstName" class="form-control" required>
            </div>

            <div class="form-group">
                <label class="required">Email</label>
                <input type="text" v-model="item.email" class="form-control" required>
            </div>

            <div class="form-group">
                <label>Date</label>
                <input type="date" v-model="item.birthDate" class="form-control">
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
            ...mapGetters(['userAccountList'])
        },

        created() {
            this.item = {};
            this.mode = this.$route.params.mode;
            this.id = this.$route.params.id;

            if(this.mode == 'edit') {
                let item = this.userAccountList;

                if(!item) this.$router.replace('/users');

                this.item = { ...item }
            }
        },

        methods: {
            ...mapActions(['updateUser']),

            onSubmit: async function(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.lastName) errors.push("Nom")
                if(!this.item.firstName) errors.push("Prénom")
                if(!this.item.email) errors.push("Email")
                if(!this.item.birthDate) errors.push("Date de Naissance")

                this.errors = errors;

                if(errors.length == 0) {
                    var result = null;

                    if(this.mode == 'create') {
                        result = await this.createTask(this.item);
                    }
                    else {
                        result = await this.updateUser(this.item);
                    }

                    if(result != null) this.$router.replace('/users');
                }
            }
        }
    }
</script>

<style lang="less">
.form-group {
    text-align: left;
    color: black;
}
</style>