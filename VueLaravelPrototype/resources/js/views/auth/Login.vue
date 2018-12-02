<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Sign In</h2>
        <b-form>
            <b-form-group id="userIdInputGroup" label="User ID:" label-for="userIdInput">
                <b-form-input id="userIdInput" type="text" v-model="form.userId"></b-form-input>
            </b-form-group>

            <b-form-group id="passwordInputGroup" label="Password:" label-for="passwordInput">
                <b-form-input id="passwordInput" type="password" v-model="form.password"></b-form-input>
            </b-form-group>


            <b-button type="submit" variant="primary" :to="{ name: 'dashboard' }">Sign In</b-button>
        </b-form>
    </b-card>
</template>

<script>
export default {
    data() {
        return {
            form: {
                userId: "",
                password: ""
            }
        }
    }
}
</script>
