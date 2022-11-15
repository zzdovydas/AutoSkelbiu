using System;

namespace AutoSkelbiu.Models
{
    public class Auto
    {
        public int? ID { get; set; }
        public int? LINK_ID { get; set; }
        public string MAKE { get; set; }
        public string MODEL { get; set; }
        public int PRICE { get; set; }
        public DateTime MADE_IN { get; set; }
        public int MILEAGE { get; set; }
        public string ENGINE { get; set; }
        public string FUEL_TYPE { get; set; }
        public string TYPE { get; set; }
        public string DOOR_NUMBER { get; set; }
        public string GEARBOX { get; set; }
        public string CLIMATE_CONTROL { get; set; }
        public string COLOR { get; set; }
        public string STEERING_WHEEL_SIDE { get; set; }
        public string WHEEL_SIZE { get; set; }
        public string SEATS { get; set; }
        public string EMISSIONS { get; set; }
        public string LENGTH_TYPE { get; set; }
        public string HEIGHT_TYPE { get; set; }
        public string SDK { get; set; }
        public string WHEEL_DRIVE { get; set; }
        public string DEFECT { get; set; }
        public DateTime CREATED_AT { get; set; }
        public string THUMBNAIL { get; set; }
        public string LINK_URL { get; set; }
        public string VIN { get; set; }
    }
}
