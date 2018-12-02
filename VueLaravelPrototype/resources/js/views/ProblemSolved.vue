<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Problem Solved</h2>
        <b-form>
            <b-form-group id="specialistIdInputGroup" label="Solved by:" label-for="specialistIdInput">
                <b-form-input id="specialistIdInput" type="number" v-model="form.specialistId" placeholder="Enter specialist ID"></b-form-input>
            </b-form-group>

            <b-form-group id="dateTimeInputGroup" label="Date and time:" label-for="dateTimeInput">
                <b-form-input id="dateTimeInput" type="date" v-model="form.dateTime"></b-form-input>
            </b-form-group>

            <b-form-group id="solutionInputGroup" label="Solution:" label-for="solutionInput">
                <b-form-textarea id="solutionInput" v-model="form.solution"></b-form-textarea>
            </b-form-group>

            <b-button type="submit" variant="primary">Submit</b-button>
        </b-form>
    </b-card>
</template>

<script>
export default {
    data() {
        return {
            form: {
                problemId: this.$route.params.problemId,
                specialistId: "",
                dateTime: "",
                solution: ""
            }
        };
    }
};
</script>
