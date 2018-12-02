<template>
    <b-card header-tag="header">
        <h2 slot="header" class="mb-0">Edit Problem</h2>
        <b-form @submit.prevent>
            <b-form-group id="callInputGroup" label="" label-for="problemidInput">
                <b-table small striped outlined hover :items="form.callers" :class="{'mb-0': form.callers.length === 0 }"></b-table>
                <b-button variant="primary" v-b-modal.callModal>Add Call</b-button>
            </b-form-group>

            <b-modal id="callModal" @ok="addCall" ok-title="Add Call" hide-header ref="callModal">
                <b-form-group id="callerInputGroup" label="Caller:" label-for="callerInput">
                    <b-form-input id="callerInput" type="text" v-model="form.call.caller" name="caller" v-validate="'required|alpha_spaces'" data-vv-delay="500" data-vv-scope="call" :class="{'is-invalid': errors.has('call.caller') }">
                    </b-form-input>
                    <span v-show="errors.has('call.caller')" class="text-danger">{{ errors.first('call.caller') }}</span>
                </b-form-group>

                <b-form-group id="operatorInputGroup" label="Operator:" label-for="operatorInput">
                    <b-form-input id="operatorInput" type="text" v-model="form.call.operator" name="operator" v-validate="'required|alpha_spaces'" data-vv-delay="500" data-vv-scope="call" :class="{'is-invalid': errors.has('call.operator') }">
                    </b-form-input>
                    <span v-show="errors.has('call.operator')" class="text-danger">{{ errors.first('call.operator') }}</span>
                </b-form-group>

                <b-form-group id="dateInputGroup" label="Date:" label-for="dateInput">
                    <b-form-input id="dateInput" type="date" v-model="form.call.date" name="date" v-validate="'required'" data-vv-scope="call" data-vv-delay="500" :class="{'is-invalid': errors.has('call.date') }">
                    </b-form-input>
                    <span v-show="errors.has('call.date')" class="text-danger">{{ errors.first('call.date') }}</span>
                </b-form-group>

                <b-form-group id="timeInputGroup" label="Time:" label-for="timeInput">
                    <b-form-input id="timeInput" type="time" v-model="form.call.time" name="time" v-validate="'required'" data-vv-scope="call" data-vv-delay="500" :class="{'is-invalid': errors.has('call.time') }">
                    </b-form-input>
                    <span v-show="errors.has('call.time')" class="text-danger">{{ errors.first('call.time') }}</span>
                </b-form-group>

                <b-form-group id="reasonInputGroup" label="Reason:" label-for="reasonInput">
                    <b-form-textarea id="reasonInput" v-model="form.call.reason" :rows="3" name="reason" v-validate="'required'" data-vv-scope="call" data-vv-delay="500" :class="{'is-invalid': errors.has('call.reason') }">
                    </b-form-textarea>
                    <span v-show="errors.has('call.reason')" class="text-danger">{{ errors.first('call.reason') }}</span>
                </b-form-group>
            </b-modal>

            <b-form-group id="problemidInputGroup" label="Problem ID:" label-for="problemidInput">
                <b-form-input id="problemidInput" type="text" v-model="form.problemId" v-validate="'required'" disabled>
                </b-form-input>
            </b-form-group>

            <!-- <b-form-group id="reporteridInputGroup" label="Reporter ID:" label-for="reporteridInput">
                <b-form-input id="reporteridInput" type="text" v-model="form.reporterId" v-validate="'required'" placeholder="Enter reporter ID">
                </b-form-input>
            </b-form-group> -->

            <b-form-group id="problemTypeInputGroup" label="Problem Type:" label-for="problemTypeInput">
                <b-form-select @change="form.specialist = null" id="problemTypeInput" v-model="form.problemType.selected" :options="form.problemType.options" />
            </b-form-group>

            <b-form-group v-if="form.problemType.selected === 'software'" id="problemTypeSoftwareInputGroup" label="Software Subtype:" label-for="problemTypeSoftwareInput">
                <b-form-select @change="form.specialist = null" id="problemTypeSoftwareInput" v-model="form.subProblemTypes.software.selected" :options="form.subProblemTypes.software.options" />
            </b-form-group>

            <b-form-group v-if="form.problemType.selected === 'hardware'" id="problemTypeHardwareInputGroup" label="Hardware Subtype:" label-for="problemTypeHardwareInput">
                <b-form-select @change="form.specialist = null" id="problemTypeHardwareInput" v-model="form.subProblemTypes.hardware.selected" :options="form.subProblemTypes.hardware.options" />
            </b-form-group>

            <hardware v-if="form.problemType.selected === 'hardware'" v-bind:serialNo.sync="form.hardware.serialNo" v-bind:type.sync="form.hardware.type" v-bind:make.sync="form.hardware.make" v-bind:description.sync="form.hardware.description"></hardware>

            <software v-if="form.problemType.selected === 'software'" v-bind:name.sync="form.software.name" v-bind:description.sync="form.software.description"></software>

            <b-form-group id="notesInputGroup" label="Notes:" label-for="notesInput">
                <b-form-textarea id="notesInput" v-model="form.notes" rows="3"></b-form-textarea>
            </b-form-group>

            <b-form-group>
                <p v-if="form.specialist">Selected: {{ form.specialist.name }} ({{ form.specialist.problemTypes.join(', ') }})</p>
                <b-button :disabled="!form.problemType.selected" :variant="form.specialist == null ? 'primary' : 'secondary'" @click="updateSpecialists" v-b-modal.specialistModal>Choose Specialist</b-button>
            </b-form-group>

            <b-modal id="specialistModal" hide-footer hide-header ref="specialistModal">
                <b-card v-for="specialist in form.specialists" :key="specialist.id" :title="specialist.name" class="my-1">
                    <b-row>
                        <b-col>
                            <span>Jobs: {{ specialist.jobs }}</span>
                            <div v-for="(problemType, index) in specialist.problemTypes" :key="index">
                                <span class="text-capitalize">{{ problemType }}</span>
                            </div>
                        </b-col>
                        <b-col class="d-flex align-items-end justify-content-end">
                            <b-button variant="primary" @click="chooseSpecialist(specialist)">Choose</b-button>
                        </b-col>
                    </b-row>
                </b-card>
            </b-modal>

            <b-form-group id="solvedInputGroup">
                <b-form-checkbox id="solvedInput" v-model="form.solved">
                    <span>Solved</span>
                </b-form-checkbox>
            </b-form-group>

            <b-form-group v-if="form.solved" id="specialistIdInputGroup" label="Solved by:" label-for="specialistIdInput">
                <b-form-input id="specialistIdInput" type="number" v-model="form.specialistId" placeholder="Enter specialist ID"></b-form-input>
            </b-form-group>

            <b-form-group v-if="form.solved" id="dateTimeInputGroup" label="Date and time:" label-for="dateTimeInput">
                <b-form-input id="dateTimeInput" type="date" v-model="form.dateTime"></b-form-input>
            </b-form-group>

            <b-form-group v-if="form.solved" id="solutionInputGroup" label="Solution:" label-for="solutionInput">
                <b-form-textarea id="solutionInput" v-model="form.solution"></b-form-textarea>
            </b-form-group>

            <b-button type="submit" variant="primary">Submit Edit</b-button>
        </b-form>
    </b-card>
