import Vue from 'vue';
import Router from 'vue-router';
import Dashboard from './views/Dashboard.vue';
import About from './views/About.vue';
import ProblemNew from './views/ProblemNew.vue';
import ProblemEdit from './views/ProblemEdit.vue';
import ProblemSolved from './views/ProblemSolved.vue';
import Login from './views/auth/Login.vue';
import Register from './views/auth/Register.vue';
import DashboardAnalyst from './views/DashboardAnalyst.vue';
import DashboardSpecialist from './views/DashboardSpecialist.vue';
Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/', 
      redirect: { name: 'login' }
    },
    {
      path: '/operator/dashboard',
      name: 'dashboard',
      component: Dashboard
    },
    {
      path: '/about',
      name: 'about',
      component: About
    },
    {
      path: '/problem/new',
      name: 'problemnew',
      component: ProblemNew
    },
    {
      path: '/problem/:problemId/edit',
      name: 'problemedit',
      component: ProblemEdit
    },
    {
      path: '/problem/:problemId/solved',
      name: 'problemsolved',
      component: ProblemSolved
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/register',
      name: 'register',
      component: Register
    },
    {
      path: '/specialist/dashboard',
      name: 'dashboardS',
      component: DashboardSpecialist
    },
    {
      path: '/analyst/dashboard',
      name: 'dashboardA',
      component: DashboardAnalyst
    }
  ]
});