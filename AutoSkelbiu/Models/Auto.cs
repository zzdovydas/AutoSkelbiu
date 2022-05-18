using System;

namespace AutoSkelbiu.Models
{
    public class Auto
    {
        public int? AUTO_ID { get; set; }
        public int? LINK_ID { get; set; }
        public string AUTO_MAKE { get; set; }
        public string AUTO_MODEL { get; set; }
        public int AUTO_PRICE { get; set; }
        public DateTime AUTO_MADE_IN { get; set; }
        public int AUTO_MILEAGE { get; set; }
        public string AUTO_ENGINE { get; set; }
        public string AUTO_FUEL_TYPE { get; set; }
        public string AUTO_TYPE { get; set; }
        public string AUTO_DOOR_NUMBER { get; set; }
        public string AUTO_GEARBOX { get; set; }
        public string AUTO_CLIMATE_CONTROL { get; set; }
        public string AUTO_COLOR { get; set; }
        public string AUTO_STEERING_WHEEL_SIDE { get; set; }
        public string AUTO_WHEEL_SIZE { get; set; }
        public string AUTO_SEATS { get; set; }
        public string AUTO_EMISSIONS { get; set; }
        public string AUTO_LENGTH_TYPE { get; set; }
        public string AUTO_HEIGHT_TYPE { get; set; }
        public string AUTO_SDK { get; set; }
        public string AUTO_WHEEL_DRIVE { get; set; }
        public string AUTO_DEFECT { get; set; }
        public DateTime CREATED_AT { get; set; }
        public string THUMBNAIL { get; set; }
        public string LINK_URL { get; set; }
        public string AUTO_VIN { get; set; }
    }
}
