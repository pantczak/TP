using System;
using Task4Data.Database;

namespace Task4Service.ClassWrapper
{
    public static class LocationConverter
    {

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