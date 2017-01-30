<template>
    <div>
        <div class="page-header">
            <h1 v-if="mode == 'create'">Créer un évènement</h1>
            <h1 v-else>Editer un évènement</h1>
        </div>

        <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group">
                <label class="required">Nom de l'évènement</label>
                <input type="text" v-model="item.eventName" class="form-control" required>
            </div>

            <div class="form-group">
                <label class="required">Date de l'évènement</label>
                <input type="date" v-model="item.eventDate" class="form-control" required>
            </div>

            <div class="form-group">
                <label>Membres additionnels</label>
                <input type="text" v-model="item.members" class="form-control">
            </div>

            <div class="form-group">
                <label>Privé</label>
                <input type="checkbox" v-model="item.isPrivate" class="form-control">
            </div>

            <button type="submit" class="btn btn-warning">Enregistrer</button>
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
            ...mapGetters(['eventsList'])
        },

        created() {
            this.item = {};
            this.mode = this.$route.params.mode;
            this.id = this.$route.params.id;

            if(this.mode == 'edit') {
                let item = this.eventsList.find(x => x.eventId == this.id);

                if(!item) this.$router.replace('/calendars');

                this.item = { ...item }
            }
        },

        methods: {
            ...mapActions(['createEvent', 'updateEvent']),

            onSubmit: async function(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.eventName) errors.push("Nom de l'évènement incorrect")
                if(!this.item.eventDate) errors.push("Date incorrect")

                this.errors = errors;

                if(errors.length == 0) {
                    var result = null;

                    if(this.mode == 'create') {
                        result = await this.createEvent(this.item);
                    }
                    else {
                        result = await this.updateEvent(this.item);
                    }

                    if(result != null) this.$router.replace('/calendars');
                }
            }
        }
    }
</script>

<style lang="less">

</style>