<template>
  <div>
    <div id="myModal" class="modal">
      <span class="close">&times;</span>
      <img class="modal-content" id="img01" />
      <div id="caption"></div>
    </div>
    <div class="container p-0 mt-5 mb-5">
      <div class="card">
        <div class="row m-0">
          <div class="col-md-6 border-end">
            <div class="row m-0">
              <div class="col-md-12 p-0 bg-dark">
                <img
                  :src="selectedImg"
                  id="myImg"
                  style="
                    max-width: 100%;
                    max-height: 450px;
                    display: block;
                    margin: auto;
                  "
                />
              </div>
              <div class="mt-4">
                <p>Daugiau nuotraukų:</p>
                <ul style="list-style-type: none">
                  <li>
                    <img
                      v-for="image in imageList"
                      :key="image"
                      @click="changeImage(image.imagE_PATH)"
                      :src="'/' + image.imagE_PATH"
                      width="70"
                    />
                  </li>
                </ul>
              </div>
            </div>
          </div>
          <div class="col-md-6 p-2">
            <div class="right-side">
              <div class="d-flex justify-content-between align-items-center">
                <h3>{{ autoList.autO_MAKE }} {{ autoList.autO_MODEL }}</h3>
                <h6 v-if="autoRemembered == true" 
                @click="removeRememberedItem(autoList.linK_ID)" class="float-end">
                    Pamiršti skelbimą 
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="20"
                    height="20"
                    fill="currentColor"
                    class="bi bi-suit-heart-fill"
                    viewBox="0 0 16 16"
                  >
                    <path
                      d="M4 1c2.21 0 4 1.755 4 3.92C8 2.755 9.79 1 12 1s4 1.755 4 3.92c0 3.263-3.234 4.414-7.608 9.608a.513.513 0 0 1-.784 0C3.234 9.334 0 8.183 0 4.92 0 2.755 1.79 1 4 1z"
                    />
                  </svg>
                </h6>
                <h5 v-if="autoRemembered == false"
                  @click="saveAsRemembered(autoList.linK_ID)"
                  class="float-end"
                >
                  Įsiminti skelbimą
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    width="20"
                    height="20"
                    fill="currentColor"
                    class="bi bi-heart"
                    viewBox="0 0 16 16"
                  >
                    <path
                      d="m8 2.748-.717-.737C5.6.281 2.514.878 1.4 3.053c-.523 1.023-.641 2.5.314 4.385.92 1.815 2.834 3.989 6.286 6.357 3.452-2.368 5.365-4.542 6.286-6.357.955-1.886.838-3.362.314-4.385C13.486.878 10.4.28 8.717 2.01L8 2.748zM8 15C-7.333 4.868 3.279-3.04 7.824 1.143c.06.055.119.112.176.171a3.12 3.12 0 0 1 .176-.17C12.72-3.042 23.333 4.867 8 15z"
                    />
                  </svg>
                </h5>
              </div>
              <h3>{{ autoList.autO_PRICE }} €</h3>
              <paramTemplate
                v-if="autoList.autO_MAKE != null"
                v-bind:paramList="autoList"
              />
              <div class="buttons d-flex flex-row mt-5 gap-3">
                <a :href="autoList.linK_URL" class="btn btn-dark"
                  >Eiti į pardavėjo skelbimą</a
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import paramTemplate from "@/components/ParamsToTemplate.vue";
export default {
  name: "Home",
  components: {
    paramTemplate,
  },
  data() {
    return {
      autoList: {},
      imageList: [],
      selectedImg: "",
      autoRememberedList: [],
    };
  },
  methods: {
    changeImage(path) {
      this.selectedImg = "/" + path;
    },
    saveAsRemembered(linkId) {
      var userId = localStorage.userId;
      axios
        .get("/api/User/AddRememberedItem", {
          params: { userId: userId, linkId: linkId },
        })
        .then((response) => {console.log(response);
        location.reload();});
    },
    removeRememberedItem(linkId) {
          var userId = localStorage.userId;
          axios
        .get("/api/User/RemoveRememberedItem", { params: { userId: userId, linkId: linkId } })
        .then(location.reload());
      }
  },
  computed: {
    getParams() {
      var params = this.autoList;
      params.linK_ID = null;
      params.autO_ID = null;
      return params;
    },
    autoRemembered() {
      var autoRemembered = false;
      this.autoRememberedList.forEach(auto => {
        if (auto.linK_ID == this.autoList.linK_ID)
          autoRemembered = true;
      });
      return autoRemembered;
    }
  },
  created() {
    var autoId = this.$route.params.id;
    axios
      .get("/api/Auto/GetAutoById", { params: { autoId: autoId } })
      .then((response) => {
        this.autoList = response.data;
        axios
          .get("/api/Auto/GetImagesById", {
            params: { linkId: this.autoList.linK_ID },
          })
          .then((response) => {
            this.imageList = response.data;
            this.selectedImg = "/" + this.imageList[0].imagE_PATH;
            var userId = localStorage.userId;
            if (userId != null || userId != "") {
              axios
                .get("/api/User/GetRememberedList", {
                  params: { userId: userId },
                })
                .then((response) => (this.autoRememberedList = response.data));
            }
          });
      });
  },
  mounted() {
    require("../services/img.enlarge.js");
  },
};
</script>

<style>
.thumbnail {
  float: left;
  width: 240px;
  height: 180px;
  object-fit: scale-down;
}
</style>

<style>
body {
  font-family: Arial, Helvetica, sans-serif;
}

#myImg {
  border-radius: 5px;
  cursor: pointer;
  transition: 0.3s;
}

#myImg:hover {
  opacity: 0.7;
}

/* The Modal (background) */
.modal {
  display: none; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  padding-top: 100px; /* Location of the box */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0, 0, 0); /* Fallback color */
  background-color: rgba(0, 0, 0, 0.9); /* Black w/ opacity */
}

/* Modal Content (image) */
.modal-content {
  margin: auto;
  display: block;
  width: 80%;
  max-width: 1000px;
  max-height: 720px;
}

/* Caption of Modal Image */
#caption {
  margin: auto;
  display: block;
  width: 80%;
  max-width: 700px;
  text-align: center;
  color: #ccc;
  padding: 10px 0;
  height: 150px;
}

/* Add Animation */
.modal-content,
#caption {
  -webkit-animation-name: zoom;
  -webkit-animation-duration: 0.6s;
  animation-name: zoom;
  animation-duration: 0.6s;
}

/* The Close Button */
.close {
  position: absolute;
  top: 15px;
  right: 35px;
  color: #f1f1f1;
  font-size: 40px;
  font-weight: bold;
  transition: 0.3s;
}

.close:hover,
.close:focus {
  color: #bbb;
  text-decoration: none;
  cursor: pointer;
}

/* 100% Image Width on Smaller Screens */
@media only screen and (max-width: 700px) {
  .modal-content {
    width: 100%;
  }
}
</style>