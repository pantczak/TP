using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Task3;
using Task3.Database;

namespace Task3Tests
{
    [TestClass]
    public class ExtensionToolClassTest
    {
        [TestMethod]
        public void GetProductsWithNoCategory()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<Product> results = productsList.GetProductsWithNoCategory();

                foreach (Product result in results)
                {
                    Assert.AreEqual(result.ProductSubcategory, null);
                }

                Assert.AreEqual(209, results.Count);
            }
        }

        [TestMethod]
        public void GetProductsWithNoCategoryImperative()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<Product> results = productsList.GetProductsWithNoCategoryImperative();

                foreach (Product result in results)
                {
                    Assert.AreEqual(result.ProductSubcategory, null);
                }

                Assert.AreEqual(209, results.Count);
            }
        }

        [TestMethod]
        public void GetProductsAsPage()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<Product> results = productsList.GetProductsAsPage(1, 3);

                Assert.AreEqual(3, results.Count);
                Assert.AreEqual("Adjustable Race", results[0].Name);
                Assert.AreEqual("Bearing Ball", results[1].Name);
                Assert.AreEqual("BB Ball Bearing", results[2].Name);
            }
        }

        [TestMethod]
        public void GetProductsAsPageImperative()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<Product> results = productsList.GetProductsAsPageImperative(1, 3);
                Assert.AreEqual(3, results.Count);
                Assert.AreEqual("Adjustable Race", results[0].Name);
                Assert.AreEqual("Bearing Ball", results[1].Name);
                Assert.AreEqual("BB Ball Bearing", results[2].Name);
            }
        }

        [TestMethod]
        public void GetProductVendorPairs()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<ProductVendor> productVendorsList = context.GetTable<ProductVendor>().ToList();
                string data = productsList.GetProductVendorAsPair(productVendorsList);
                Assert.AreEqual(460, data.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Count());
            }
        }

        [TestMethod]
        public void GetProductVendorPairsImperative()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<ProductVendor> productVendorsList = context.GetTable<ProductVendor>().ToList();
                string data = productsList.GetProductVendorAsPairImperative(productVendorsList);
                Assert.AreEqual(460, data.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Count());
            }
        }
    }
}