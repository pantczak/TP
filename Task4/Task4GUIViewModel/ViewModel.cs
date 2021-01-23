using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public ICommand UpdateCommand { get; set; }
        public ICommand LoactionInfoCommand { get; set; }

        #region constructors

        public ViewModel()
        {
            GetAllDataCommand = new RelayCommand(() => _locationServiceModel = new LocationsServiceModel());

        }

        public ViewModel(LocationsServiceModel locations)
        {
            _locationServiceModel = locations;
        }

        #endregion

        #region API

        #endregion

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region private variables

        private LocationsServiceModel _locationServiceModel;
        private LocationModel _location;
        private LocationModel _locationInfo;
        private ObservableCollection<LocationModel> _locations;
        private ObservableCollection<LocationModel> _locationsInfo;

        #endregion
    }
}