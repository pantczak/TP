using System;
using System.Collections.Generic;

namespace Task4GUIModel
{
    public class LocationModel
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public decimal CostRate { get; set; }
        public decimal Availability { get; set; }
        public DateTime ModifiedDate { get; set; }

        public LocationModel()
        {
        }

        public LocationModel(short locationId, string name, decimal costRate, decimal availability,
            DateTime modifiedDate)
        {
            this.Id = locationId;
            this.Name = name;
            this.CostRate = costRate;
            this.Availability = availability;
            this.ModifiedDate = modifiedDate;
        }
    }
}