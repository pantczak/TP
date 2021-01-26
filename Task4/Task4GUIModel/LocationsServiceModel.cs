using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Task4Service.ClassWrapper;
using Task4Service.ServiceClasses;

namespace Task4GUIModel
{
    public class LocationsServiceModel : IServiceModel
    {
        private readonly IDataRepository _repository;

        public LocationsServiceModel(IDataRepository dataRepository)
        {
            this._repository = dataRepository;
        }

        public LocationsServiceModel() : this(new DataRepository())
        {
        }

        public void Add(LocationModel location)
        {
            _repository.CreateLocation(LocationConverter.CreateNewLocationPlaceholder(location.Id, location.Name,
                location.CostRate, location.Availability));
        }

        public LocationModel Get(short locationId)
        {
            LocationPlaceholder placeholder = _repository.ReadLocation(locationId);
            return new LocationModel(placeholder.LocationId, placeholder.Name, placeholder.CostRate,
                placeholder.Availability, placeholder.ModifiedDate);
        }

        public void Delete(short locationId)
        {
          _repository.DeleteLocation(locationId);
        }

        public void Update(LocationModel locationModelToUpdate)
        {
            _repository.UpdateLocation(LocationConverter.CreateNewLocationPlaceholder(locationModelToUpdate.Id,
                locationModelToUpdate.Name,
                locationModelToUpdate.CostRate, locationModelToUpdate.Availability));
        }

        public ObservableCollection<LocationModel> GetAll()
        {
            ObservableCollection<LocationModel> models = new ObservableCollection<LocationModel>();

            foreach (LocationPlaceholder location in _repository.ReadAllLocations())
            {
                models.Add(new LocationModel(location.LocationId, location.Name, location.CostRate,
                    location.Availability, location.ModifiedDate));
            }

            return models;
        }
    }
}