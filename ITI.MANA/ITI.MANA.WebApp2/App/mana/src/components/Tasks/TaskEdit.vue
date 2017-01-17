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
                <label class="required">Name</label>
                <input type="text" v-model="item.taskName" class="form-control" required>
            </div>

            <div class="form-group">
                <label>Date</label>
                <input type="date" v-model="item.taskDate" class="form-control">
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

                if(!this.item.taskName) errors.push("TaskName")

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
.form-group {
    text-align: left;
    color: black;
}
</style>