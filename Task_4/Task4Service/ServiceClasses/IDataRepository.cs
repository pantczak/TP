using System.Collections.Generic;
using Task4Data.Database;
using Task4Service.ClassWrapper;

namespace Task4Service.ServiceClasses
{
    public interface IDataRepository
    {
        void CreateLocation(LocationPlaceholder location);
        LocationPlaceholder ReadLocation(int locationId);
        void DeleteLocation(int locationId);
        void UpdateLocation(LocationPlaceholder location);
        IEnumerable<LocationPlaceholder> ReadAllLocations();
    }
}