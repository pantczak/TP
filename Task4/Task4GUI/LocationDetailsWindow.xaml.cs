using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task4GUIViewModel;

namespace Task4GUI
{
    /// <summary>
    /// Logika interakcji dla klasy LocationDetailsWindow.xaml
    /// </summary>
    public partial class LocationDetailsWindow : Window,IDetailInfoWindow
    {
        public LocationDetailsWindow()
        {
            InitializeComponent();
        }

        public void ShowInfoWindow<T>(T viewModel) where T : INotifyPropertyChanged
        {
            var window = new LocationDetailsWindow();
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
