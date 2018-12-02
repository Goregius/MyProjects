
/**
 * First we will load all of this project's JavaScript dependencies which
 * includes Vue and other libraries. It is a great starting point when
 * building robust, powerful web applications using Vue and Laravel.
 */

import router from './router';
import store from './store';
import './bootstrap';
import BootstrapVue from 'bootstrap-vue';
import VeeValidate from 'vee-validate';

window.Vue = require('vue');
Vue.use(BootstrapVue);
Vue.use(require('vue-moment'));
Vue.use(VeeValidate, { fieldsBagName: 'veeFields' });


/**
 * Next, we will create a fresh Vue application instance and attach it to
 * the page. Then, you may begin adding components to this application
 * or customize the JavaScript scaffolding to fit your unique needs.
 */

Vue.component('app', require('./components/App.vue'));
Vue.component('navbar', require('./components/Navbar.vue'));
Vue.component('problem-edit-short', require('./components/ProblemEditShort.vue'));
Vue.component('hardware', require('./components/Hardware.vue'));
Vue.component('software', require('./components/Software.vue'));

const app = new Vue({
    router,
    store,
    el: '#app'
});
