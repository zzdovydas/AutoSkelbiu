import { createWebHistory, createRouter } from "vue-router";
import Home from "@/views/Home.vue";
import Search from "@/views/Search.vue";
import Auto from "@/views/Auto.vue";
import NotFound from "@/views/NotFound.vue";
import Login from "@/views/Login.vue";
import About from "@/views/About.vue";
import Register from "@/views/Register.vue";
import RememberedList from "@/views/RememberedList.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/search",
    name: "Search",
    component: Search,
  },
  {
    path: "/remembered",
    name: "RememberedList",
    component: RememberedList,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/register",
    name: "Register",
    component: Register,
  },
  {
    path: "/auto/:id",
    name: "Auto",
    component: Auto,
  },
  {
    path: "/:catchAll(.*)",
    component: NotFound,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
