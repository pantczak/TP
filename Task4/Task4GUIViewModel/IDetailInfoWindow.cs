using System.ComponentModel;

namespace Task4GUIViewModel
{
    public interface IDetailInfoWindow
    {
        void ShowInfoWindow<T>(T viewModel) where T : INotifyPropertyChanged;
    }
}