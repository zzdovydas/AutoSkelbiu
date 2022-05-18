<template>
  <div class="col-md-3">
    <div class="card p-1">
      <h4>Filtras</h4>
      Automobilio markė
      <select
        @change="setAutoMake($event.target.value)"
        class="selectpicker form-select"
      >
        <option selected>Pasirinkite automobilio markę</option>
        <option :value="make.autO_MAKE_ID" v-for="make in makeList" :key="make">
          {{ make.autO_MAKE }}
          <span class="text-muted float-end">{{ make.autO_COUNT }}</span>
        </option>
      </select>
      Automobilio modelis
      <select
        @change="setAutoModel($event.target.value)"
        class="selectpicker form-select"
      >
        <option selected>Pasirinkite automobilio modelį</option>
        <option
          :value="model.autO_MODEL"
          v-for="model in modelList"
          :key="model"
        >
          {{ model.autO_MODEL }}
        </option>
      </select>
      Kuro tipas
      <select
        @change="setFuelType($event.target.value)"
        class="form-select mb-2"
        aria-label="Default select example"
      >
        <option selected>Pasirinkite kuro tipą</option>
        <option
          :value="fuelType.autO_FUEL_TYPE_ID"
          v-for="fuelType in fuelTypeList"
          :key="fuelType"
        >
          {{ fuelType.autO_FUEL_TYPE_NAME }}
        </option>
      </select>
      Pavarų dėžė
      <select
        @change="setGearbox($event.target.value)"
        class="form-select mb-2"
        aria-label="Default select example"
      >
        <option selected>Pasirinkite pavarų dėžę</option>
        <option value="Automatinė">Automatinė</option>
        <option value="Mechaninė">Mechaninė</option>
      </select>
      <div class="row mb-2">
        <div class="col-md-6">
          Kaina nuo
          <input
            type="text"
            class="form-control"
            v-model="priceFrom"
            oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1').substring(0, 7);"
          />
        </div>
        <div class="col-md-6">
          Kaina iki
          <input
            type="text"
            class="form-control"
            v-model="priceTo"
            oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1').substring(0, 7);"
          />
        </div>
      </div>
      <div class="row mb-2">
        <div class="col-md-6">
          Metai nuo
          <input
            type="text"
            class="form-control"
            v-model="yearFrom"
            oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1').substring(0, 4);"
          />
        </div>
        <div class="col-md-6">
          Metai iki
          <input
            type="text"
            class="form-control"
            v-model="yearTo"
            oninput="this.value = this.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1').substring(0, 4);"
          />
        </div>
      </div>
      <div class="row mb-2">
        <div class="col-md-12">
          <div class="form-check">
            <input
              v-model="vin"
              class="form-check-input"
              type="checkbox"
              value="1"
              id="flexCheckDefault"
            />
            <label class="form-check-label" for="flexCheckDefault">
              Ieškoti skelbimų su VIN kodu
            </label>
          </div>
        </div>
      </div>
      <button
        type="button"
        @click="searchWithFilters()"
        class="btn btn-primary"
      >
        Filtruoti
      </button>
    </div>
    {{ products }}
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Home",
  components: {},
  data() {
    return {
      autoList: [],
      makeList: [],
      modelList: [],
      fuelTypeList: [],
      searchParams: {},
      selectedAutoMake: null,
      selectedAutoModel: null,
      selectedFuelType: null,
      priceFrom: null,
      priceTo: null,
      yearFrom: null,
      yearTo: null,
      gearBox: null,
      modelLoaded: false,
      vin: 0,
    };
  },
  methods: {
    setAutoMake(autoMake) {
      this.selectedAutoMake = autoMake;
      this.getAutoModels(autoMake);
    },
    getAutoModels(makeId) {
      axios
      .get("/api/Auto/GetAutoModels", {params: {makeId: makeId}})
      .then((response) => (this.modelList = response.data.slice().sort(function(a, b){
    return (a.autO_MODEL > b.autO_MODEL) ? 1 : -1})));
    },
    setAutoModel(autoModel) {
      this.selectedAutoModel = autoModel;
    },
    setFuelType(fuelType) {
      this.selectedFuelType = fuelType;
    },
    setGearbox(gearbox) {
      this.gearBox = gearbox;
      console.log(this.gearBox);
    },
    searchWithFilters() {
      let params = "";
      if (this.selectedAutoMake != null)
        params += "makeId=" + this.selectedAutoMake + "&";

      if (this.selectedFuelType != null)
        params += "fuelTypeId=" + this.selectedFuelType + "&";

      if (this.priceFrom != null) params += "priceFrom=" + this.priceFrom + "&";

      if (this.priceTo != null) params += "priceTo=" + this.priceTo + "&";

      if (this.yearFrom != null) params += "yearFrom=" + this.yearFrom + "&";

      if (this.yearTo != null) params += "yearTo=" + this.yearTo + "&";

      if (this.gearBox != null) params += "gearbox=" + this.gearBox + "&";

      if (this.selectedAutoModel != null) params += "autoModel=" + this.selectedAutoModel + "&";

      if (this.vin != null)
        params += "hasVin=" + (this.vin == true ? 1 : 0) + "&";

      console.log(params);

      window.location.href = "/search?" + params;
    },
    setFiltersToLastSearch() {
      //var fuelTypeId = this.$route.query.fuelTypeId;
      var priceFrom = this.$route.query.priceFrom;
      var priceTo = this.$route.query.priceTo;
      var yearFrom = this.$route.query.yearFrom;
      var yearTo = this.$route.query.yearTo;
      this.vin = this.$route.query.hasVin == 1 ? true : false;
      this.selectedAutoModel = this.$route.query.autoModel;

      this.priceFrom = priceFrom;
      this.priceTo = priceTo;
      this.yearFrom = yearFrom;
      this.yearTo = yearTo;
    },
  },
  computed: {},
  created() {
    axios
      .get("/api/Auto/GetAutoMakes")
      .then((response) => (this.makeList = response.data));
    axios
      .get("/api/Auto/GetFuelTypes")
      .then((response) => (this.fuelTypeList = response.data));

    this.setFiltersToLastSearch();
  },
};
</script>