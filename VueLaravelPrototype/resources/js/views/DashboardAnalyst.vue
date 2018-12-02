<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Welcome Analyst</h2>
        <p id="" class="pt-0"><b>Ongoing Problems:</b> {{problem.outgoing }}</p>
        <p id="" class="pt-0"><b>Most Common Problem Type:</b> {{problem.mostCommon }}</p>
        <p id="" class="pt-0"><b>Problems Reported This Week:</b> {{ problem.reported.week }}</p>
        <p id="" class="pt-0"><b>Total Problems Reported:</b> {{ problem.reported.total }}</p>
        <b-card>
            <b-form-group id="problemTypeInputGroup" label="Problem Type:" label-for="problemTypeInput">
                <b-form-select id="problemTypeInput" v-model="form.problemType.selected" :options="form.problemType.options" />
            </b-form-group>
            <b-form-group id="specialistInputGroup" label="Specialist:" label-for="specialistInput">
                <b-form-select id="specialistInput" v-model="form.specialist.selected" :options="form.specialist.options" />
            </b-form-group>
            <b-table :items="problemsTableFiltered.items" :fields="problemsTableFiltered.fields">
                <template slot="show_details" slot-scope="row">
                    <!-- we use @click.stop here to prevent emitting of a 'row-clicked' event  -->
                    <b-button size="sm" @click.stop="row.toggleDetails" class="mr-2">
                        {{ row.detailsShowing ? 'Hide' : 'Show'}} Details
                    </b-button>
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
                        <template v-else-if="row.item.software">
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

    </b-card>
</template>

<script>
export default {
    data() {
        return {
            form: {
                specialist: {
                    selected: null,
                    options: [
                        { value: null, text: "All specialists" },
                        { value: "Elon Musk", text: "Elon Musk" },
                        { value: "Jenna Fischer", text: "Jenna Fischer" },
                        { value: "Jim Davis", text: "Jim Davis" }
                    ]
                },
                problemType: {
                    selected: null,
                    options: [
                        { value: null, text: "All problem types" },
                        { value: "hardware", text: "Hardware" },
                        { value: "software", text: "Software" },
                        { value: "network", text: "Network" }
                    ]
                }
            },
            problem: {
                outgoing: 5,
                mostCommon: "hardware",
                reported: {
                    week: 15,
                    total: 102
                }
            },
            problemsTable: {
                fields: ["problem_type", "specialist", "notes", "show_details"],
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
                        specialist: "Elon Musk",
                        notes: "PC has water damage"
                    },
                    {
                        problemID: "543234",
                        callerID: "123445",
                        hardware: {
                            serialNo: "aawe4t43",
                            type: "d93mdf5",
                            make: "Dell"
                        },
                        problem_type: "hardware",
                        specialist: "Elon Musk",
                        notes: "PCs in the financial dept catching fire"
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
                        specialist: "Jenna Fischer",
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
                        specialist: "Jim Davis",
                        notes: "Image keeps moving my text the wrong way"
                    },
                    {
                        problemID: "765345",
                        callerID: "445633",
                        problem_type: "network",
                        specialist: "Dominic Howard",
                        notes: "Image keeps moving text the wrong way"
                    }
                ]
            }
        };
    },
    computed: {
        problemsTableFiltered() {
            return {
                fields: this.problemsTable.fields,
                items: this.problemsTable.items.filter(
                    item =>
                        (!this.form.problemType.selected ||
                            item.problem_type ===
                                this.form.problemType.selected) &&
                        (!this.form.specialist.selected ||
                            item.specialist === this.form.specialist.selected)
                )
            };
        }
    }
};
</script>
