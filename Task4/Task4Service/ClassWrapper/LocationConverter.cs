using System;
using Task4Data.Database;

namespace Task4Service.ClassWrapper
{
    public static class LocationConverter
    {
        public static LocationPlaceholder CreateNewLocationPlaceholder(string id, string name, string costRate, string availability)
        {
            Location location = new Location
            {
                LocationID = short.Parse(id),
                Name = name,
                Availability = decimal.Parse(availability),
                CostRate = decimal.Parse(costRate),
                ModifiedDate = DateTime.Now
            };

            return new LocationPlaceholder(location);
        }

        public static LocationPlaceholder CreateNewLocationPlaceholder(short id, string name, decimal costRate, decimal availability)
        {
            Location location = new Location
            {
                LocationID = id,
                Name = name,
                Availability = availability,
                CostRate = costRate,
                ModifiedDate = DateTime.Now
            };

            return new LocationPlaceholder(location);
        }
    }
}