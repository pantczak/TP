using System.Collections.ObjectModel;

namespace Task4GUIModel
{
    public interface IServiceModel
    {
         void Add(LocationModel location);
         LocationModel Get(short locationId);
         void Delete(short locationId);
         void Update(LocationModel locationModelToUpdate);
         ObservableCollection<LocationModel> GetAll();
    }
}