<template>
  <div>
    <div class="row">
      <div class="col-md-9">
        <div
          class="alert alert-light mx-auto"
          style="max-width: 740px"
          role="alert"
        >
          Rodoma {{ autoList.length }} įrašų
        </div>
        <div
          v-for="auto in autoList"
          :key="auto"
          class="card mb-3 p-1 mx-auto"
          style="max-width: 740px"
        >
          <div class="row g-0">
            <div class="col-md-4">
              <img @click="
            this.$router.push({ name: 'Auto', params: { id: auto.autO_ID } })
          " :src="auto.thumbnail" class="thumbnail" alt="" />
            </div>
            <div class="col-md-8 ps-2">
              <div class="card-body">
                <h6 @click="removeRememberedItem(auto.linK_ID)" class="float-end">
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
                <h5 @click="
            this.$router.push({ name: 'Auto', params: { id: auto.autO_ID } })
          " class="card-title">
                  {{ auto.autO_MAKE }} {{ auto.autO_MODEL }}
                </h5>
                <p class="card-text mb-4">
                  {{ auto.autO_ENGINE }} | {{ auto.autO_MILEAGE }} |
                  {{ auto.autO_FUEL_TYPE }} | {{ auto.autO_TYPE }} |
                  {{ auto.autO_GEARBOX }}
                </p>
                <h5 class="float-end">{{ auto.autO_PRICE }} €</h5>
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
export default {
  name: "RememberedList",
  data() {
    return {
      autoList: [],
    };
  },
  methods: {
      removeRememberedItem(linkId) {
          var userId = localStorage.userId;
          axios
        .get("api/User/RemoveRememberedItem", { params: { userId: userId, linkId: linkId } })
        .then(location.reload());
      }
  },
  created() {
    var userId = localStorage.userId;
    if (userId != null || userId != "") {
      axios
        .get("api/User/GetRememberedList", { params: { userId: userId } })
        .then((response) => (this.autoList = response.data));
    }
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