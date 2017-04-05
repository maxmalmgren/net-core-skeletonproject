import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import 'font-awesome-webpack';
import Vue from 'vue';
import VueResource from 'vue-resource';
import App from './components/app.vue';
import LandingPage from './components/landing-page/landing-page.vue';
import ExamplePage from './components/example/example.vue';
import VueRouter from 'vue-router';

Vue.use(VueResource);
Vue.use(VueRouter);

const routes = [
    { path: '/', component: LandingPage }
	{ path: '/example', component: ExamplePage }
];

const router = new VueRouter({
	routes,
	mode: 'history'
})

const app = new Vue({
	router,
	render: h => h(App)
}).$mount('#app');