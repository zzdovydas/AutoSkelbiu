<template>
  <div>
    <div class="row">
      <Filter />
      <div class="col-md-9">
        <div class="alert alert-light" style="max-width: 740px" role="alert">
          Rodoma {{ autoList.length }} įrašų
        </div>
        <div
          v-for="auto in autoList"
          :key="auto"
          class="card mb-3 p-1"
          style="max-width: 740px"
          @click="
            this.$router.push({ name: 'Auto', params: { id: auto.autO_ID } })
          "
        >
          <div class="row g-0">
            <div class="col-md-4">
              <img :src="auto.thumbnail" class="thumbnail" alt="" />
            </div>
            <div class="col-md-8 ps-2">
              <div class="card-body">
                <h5 class="card-title">
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
import Filter from "@/components/Filter.vue";
export default {
  name: "Home",
  components: {
    Filter,
  },
  data() {
    return {
      autoList: [],
    };
  },
  created() {
    axios
      .get("api/Auto/GetAutoList")
      .then((response) => (this.autoList = response.data));
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