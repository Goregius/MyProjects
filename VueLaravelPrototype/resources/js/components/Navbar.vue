<template>
    <b-navbar class="mia-navbar" toggleable="md" type="dark" variant="info">

        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

        <b-navbar-brand :to="{ name: 'dashboard' }">Make-It-All</b-navbar-brand>

        <b-collapse is-nav id="nav_collapse">
            <!-- Right aligned nav items -->
            <b-navbar-nav class="ml-auto">
                <b-nav-item :to="{ name: 'problemnew' }">New Problem</b-nav-item>
                <b-nav-item-dropdown :text="userName" right>
                    <b-dropdown-item :to="{ name: 'dashboard' }">Dashboard</b-dropdown-item>
                    <b-dropdown-item :to="{ name: 'dashboardA' }">DashboardA</b-dropdown-item>
                    <b-dropdown-item :to="{ name: 'dashboardS' }">DashboardS</b-dropdown-item>
                    <b-dropdown-item :to="{ name: 'login' }">Logout</b-dropdown-item>
                </b-nav-item-dropdown>
            </b-navbar-nav>

        </b-collapse>
    </b-navbar>
</template>

<script>
export default {
    computed: {
        userName() {
            return this.$store.state.userName;
        }
    }
};
</script>
