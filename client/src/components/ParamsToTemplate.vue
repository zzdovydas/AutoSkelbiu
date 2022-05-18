<template>
  <div class="mt-5">
    <span class="fw-bold">Parametrai:</span>
    <div>
      <table class="table">
        <tbody>
          <tr>
            <td v-if="paramList.autO_MAKE != null">Markė</td>
            <td v-if="paramList.autO_MAKE != null">
              {{ paramList.autO_MAKE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_MODEL != null">Modelis</td>
            <td v-if="paramList.autO_MODEL != null">
              {{ paramList.autO_MODEL }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_MADE_IN != null">Pagaminimo data</td>
            <td v-if="paramList.autO_MADE_IN != null">
              {{ paramList.autO_MADE_IN }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_ENGINE != null">Variklis</td>
            <td v-if="paramList.autO_ENGINE != null">
              {{ paramList.autO_ENGINE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_MILEAGE != null">Rida</td>
            <td v-if="paramList.autO_MILEAGE != null">
              {{ paramList.autO_MILEAGE }} km
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_FUEL_TYPE != null">Kuro tipas</td>
            <td v-if="paramList.autO_FUEL_TYPE != null">
              {{ paramList.autO_FUEL_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_TYPE != null">Kėbulo tipas</td>
            <td v-if="paramList.autO_TYPE != null">
              {{ paramList.autO_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_DOOR_NUMBER != null">Durų skaičius</td>
            <td v-if="paramList.autO_DOOR_NUMBER != null">
              {{ paramList.autO_DOOR_NUMBER }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_GEARBOX != null">Pavarų dėžė</td>
            <td v-if="paramList.autO_GEARBOX != null">
              {{ paramList.autO_GEARBOX }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_CLIMATE_CONTROL != null">
              Klimato kontrolė
            </td>
            <td v-if="paramList.autO_CLIMATE_CONTROL != null">
              {{ paramList.autO_CLIMATE_CONTROL }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_COLOR != null">Spalva</td>
            <td v-if="paramList.autO_COLOR != null">
              {{ paramList.autO_COLOR }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_STEERING_WHEEL_SIDE != null">
              Vairo padėtis
            </td>
            <td v-if="paramList.autO_STEERING_WHEEL_SIDE != null">
              {{ paramList.autO_STEERING_WHEEL_SIDE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_WHEEL_SIZE != null">Ratlankių dydis</td>
            <td v-if="paramList.autO_WHEEL_SIZE != null">
              {{ paramList.autO_WHEEL_SIZE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_SEATS != null">Sėdimų vietų skaičius</td>
            <td v-if="paramList.autO_SEATS != null">
              {{ paramList.autO_SEATS }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_EMISSIONS != null">
              Išmetamų dujų skaičius
            </td>
            <td v-if="paramList.autO_EMISSIONS != null">
              {{ paramList.autO_EMISSIONS }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_LENGTH_TYPE != null">Ilgis</td>
            <td v-if="paramList.autO_LENGTH_TYPE != null">
              {{ paramList.autO_LENGTH_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">Aukštis</td>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">
              {{ paramList.autO_HEIGHT_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_SDK != null">SDK kodas</td>
            <td v-if="paramList.autO_SDK != null">{{ paramList.autO_SDK }}</td>
          </tr>
          <tr>
            <td v-if="paramList.autO_WHEEL_DRIVE != null">Varomieji ratai</td>
            <td v-if="paramList.autO_WHEEL_DRIVE != null">
              {{ paramList.autO_WHEEL_DRIVE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_DEFECT != null">Defektai</td>
            <td v-if="paramList.autO_DEFECT != null">
              {{ paramList.autO_DEFECT }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">Aukštis</td>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">
              {{ paramList.autO_HEIGHT_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">Aukštis</td>
            <td v-if="paramList.autO_HEIGHT_TYPE != null">
              {{ paramList.autO_HEIGHT_TYPE }}
            </td>
          </tr>
          <tr>
            <td v-if="paramList.autO_VIN != ''">VIN</td>
            <td v-if="paramList.autO_VIN != ''">
              {{ paramList.autO_VIN }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: "ParamsToTemplate",
  props: ["paramList"],
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