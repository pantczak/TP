using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Task4Data.Database;
using Task4Service.ClassWrapper;

namespace Task4Service.ServiceClasses
{
    public class DataRepository : IDataRepository
    {
        private readonly DataSourceDataContext _context;

        public DataRepository(DataSourceDataContext context)
        {
            _context = context;
        }

        public DataRepository()
        {
            _context = new DataSourceDataContext();
        }

        public void CreateLocation(LocationPlaceholder locationPlaceholder)
        {
            Task.Run(() =>
            {
                _context.Locations.InsertOnSubmit(locationPlaceholder.GetLocation());
                _context.SubmitChanges();
            });
        }

        public LocationPlaceholder ReadLocation(int locationId)
        {
            Location result =
                _context.Locations.FirstOrDefault(location => location.LocationID == locationId);

            return new LocationPlaceholder(result);
        }

        public void DeleteLocation(int locationId)
        {
            Task.Run(() =>
            {
                _context.Locations.DeleteOnSubmit(ReadLocation(locationId).GetLocation());
                _context.SubmitChanges();
            });
        }

        public void UpdateLocation(LocationPlaceholder location)
        {
            Task.Run(() =>
            {
                Location locationToUpdate = _context.Locations.FirstOrDefault(loc =>
                    loc.LocationID == location.GetLocation().LocationID);
                if (locationToUpdate != null)
                    foreach (PropertyInfo info in locationToUpdate.GetType().GetProperties())
                    {
                        if (info.CanWrite)
                        {
                            info.SetValue(locationToUpdate, info.GetValue(location));
                        }
                    }
            });
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
    }
}