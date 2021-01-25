using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4GUIViewModel;

namespace Task4Test
{
    [TestClass]
    public class ViewModelTest
    {
        private ViewModel _viewModel;

        [TestInitialize()]
        public void Init()
        {
            _viewModel = new ViewModel(new TestServiceModel());

        }
    }
}