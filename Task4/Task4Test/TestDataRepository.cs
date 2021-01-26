using System.Collections.Generic;
using Task4Service.ClassWrapper;
using Task4Service.ServiceClasses;

namespace Task4Test
{
    public class TestDataRepository : IDataRepository
    {
        private List<LocationPlaceholder> Locations { get; }

        private static List<LocationPlaceholder> SetupList()
        {
            List<LocationPlaceholder> list = new List<LocationPlaceholder>
            {
                LocationConverter.CreateNewLocationPlaceholder(0, "Location1", 0.5m, 1.0m),
                LocationConverter.CreateNewLocationPlaceholder(1, "Location2", 1.0m, 1.1m),
                LocationConverter.CreateNewLocationPlaceholder(2, "Location3", 1.5m, 1.2m),
                LocationConverter.CreateNewLocationPlaceholder(3, "Location4", 2.0m, 1.3m),
                LocationConverter.CreateNewLocationPlaceholder(4, "Location5", 2.5m, 1.4m)
            };
            return list;
        }

        public TestDataRepository()
        {
            Locations = SetupList();
        }

        public void CreateLocation(LocationPlaceholder location)
        {
            Locations.Add(location);
        }

        public LocationPlaceholder ReadLocation(int locationId)
        {
            return Locations[locationId];
        }

        public void DeleteLocation(int locationId)
        {
            Locations.RemoveAt(locationId);
        }

        public void UpdateLocation(LocationPlaceholder location)
        {
            Locations[location.LocationId] = location;
        }

        public IEnumerable<LocationPlaceholder> ReadAllLocations()
        {
            return Locations;
        }
    }
}