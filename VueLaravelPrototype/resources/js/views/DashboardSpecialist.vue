<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Welcome Specialist</h2>
        <b-table :items="problemsTable.items" :fields="problemsTable.fields">
            <template slot="show_details" slot-scope="row">
                <!-- we use @click.stop here to prevent emitting of a 'row-clicked' event  -->
                <b-button size="sm" @click.stop="row.toggleDetails" class="mr-2">
                    {{ row.detailsShowing ? 'Hide' : 'Show'}} Details
                </b-button>
                <!-- In some circumstances you may need to use @click.native.stop instead -->
                <!-- As `row.showDetails` is one-way, we call the toggleDetails function on @change -->
                <!-- <b-form-checkbox @change="row.toggleDetails" v-model="row.detailsShowing">
                    Show Details?
                </b-form-checkbox> -->
            </template>
            <template slot="row-details" slot-scope="row">
                <b-card>
                    <b-row class="mb-2">
                        <b-col sm="3" class="text-sm-right"><b>Problem ID:</b></b-col>
                        <b-col>{{ row.item.problemID }}</b-col>
                    </b-row>
                    <b-row class="mb-2">
                        <b-col sm="3" class="text-sm-right"><b>Caller ID:</b></b-col>
                        <b-col>{{ row.item.callerID }}</b-col>
                    </b-row>
                    <template v-if="row.item.hardware">
                        <b-row class="mb-2">
                            <b-col sm="3" class="text-sm-right"><b>Make:</b></b-col>
                            <b-col>{{ row.item.hardware.make }}</b-col>
                        </b-row>
                        <b-row class="mb-2">
                            <b-col sm="3" class="text-sm-right"><b>Type:</b></b-col>
                            <b-col>{{ row.item.hardware.type }}</b-col>
                        </b-row>
                        <b-row class="mb-2">
                            <b-col sm="3" class="text-sm-right"><b>Serial No:</b></b-col>
                            <b-col>{{ row.item.hardware.serialNo }}</b-col>
                        </b-row>
                    </template>
                    <template v-if="row.item.software">
                        <b-row class="mb-2">
                            <b-col sm="3" class="text-sm-right"><b>Name:</b></b-col>
                            <b-col>{{ row.item.software.name }}</b-col>
                        </b-row>
                    </template>
                    <b-button size="sm" @click="row.toggleDetails">Hide Details</b-button>
                </b-card>
            </template>
        </b-table>
    </b-card>
</template>

<script>
export default {
    data() {
        return {
            problemsTable: {
                fields: ["problem_type", "notes", "show_details"],
                items: [
                    {
                        problemID: "123123",
                        callerID: "243234",
                        hardware: {
                            serialNo: "r8398a08",
                            type: "axbty",
                            make: "Dell"
                        },
                        problem_type: "hardware",
                        notes: "PC has water damage"
                    },
                    {
                        problemID: "543234",
                        callerID: "654234",
                        hardware: {
                            serialNo: "8fh40983h",
                            type: "sdo78",
                            make: "BenQ"
                        },
                        problem_type: "hardware",
                        notes:
                            "Monitor has dead pixels on the left side of the screen"
                    },
                    {
                        problemID: "435643",
                        callerID: "234555",
                        software: {
                            name: "Microsoft Word 2016"
                        },
                        problem_type: "software",
                        notes: "Image keeps moving my text the wrong way"
                    }
                ]
            }
        };
    }
};
</script>
