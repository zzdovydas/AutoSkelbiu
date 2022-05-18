<template>
  <div>
    <div class="row">
      <Filter />
      <div class="col-md-9">
        <div class="alert alert-light" style="max-width: 740px" role="alert">
          {{ pageNumber }} puslapis. Rodoma {{ autoList.length }} įrašų.
        </div>
        <div
          v-for="auto in autoList"
          :key="auto"
          class="card mb-3 p-1"
          style="max-width: 740px"
          @click="newPage(auto.autO_ID)"
        >
          <div class="row g-0">
            <div class="col-md-4">
              <img :src="'/' + auto.thumbnail" class="thumbnail" alt="" />
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
        <nav aria-label="Page navigation example">
          <ul class="pagination">
            <li v-if="pageNumber != 1" class="page-item">
              <a class="page-link" @click="nextPage(-1)" aria-label="Previous">
                <span aria-hidden="true">&laquo;</span>
              </a>
            </li>
            <li v-if="autoList.length >= 20" class="page-item">
              <a class="page-link" @click="nextPage(1)" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
              </a>
            </li>
          </ul>
        </nav>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import Filter from "@/components/Filter.vue";
export default {
  name: "Search",
  components: {
    Filter,
  },
  data() {
    return {
      autoList: [],
      pageNumber: 1,
    };
  },
  methods: {
    newPage(id) {
      let routeData = this.$router.resolve({
        name: "Auto",
        params: { id: id },
      });
      window.open(routeData.href, "_blank");
    },
    nextPage(inc) {
      var makeId = this.$route.query.makeId;
      var priceFrom = this.$route.query.priceFrom;
      var priceTo = this.$route.query.priceTo;
      var yearFrom = this.$route.query.yearFrom;
      var yearTo = this.$route.query.yearTo;
      var pageNumber = parseInt(this.$route.query.pageNumber);
      var fuelTypeId = this.$route.query.fuelTypeId;
      var vin = this.$route.query.hasVin;
      var gearbox = this.$route.query.gearbox;
      var autoModel = this.$route.query.autoModel;

      if (typeof makeId === "undefined") makeId = "";
      if (typeof fuelTypeId === "undefined") fuelTypeId = "";
      if (typeof priceFrom === "undefined") priceFrom = "";
      if (typeof priceTo === "undefined") priceTo = "";
      if (typeof yearFrom === "undefined") yearFrom = "";
      if (typeof yearTo === "undefined") yearTo = "";
      if (typeof vin === "undefined") vin = "0";
      if (typeof gearbox === "undefined") gearbox = "";
      if (typeof autoModel === "undefined") autoModel = "";

      this.pageNumber = pageNumber;

      if (isNaN(pageNumber)) pageNumber = 2;
      else pageNumber += inc;

      window.location.href =
        "/search?makeId=" +
        makeId +
        "&fuelTypeId=" +
        fuelTypeId +
        "&pageNumber=" +
        pageNumber.toString() +
        "&priceFrom=" +
        priceFrom +
        "&priceTo=" +
        priceTo +
        "&yearFrom=" +
        yearFrom +
        "&yearTo=" +
        yearTo +
        "&hasVin=" +
        vin +
        "&gearbox=" +
        gearbox +
        "&autoModel=" +
        autoModel +
        "&";
    },
  },
  created() {
    var makeId = this.$route.query.makeId;
    var pageNumber = this.$route.query.pageNumber;
    var fuelTypeId = this.$route.query.fuelTypeId;
    var priceFrom = this.$route.query.priceFrom;
    var priceTo = this.$route.query.priceTo;
    var yearFrom = this.$route.query.yearFrom;
    var yearTo = this.$route.query.yearTo;
    var vin = this.$route.query.hasVin;
    var gearbox = this.$route.query.gearbox;
      var autoModel = this.$route.query.autoModel;

    this.pageNumber = pageNumber;
    console.log(this.pageNumber);

    if (typeof pageNumber == "undefined") pageNumber = "1";
    if (typeof makeId === "undefined") makeId = "";
    if (typeof fuelTypeId === "undefined") fuelTypeId = "";
    if (typeof priceFrom === "undefined") priceFrom = "";
    if (typeof priceTo === "undefined") priceTo = "";
    if (typeof yearFrom === "undefined") yearFrom = "";
    if (typeof yearTo === "undefined") yearTo = "";
    if (typeof vin === "undefined") vin = "0";
    if (typeof gearbox === "undefined") gearbox = "";
      if (typeof autoModel === "undefined") autoModel = "";

    this.pageNumber = pageNumber;
    axios
      .get("/api/Auto/GetAutoListFiltered", {
        params: {
          makeId: makeId,
          fuelTypeId: fuelTypeId,
          pageNumber: pageNumber,
          priceFrom: priceFrom,
          priceTo: priceTo,
          yearFrom: yearFrom,
          yearTo: yearTo,
          hasVin: vin,
          gearbox: gearbox,
          autoModel: autoModel
      }})
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
