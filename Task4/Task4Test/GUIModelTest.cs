using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Task4GUIModel;
using Task4Service.ServiceClasses;

namespace Task4Test
{
    [TestClass]
    public class GuiModelTest
    {
        private IDataRepository _repository;
        private LocationsServiceModel _model;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new TestDataRepository();
            _model = new LocationsServiceModel(_repository);
        }

        [TestMethod]
        public void GetLocationTest()
        {
            LocationModel locationModel = _model.Get(0);
            Assert.AreEqual(locationModel.Id, 0);
            Assert.AreEqual(locationModel.Name, "Location1");
            Assert.AreEqual(locationModel.CostRate, 0.5m);
            Assert.AreEqual(locationModel.Availability, 1.0m);
        }

        [TestMethod]
        public void DeleteLocationTest()
        {
            int preDelete = _model.GetAll().Count;
            _model.Delete(0);
            int postDelete = _model.GetAll().Count;
            Assert.AreEqual(preDelete - 1, postDelete);
        }

        [TestMethod]
        public void AddLocationTest()
        {
            LocationModel location = new LocationModel(5, "Location5", 3.0m, 1.1m, DateTime.Now);
            int preAdd = _model.GetAll().Count;
            _model.Add(location);
            int postAdd = _model.GetAll().Count;
            Assert.AreEqual(preAdd +1, postAdd);
        }

        [TestMethod]
        public void UpdateLocationTest()
        {
            LocationModel location = new LocationModel(0, "LocationUpdate", 3.0m, 1.1m, DateTime.Now);
            _model.Update(location);
            Assert.AreEqual(location.Id, _model.GetAll()[0].Id);
            Assert.AreEqual(location.Name, _model.GetAll()[0].Name);
            Assert.AreEqual(location.CostRate, _model.GetAll()[0].CostRate);
            Assert.AreEqual(location.Availability, _model.GetAll()[0].Availability);
        }
    }
}