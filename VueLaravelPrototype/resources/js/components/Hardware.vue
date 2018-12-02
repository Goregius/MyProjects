<template>
    <b-card title="Hardware" class="mb-3">
        <!-- <h3 slot="header" class="mb-0">Hardware</h3> -->
        <b-form>
            <b-form-group id="makeInputGroup" label="Make:" label-for="makeInput">
                <b-form-input id="makeInput" type="text" v-model="make" v-on:input="$emit('update:make', make)"></b-form-input>
            </b-form-group>
            
            <b-form-group id="typeInputGroup" label="Type:" label-for="typeInput">
                <b-form-input id="typeInput" type="text" v-model="type" v-on:input="$emit('update:type', type)"></b-form-input>
            </b-form-group>

            <b-form-group id="serialNoInputGroup" label="Serial No:" label-for="serialNoInput">
                <b-form-input id="serialNoInput" type="number" v-model="serialNo" v-on:input="$emit('update:serialNo', serialNo)"></b-form-input>
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
        serialNo: String,
        type: String,
        make: String,
        description: String
    }
};
</script>
