<template>
    <b-card title="Software" class="mb-3">
        <b-form>
            <b-form-group id="nameInputGroup" label="Name:" label-for="nameInput">
                <b-form-input id="nameInput" type="text" v-model="name" v-on:input="$emit('update:name', name)"></b-form-input>
            </b-form-group>

            <b-form-group id="descriptionInputGroup" label="Description:" label-for="descriptionInput">
                <b-form-input id="descriptionInput" type="text" v-model="description" v-on:input="$emit('update:description', description)"></b-form-input>
            </b-form-group>
        </b-form>
    </b-card>
</template>

<script>
export default {
    props: {
        name: String,
        description: String
    }
};
</script>