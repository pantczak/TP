using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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


        public IDetailInfoWindow InfoWindow { get; set; }
        public IMessageBox MessageBox { get; set; }

        #region constructors

        public ViewModel()
        {
            _locationServiceModel = new LocationsServiceModel();
            //GetAllDataCommand = new RelayCommand(() => _locationServiceModel = new LocationsServiceModel());
            GetAllDataCommand = new RelayCommand(() =>
                Locations = new ObservableCollection<LocationModel>(_locationServiceModel.GetAll()));
            AddLocationCommand = new RelayCommand(AddLocation);
            DeleteLocationCommand = new RelayCommand(RemoveLocation);
            UpdateLocationCommand = new RelayCommand(UpdateLocation);
            LocationInfoCommand = new RelayCommand(GetInfo);
        }

        public ViewModel(IServiceModel locationsServiceModel)
        {
            _locationServiceModel = locationsServiceModel;
            GetAllDataCommand = new RelayCommand(() =>
                Locations = new ObservableCollection<LocationModel>(_locationServiceModel.GetAll()));
            Locations = new ObservableCollection<LocationModel>(_locationServiceModel.GetAll());
            AddLocationCommand = new RelayCommand(AddLocation);
            DeleteLocationCommand = new RelayCommand(RemoveLocation);
            UpdateLocationCommand = new RelayCommand(UpdateLocation);
            LocationInfoCommand = new RelayCommand(GetInfo);
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

        public LocationModel LocationInfo
        {
            get => _locationInfo;
            set
            {
                _locationInfo = value;
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

        public ObservableCollection<LocationModel> LocationsInfo
        {
            get => _locationsInfo;
            set
            {
                _locationsInfo = value;
                OnPropertyChanged();
            }
        }

        private void AddLocation()
        {
            LocationModel model = new LocationModel(0, Name, CostRate, Availability, DateTime.Today);
            if (string.IsNullOrEmpty(model.Name) || model.CostRate < 0)
            {
                MessageBox.Show("Incorrect value", "ERROR");
            }
            else
            {
                _locationServiceModel.Add(model);
            }
        }

        private void RemoveLocation()
        {
            if (_location.Id == 0)
            {
                MessageBox.Show("Incorrect value", "ERROR");
            }
            else
            {
                _locationServiceModel.Delete(_location.Id);
            }
        }

        private void UpdateLocation()
        {
            LocationModel model = new LocationModel(0, Name, CostRate, Availability, DateTime.Today);
            if (string.IsNullOrEmpty(model.Name) || model.CostRate < 0 || model.Id == 0)
            {
                MessageBox.Show("Incorrect value", "ERROR");
            }
            else
            {
                _locationServiceModel.Update(model);
            }
        }

        private void GetInfo()
        {
            if (_location == null)
            {
                MessageBox.Show("Select location first", "ERROR");
            }
            else
            {
                _locationsInfo = new ObservableCollection<LocationModel> {_locationServiceModel.Get(_location.Id)};
                _locationInfo = _locationServiceModel.Get(_location.Id);
                InfoWindow.ShowInfoWindow(this);
            }
        }

        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region private variables

        private IServiceModel _locationServiceModel;
        private LocationModel _location;
        private LocationModel _locationInfo;
        private ObservableCollection<LocationModel> _locations;
        private ObservableCollection<LocationModel> _locationsInfo;
        public decimal Availability { get; set; }
        public decimal CostRate { get; set; }
        public string Name { get; set; }
        public short Id { get; set; }

        #endregion
    }
}