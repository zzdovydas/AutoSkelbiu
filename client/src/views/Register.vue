<template>
    <section class="vh-100">
  <div class="container-fluid h-custom">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-md-9 col-lg-6 col-xl-5">
        <img src="https://mdbootstrap.com/img/Photos/new-templates/bootstrap-login-form/draw2.png" class="img-fluid"
          alt="Sample image">
      </div>
      <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
        <form>
          <div class="d-flex flex-row align-items-center justify-content-center justify-content-lg-start">
            <p class="lead fw-normal mb-4 me-3">Registruoti naują paskyrą</p>
          </div>

          <!-- Username input -->
          <div class="form-outline mb-4">
            <input type="text" v-model="user.username" class="form-control form-control-lg"
              placeholder="Vartotojo vardas" />
          </div>

          <!-- Email input -->
          <div class="form-outline mb-4">
            <input type="email" v-model="user.email" class="form-control form-control-lg"
              placeholder="Vartotojo el. paštas" />
          </div>

          <!-- Password input -->
          <div class="form-outline mb-3">
            <input type="password" v-model="user.password" class="form-control form-control-lg"
              placeholder="Vartotojo slaptažodis" />
          </div>

          <div class="text-center text-lg-start mt-4 pt-2">
            <button type="button" class="btn btn-primary btn-lg"
              style="padding-left: 2.5rem; padding-right: 2.5rem;">Registruotis</button>
            <p class="small fw-bold mt-2 pt-1 mb-0">Jau turite paskyrą? <router-link :to="{ name: 'Login' }"><a href="#!"
                class="link-danger">Prisijungti</a></router-link></p>
          </div>

        </form>
      </div>
    </div>
  </div>
</section>
</template>

<script>

export default {
  name: "Register",
  data() {
    return {
      user: {
        username: "",
        email: "",
        password: ""
      }
    }
  }
}
</script>
