using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task4Data.Database;
using Task4Service.ClassWrapper;

namespace Task4Service.ServiceClasses
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private static readonly DataSourceDataContext _context = new DataSourceDataContext();

        public DataRepository()
        {
        }

        public void CreateLocation(LocationPlaceholder locationPlaceholder)
        {
            _context.Locations.InsertOnSubmit(locationPlaceholder.GetLocation());
            _context.SubmitChanges();
        }

        public LocationPlaceholder ReadLocation(int locationId)
        {
            Location result =
                _context.Locations.FirstOrDefault(location => location.LocationID == locationId);

            return new LocationPlaceholder(result);
        }

        public void DeleteLocation(int locationId)
        {
            _context.Locations.DeleteOnSubmit(ReadLocation(locationId).GetLocation());
            _context.SubmitChanges();
        }

        public void UpdateLocation(LocationPlaceholder location)
        {
            Location locationToUpdate = _context.Locations.FirstOrDefault(loc =>
                loc.LocationID == location.GetLocation().LocationID);
            if (locationToUpdate != null)
                foreach (PropertyInfo info in locationToUpdate.GetType().GetProperties())
                {
                    if (info.CanWrite)
                    {
                        info.SetValue(locationToUpdate, info.GetValue(location.GetLocation()));
                    }
                }
            else
            {
                throw new Exception();
            }
        }

        public IEnumerable<LocationPlaceholder> ReadAllLocations()
        {
            List<Location> locations = new List<Location>(_context.Locations);
            List<LocationPlaceholder> placeholders = new List<LocationPlaceholder>();
            foreach (var location in locations)
            {
                placeholders.Add(new LocationPlaceholder(location));
            }

            return placeholders;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}