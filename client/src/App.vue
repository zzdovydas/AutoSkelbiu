<template>
  <div>
    <Navbar />
    <router-view :key="$route.fullPath" />
  </div>
</template>

<script>
import Navbar from "./components/NavigationBar.vue";

export default {
  name: "App",
  components: {
    Navbar,
  },
};
</script>