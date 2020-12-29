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

                foreach (var result in results)
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

                foreach (var result in results)
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
                List<Product> results = productsList.GetProductsAsPage(1, 20);
                Assert.AreEqual(results.Count, 20);

                //TODO improve
            }
        }

        [TestMethod]
        public void GetProductsAsPageImperative()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productsList = context.GetTable<Product>().ToList();
                List<Product> results = productsList.GetProductsAsPageImperative(1, 20);
                Assert.AreEqual(results.Count, 20);

                //TODO improve
            }
        }

        //TODO add string tests
    }
}