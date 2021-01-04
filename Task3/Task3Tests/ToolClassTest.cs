using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Task3;
using Task3.Database;

namespace Task3Tests
{
    [TestClass]
    public class ToolClassTest
    {
        [TestMethod]
        public void GetProductsByName()
        {
            List<Product> list = ToolClass.GetProductsByName("Hex Nut");

            Assert.AreEqual(39, list.Count);

            foreach (Product product in list)
            {
                Assert.IsTrue(product.Name.Contains("Hex Nut"));
            }
        }

        [TestMethod]
        public void GetProductsByVendorName()
        {
            List<Product> list = ToolClass.GetProductsByVendorName("Trikes, Inc.");

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Mountain Tire Tube", list[0].Name);
            Assert.AreEqual("HL Mountain Tire", list[1].Name);
        }

        [TestMethod]
        public void GetProductNamesByVendorName()
        {
            List<string> list = ToolClass.GetProductNamesByVendorName("Trikes, Inc.");
            Assert.AreEqual("Mountain Tire Tube", list[0]);
            Assert.AreEqual("HL Mountain Tire", list[1]);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void GetProductVendorByProductName()
        {
            string name = ToolClass.GetProductVendorByProductName("Bearing Ball");

            Assert.AreEqual("Wood Fitness", name);
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews()
        {
            List<Product> list = ToolClass.GetProductsWithNRecentReviews(2);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("HL Mountain Pedal", list[0].Name);
        }

        [TestMethod]
        public void GetNRecentlyReviewedProducts()
        {
            List<Product> list = ToolClass.GetNRecentlyReviewedProducts(3);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("HL Mountain Pedal", list[0].Name);
            Assert.AreEqual("Road-550-W Yellow, 40", list[1].Name);
            Assert.AreEqual("Mountain Bike Socks, M", list[2].Name);
        }

        [TestMethod]
        public void NProductsFromCategory()
        {
            List<Product> list = ToolClass.GetNProductsFromCategory("Bikes", 3);

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual("Mountain-100 Silver, 38", list[0].Name);
            Assert.AreEqual("Mountain-100 Silver, 42", list[1].Name);
            Assert.AreEqual("Mountain-100 Silver, 44", list[2].Name);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategory()
        {
            double totalCost = ToolClass.GetTotalStandardCostByCategory(new ProductCategory {Name = "Clothing" });
            Assert.AreEqual(868, totalCost);
        }
    }
}