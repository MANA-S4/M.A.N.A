<template>
    <div>
        <div class="page-header">
            <h1 v-if="mode == 'create'">Créer une tâche</h1>
            <h1 v-else>Editer une tâche</h1>
        </div>

        <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group">
                <label class="required">Objet</label>
                <input type="text" v-model="item.object" class="form-control" required>
            </div>

            <div class="form-group">
                <label class="required">Qui</label>
                <select v-model="item.attribute" class="form-control" required>
                    <option>Père</option>
                    <option>Mère</option>
                    <option>Frère</option>
                    <option>Soeur</option>
                    <option>Colocataire</option>
                </select>
            </div>

            <button type="submit" class="btn btn-primary">Sauvegarder</button>
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
            ...mapGetters(['taskList'])
        },

        created() {
            this.item = {};
            this.mode = this.$route.params.mode;
            this.id = this.$route.params.id;

            if(this.mode == 'edit') {
                let item = this.taskList.find(x => x.taskId == this.id);

                if(!item) this.$router.replace('/tasks');

                this.item = { ...item }
            }
        },

        methods: {
            ...mapActions(['createTask', 'updateTask']),

            onSubmit: async function(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.object) errors.push("Objet")
                if(!this.item.attribute) errors.push("Qui")

                this.errors = errors;

                if(errors.length == 0) {
                    var result = null;

                    if(this.mode == 'create') {
                        result = await this.createTask(this.item);
                    }
                    else {
                        result = await this.updateTask(this.item);
                    }

                    if(result != null) this.$router.replace('/tasks');
                }
            }
        }
    }
</script>

<style lang="less">

</style>