using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Task4GUIModel;
using Task4GUIViewModel.MVVMLight;

namespace Task4GUIViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GetAllDataCommand { get; set; }
        public ICommand AddLocationCommand { get; set; }
        public ICommand DeleteLocationCommand { get; set; }
        public ICommand UpdateLocationCommand { get; set; }
        public ICommand LocationInfoCommand { get; set; }

        public bool DisableTasks { get; set; }


        #region constructors

        public ViewModel()
        {
            Location = new LocationModel();
            _locationServiceModel = new LocationsServiceModel();
            GetAllDataCommand = new RelayCommand(ReloadLocations);
            AddLocationCommand = new RelayCommand(AddLocation);
            DeleteLocationCommand = new RelayCommand(RemoveLocation);
            UpdateLocationCommand = new RelayCommand(UpdateLocation);
        }

        public ViewModel(IServiceModel locationsServiceModel)
        {
            Location = new LocationModel();
            _locationServiceModel = locationsServiceModel;
            GetAllDataCommand = new RelayCommand(ReloadLocations);
            Locations = new ObservableCollection<LocationModel>(_locationServiceModel.GetAll());
            AddLocationCommand = new RelayCommand(AddLocation);
            DeleteLocationCommand = new RelayCommand(RemoveLocation);
            UpdateLocationCommand = new RelayCommand(UpdateLocation);
        }

        #endregion

        #region API

        public IServiceModel LocationServiceModel
        {
            get => _locationServiceModel;
            set
            {
                _locationServiceModel = value;
                _locations = new ObservableCollection<LocationModel>(value.GetAll());
            }
        }

        public LocationModel Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LocationModel> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        private void AddLocation()
        {
            ExecuteTask((() =>
            {
                LocationModel model = new LocationModel((short) (_locationServiceModel.GetAll().Last().Id + 100),
                    Location.Name,
                    Location.CostRate, Location.Availability, DateTime.Now);
                _locationServiceModel.Add(model);
            }));
        }

        private void RemoveLocation()
        {
          ExecuteTask(() => { _locationServiceModel.Delete(_location.Id); });
        }

        private void UpdateLocation()
        {
            ExecuteTask(() =>
            {
                LocationModel model = new LocationModel(Location.Id, Location.Name, Location.CostRate, Location.Availability, DateTime.Now);
                _locationServiceModel.Update(model);
            });
        }

        private void ReloadLocations()
        {
            Locations = new ObservableCollection<LocationModel>(_locationServiceModel.GetAll());
            Location = new LocationModel();
        }

        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExecuteTask(Action a)
        {
            if (DisableTasks)
            {
                a();
            }
            else
            {
                Task.Run(a);
            }
        }

        #region private variables

        private IServiceModel _locationServiceModel;
        private LocationModel _location;
        private ObservableCollection<LocationModel> _locations;


        #endregion
    }
}