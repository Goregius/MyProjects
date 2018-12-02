<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Sign In</h2>
        <b-form>
            <b-form-group id="problemidInputGroup" label="Operator ID:" label-for="problemidInput">
                <b-form-input id="problemidInput" type="text" v-model="form.operatorId"></b-form-input>
            </b-form-group>

            <b-form-group id="reporteridInputGroup" label="Password:" label-for="reporteridInput">
                <b-form-input id="reporteridInput" type="password" v-model="form.password"></b-form-input>
            </b-form-group>


            <b-button type="submit" variant="primary">Sign In</b-button>
        </b-form>
    </b-card>
</template>

<script>
export default {
    data() {
        return {
            form: {
                operatorId: "",
                password: ""
            }
        }
    }
}
</script>
