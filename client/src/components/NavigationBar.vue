<template>
  <div>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="/">
          <img
            src="/AutoSkelbiu/sc.png"
            alt=""
            width="250"
            height="65"
            class="d-inline-block align-text-top"
          />
        </a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNavAltMarkup"
          aria-controls="navbarNavAltMarkup"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNavAltMarkup">
          <div class="navbar-nav me-auto">
            <router-link :to="{ name: 'Home' }" style="text-decoration: none; color: inherit;">
            <a class="nav-link">Automobilių paieška</a>
            </router-link>
            <router-link :to="{ name: 'About' }" style="text-decoration: none; color: inherit;">
              <a class="nav-link">Apie mus</a>
            </router-link>
          </div>
          <div>
            <li class="nav-item dropdown">
              <a
                v-if="username != null"
                class="nav-link dropdown-toggle"
                id="navbarDarkDropdownMenuLink"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                Sveiki atvyke, {{ username }}
              </a>
              <ul
                class="dropdown-menu"
                aria-labelledby="navbarDarkDropdownMenuLink"
              >
                <li>
                  <a @click="goToRememberedList()" class="dropdown-item"
                    >Įsiminti skelbimai</a
                  >
                </li>
                <li>
                  <a @click="logout()" class="dropdown-item">Atsijungti</a>
                </li>
              </ul>
            </li>
            <router-link :to="{ name: 'Login' }">
              <a v-if="username == null" class="nav-link">
                Esate neprisijungęs. Prisijungti?
              </a>
            </router-link>
          </div>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "NavigationBar",
  data() {
    return {
      username: null,
    };
  },
  methods: {
    getUsername() {
      console.log(localStorage.userName);
      var username = localStorage.userName;
      if (username != null) {
        this.username = username;
      }
    },
    logout() {
      localStorage.removeItem("userName");
      localStorage.removeItem("userId");
      window.location.href = "/";
    },
    goToRememberedList() {
      this.$router.push({ name: "RememberedList" });
    },
  },
  mounted() {
    this.getUsername();
  },
};
</script>

<style scoped>
ul,
li {
  list-style-type: none;
}
</style>