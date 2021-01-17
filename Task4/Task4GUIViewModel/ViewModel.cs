using System.ComponentModel;
using System.Runtime.CompilerServices;
using Task4GUIModel;

namespace Task4GUIViewModel
{
    public class ViewModel :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IModel _dataModel;
        
        private 

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
