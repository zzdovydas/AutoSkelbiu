using System;

namespace AutoSkelbiu.Models
{
    public class Auto
    {
        public int? ID { get; set; }
        public int? LINK_ID { get; set; }
        public AutoSpecification SPECIFICATION { get; set; }
        public TruckSpecification TRUCK_SPECIFICATION { get; set; }
        public int PRICE { get; set; }
        public int MILEAGE { get; set; }
        public string WHEEL_SIZE { get; set; }
        public string SDK { get; set; }
        public string DEFECT { get; set; }
        public DateTime CREATED_AT { get; set; }
        public string THUMBNAIL { get; set; }
        public string LINK_URL { get; set; }
        public string VIN { get; set; }
    }
}
