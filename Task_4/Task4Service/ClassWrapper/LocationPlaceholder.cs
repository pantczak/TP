using System;
using Task4Data.Database;

namespace Task4Service.ClassWrapper
{
    public class LocationPlaceholder
    {
        private readonly Location _location;

        public LocationPlaceholder(Location location)
        {
            this._location = location;
        }

        internal Location GetLocation()
        {
            return this._location;
        }

        public short LocationId => _location.LocationID;

        public string Name
        {
            get => _location.Name;
            set => _location.Name = value;
        }

        public decimal CostRate
        {
            get => _location.CostRate;
            set => _location.CostRate = value;
        }

        public decimal Availability
        {
            get => _location.Availability;
            set => _location.Availability = value;
        }

        public DateTime ModifiedDate
        {
            get => _location.ModifiedDate;
            set => _location.ModifiedDate = value;
        }
    }
}