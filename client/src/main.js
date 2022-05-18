import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from "./store/index.js";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

import "vue-select/dist/vue-select.css";

createApp(App).use(router).use(store).mount('#app')