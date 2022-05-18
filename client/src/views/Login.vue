<template>
  <section class="vh-100">
    <div class="container-fluid h-custom">
      <div class="row d-flex justify-content-center align-items-center h-100">
        <div class="col-md-9 col-lg-6 col-xl-5">
          <img
            src="/AutoSkelbiu/login.gif"
            class="img-fluid"
            alt="Sample image"
          />
        </div>
        <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">
          <form @submit.prevent="submitLogin()">
            <div
              class="
                d-flex
                flex-row
                align-items-center
                justify-content-center justify-content-lg-start
              "
            >
              <p class="lead fw-normal mb-4 me-3">Prisijunkite</p>
            </div>

            <!-- Email input -->
            <div class="form-outline mb-4">
              <input
                type="email"
                v-model="user.email"
                class="form-control form-control-lg"
                placeholder="Vartotojo el. paštas"
              />
            </div>

            <!-- Password input -->
            <div class="form-outline mb-3">
              <input
                type="password"
                v-model="user.password"
                class="form-control form-control-lg"
                placeholder="Vartotojo slaptažodis"
              />
            </div>

            <div class="d-flex justify-content-between align-items-center">
              <!-- Checkbox -->
              <div class="form-check mb-0">
                <input
                  class="form-check-input me-2"
                  type="checkbox"
                  value=""
                  id="form2Example3"
                />
                <label class="form-check-label" for="form2Example3">
                  Prisiminti
                </label>
              </div>
            </div>

            <div class="text-center text-lg-start mt-4 pt-2">
              <button
                type="submit"
                class="btn btn-primary btn-lg"
                style="padding-left: 2.5rem; padding-right: 2.5rem"
              >
                Prisijungti
              </button>
              <p class="small fw-bold mt-2 pt-1 mb-0">
                Neturite paskyros?
                <router-link :to="{ name: 'Register' }"
                  ><a href="#!" class="link-danger">Registruotis</a></router-link
                >
              </p>
            </div>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from "axios";
import shaEncoder from "@/helpers/sha256encoder.js";

export default {
  name: "Login",
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
    };
  },
  methods: {
      checkValidity(data) {
        console.log(data);
          if (data != null && data != "")
          {
              console.log(data);
              localStorage.setItem("userName", data.useR_NAME);
              localStorage.setItem("userId", data.useR_ID);
              window.location.href = "/";
          }
      },
    submitLogin() {
      var passwordEncrypted = shaEncoder.sha256(this.user.password);
      axios
        .get("api/User/ValidateUserData", {
          params: { email: this.user.email, password: passwordEncrypted },
        }).then((response) => this.checkValidity(response.data));
    },
  },
};
</script>
