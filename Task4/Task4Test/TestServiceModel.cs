using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Task4GUIModel;

namespace Task4Test
{
    internal class TestServiceModel : IServiceModel
    {
        readonly List<LocationModel> _list = new List<LocationModel>();

        public TestServiceModel()
        {
            _list.Add(new LocationModel(1, "Location1", 0.5m, 1.0m, DateTime.Now));
            _list.Add(new LocationModel(2, "Location2", 1.5m, 1.0m, DateTime.Now));
            _list.Add(new LocationModel(3, "Location3", 2.5m, 1.0m, DateTime.Now));
            _list.Add(new LocationModel(4, "Location4", 3.5m, 1.0m, DateTime.Now));
        }

        public void Add(LocationModel location)
        {
            _list.Add(location);
        }

        public LocationModel Get(short locationId)
        {
            return _list[locationId];
        }

        public void Delete(short locationId)
        {
            _list.RemoveAt(locationId);
        }

        public void Update(LocationModel locationModelToUpdate)
        {
            _list[locationModelToUpdate.Id] = locationModelToUpdate;
        }

        public ObservableCollection<LocationModel> GetAll()
        {
            return new ObservableCollection<LocationModel>(_list);
        }
    }
}