</template>

<script>
import moment from "moment";
import VeeValidate from "vee-validate";

export default {
    data() {
        return {
            form: {
                call: {
                    callId: "",
                    caller: "",
                    operator: "",
                    date: "",
                    time: "",
                    reason: ""
                },
                callers: [
                    {
                        callId: "370601",
                        caller: "James Boss",
                        operator: "Jimmy Page",
                        date: "2018-11-11",
                        time: "20:11",
                        reason: "Dead pixels on monitor"
                    }
                ],
                problemId: this.$route.params.problemId,
                reporterId: "",
                problemType: {
                    selected: "hardware",
                    options: [
                        { value: null, text: "Please select a problem type" },
                        { value: "hardware", text: "Hardware" },
                        { value: "software", text: "Software" },
                        { value: "network", text: "Network" }
                    ]
                },
                subProblemTypes: {
                    hardware: {
                        selected: "monitor",
                        options: [
                            {
                                value: null,
                                text:
                                    "Please select a sub problem type (optional)"
                            },
                            { value: "PC", text: "PC" },
                            { value: "monitor", text: "Monitor" },
                            { value: "keyboard", text: "Keyboard" },
                            { value: "printer", text: "Printer" }
                        ]
                    },
                    software: {
                        selected: null,
                        options: [
                            {
                                value: null,
                                text:
                                    "Please select a sub problem type (optional)"
                            },
                            { value: "Windows", text: "Windows" },
                            { value: "Mac OS", text: "Mac OS" },
                            { value: "Linux", text: "Linux" }
                        ]
                    }
                },
                hardware: {
                    serialNo: "543543",
                    type: "RFIX78R",
                    make: "BenQ",
                    description: ""
                },
                software: {
                    name: "",
                    description: ""
                },
                notes: "Water damage found in PC",
                specialist: {
                    id: "12",
                    name: "Tom Scott",
                    problemTypes: ["hardware", "monitor"],
                    jobs: 4
                },
                specialists: [],
                dateTime: "",
                solution: ""
            }
        };
    },
    methods: {
        addCall(evt) {
            evt.preventDefault();
            this.$validator.validateAll("call").then(res => {
                if (res) {
                    const newCallId = this.getNewId();
                    const call = this.form.call;
                    call.callId = newCallId;
                    this.form.callers.push(call);
                    this.resetCallInputs();
                }
            });
        },
        getNewId() {
            //Get ID from server...
            return Math.floor(100000 + Math.random() * 900000).toString();
        },
        resetCallInputs() {
            this.form.call = {
                callId: "",
                caller: "",
                operator: this.userName,
                date: moment().format("YYYY-MM-DD"),
                time: moment().format("HH:mm"),
                reason: ""
            };
            this.$refs.callModal.hide();
            this.$validator.reset();
        },
        updateSpecialists() {
            //From database...
            const specialists = [
                {
                    id: "12",
                    name: "Tom Scott",
                    problemTypes: ["hardware", "monitor"],
                    jobs: 4
                },
                {
                    id: "14",
                    name: "Jenna Fischer",
                    problemTypes: ["software", "Linux", "Windows"],
                    jobs: 0
                },
                {
                    id: "32",
                    name: "John Bishop",
                    problemTypes: ["software", "Mac OS"],
                    jobs: 2
                },
                {
                    id: "65",
                    name: "Jim Davis",
                    problemTypes: ["hardware", "printer"],
                    jobs: 3
                },
                {
                    id: "87",
                    name: "Kelly Kapoor",
                    problemTypes: ["software", "Windows"],
                    jobs: 0
                },
                {
                    id: "33",
                    name: "William Jones",
                    problemTypes: ["software", "Mac OS"],
                    jobs: 1
                },
                {
                    id: "33",
                    name: "Ivan Torvalds Guy",
                    problemTypes: ["network"],
                    jobs: 1
                },
                {
                    id: "56",
                    name: "Greg Smith",
                    problemTypes: ["hardware", "PC"],
                    jobs: 3
                }
            ];
            let types = this.types;

            this.form.specialists = specialists.filter(spec =>
                this.types.every(type => spec.problemTypes.includes(type))
            );

            while (this.form.specialists.length === 0) {
                this.form.specialists = specialists.filter(spec =>
                    types
                        .slice(0, types.length - 1)
                        .every(type => spec.problemTypes.includes(type))
                );
            }

            this.form.specialists = this.form.specialists.sort(
                (a, b) => a.jobs < b.jobs
            );
            console.log(this.form.specialists.sort((a, b) => a.jobs - b.jobs));
        },
        chooseSpecialist(specialist) {
            this.form.specialist = specialist;
            this.$refs.specialistModal.hide();
        }
    },
    mounted() {
        this.resetCallInputs();
    },
    computed: {
        userName() {
            return this.$store.state.userName;
        },
        types() {
            const problemTypes = [this.form.problemType.selected];
            switch (this.form.problemType.selected) {
                case "hardware":
                    if (this.form.subProblemTypes.hardware.selected) {
                        problemTypes.push(
                            this.form.subProblemTypes.hardware.selected
                        );
                    }
                    break;
                case "software":
                    if (this.form.subProblemTypes.software.selected) {
                        problemTypes.push(
                            this.form.subProblemTypes.software.selected
                        );
                    }

                    break;
            }
            return problemTypes;
        },
        showResult() {
            return this.form.callers.length > 0 && this.form.specialist;
        }
    }
};
</script>
