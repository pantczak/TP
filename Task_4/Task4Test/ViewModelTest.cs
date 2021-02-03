using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task4GUIModel;
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
            _viewModel = new ViewModel(new TestServiceModel()) {DisableTasks = true};
        }


        [TestMethod]
        public void AllCountTest()
        {
            int x = _viewModel.Locations.Count;
            Assert.AreEqual(4, x);
        }

        [TestMethod]
        public void AddLocationTest()
        {
            int counter = _viewModel.Locations.Count;

            _viewModel.Location.Name = "Old Warehouse";
            _viewModel.Location.CostRate = 0.5m;
            _viewModel.Location.Availability = 0.5m;
            _viewModel.AddLocationCommand.Execute(null);

            _viewModel.GetAllDataCommand.Execute(null);
            Assert.AreEqual("Old Warehouse", _viewModel.Locations[counter].Name);
            Assert.AreEqual(counter + 1, _viewModel.Locations.Count);

        }


        [TestMethod]
        public void DeleteLocationTest()
        {
            int counter = _viewModel.Locations.Count;

            _viewModel.Location = _viewModel.Locations[1];
            _viewModel.DeleteLocationCommand.Execute(null);
            _viewModel.GetAllDataCommand.Execute(null);

            Assert.AreEqual(counter - 1, _viewModel.Locations.Count);
        }

        [TestMethod]
        public void UpdateLocationTest()
        {
            LocationModel model = _viewModel.Locations[2];
            _viewModel.Location.Name = "Change";
            _viewModel.Location.CostRate = 1.2m;
            _viewModel.Location.Availability = 0.22m;
            _viewModel.Location.Id = model.Id;

            Assert.AreNotEqual(model.Name, _viewModel.Location.Name);
            Assert.AreNotEqual(model.CostRate, _viewModel.Location.CostRate);
            Assert.AreNotEqual(model.Availability, _viewModel.Location.Availability);
            Assert.AreEqual(model.Id, _viewModel.Location.Id);

            _viewModel.UpdateLocationCommand.Execute(null);
            _viewModel.GetAllDataCommand.Execute(null);

            Assert.AreEqual("Change", _viewModel.Locations[2].Name );
            Assert.AreEqual(1.2m, _viewModel.Locations[2].CostRate);
            Assert.AreEqual(0.22m, _viewModel.Locations[2].Availability);

        }


    }
}