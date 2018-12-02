<template>
    <b-row class="justify-content-center">
        <b-col md="8">
            <b-card class="text-center" header-tag="header">
                <h2 slot="header" class="mb-0">Welcome Operator</h2>
                <div id="logExistingProblemContainer" class="mb-4">
                    <h3>Log Existing Problem</h3>
                    <b-button :to="{ name: 'problemnew' }" class="mia-block-semi" id="newProblemBtn" block variant="primary">New Problem</b-button>
                </div>
                <problem-edit-short></problem-edit-short>
            </b-card>
        </b-col>
    </b-row>
</template>

