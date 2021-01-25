using System.Collections.Generic;
using System.Collections.ObjectModel;
using Task4Service.ClassWrapper;
using Task4Service.ServiceClasses;

namespace Task4GUIModel
{
    public class LocationsServiceModel : IServiceModel
    {
        private readonly IDataRepository _repository;
        // public ObservableCollection<LocationModel> LocationModels { get; set; }
        // public LocationModel CurrentLocationModel { get; set; }

        public LocationsServiceModel(IDataRepository dataRepository)
        {
            this._repository = dataRepository;
            // LocationModels = new ObservableCollection<LocationModel>();
            // InitCollection();
        }

        public LocationsServiceModel() : this(new DataRepository())
        {
        }

        // private void InitCollection()
        // {
        //     foreach (LocationPlaceholder location in _repository.ReadAllLocations())
        //     {
        //         LocationModels.Add(new LocationModel(location.LocationId, location.Name, location.CostRate,
        //             location.Availability, location.ModifiedDate));
        //     }
        // }

        // public void ReloadLocations()
        // {
        //     LocationModels.Clear();
        //     InitCollection();
        // }

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