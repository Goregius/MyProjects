<template>
    <b-form-group id="editProblemContainer" class="mb-2">
        <h3>Edit Old Problem</h3>
        <b-form-input class="mia-block-semi mb-2" id="problemidInput" type="text" v-model="form.problemId" placeholder="Enter Problem Id" required>
        </b-form-input>
        <b-button @click="edit" class="mia-block-semi" id="editProblemBtn" block variant="primary">Edit</b-button>
        <b-button @click="solved" class="mia-block-semi" id="solvedProblemBtn" block variant="primary">Solved</b-button>
    </b-form-group>
</template>

<script>
export default {
    data() {
        return {
            form: {
                problemId: ""
            }
        };
    },
    computed: {
        problemIdValid() {
            if (this.form.problemId == "") {
                return {
                    valid: false,
                    message: "The problem ID can't be empty"
                };
            }
            else if (!this.form.problemId.match(/^[0-9]{6}$/)) {
                return {
                    valid: false,
                    message: "The problem ID must be a 6 digit number"
                }
            }
            return { valid: true };
        }
    },
    methods: {
        edit() {
            if (this.problemIdValid.valid) {
                this.$router.push({ name: 'problemedit', params: { 'problemId': this.form.problemId }});
            } else {
                alert(this.problemIdValid.message);
            }
        },
        solved() {
            if (this.problemIdValid.valid) {
                this.$router.push({ name: 'problemsolved', params: { 'problemId': this.form.problemId }});
            } else {
                alert(this.problemIdValid.message);
            }
        }
    }
};
</script>