using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task3;
using Task3.Database;

namespace Task3Tests
{
    [TestClass]
    public class MyProductToolsTest
    {
        [TestMethod]
        public void GetProductsByName()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                MyProductDataContext myContext = new MyProductDataContext(context);
                List<MyProduct> list = MyProductTools.GetProductsByName("Hex Nut");

                Assert.AreEqual(39, list.Count);

                foreach (MyProduct product in list)
                {
                    Assert.IsTrue(product.Name.Contains("Hex Nut"));
                }
            }
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                MyProductDataContext myContext = new MyProductDataContext(context);
                List<MyProduct> list = MyProductTools.GetProductsWithNRecentReviews(2);
                Assert.AreEqual(1, list.Count);
            }
        }


        [TestMethod]
        public void NProductsFromCategory()
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                MyProductDataContext myContext = new MyProductDataContext(context);
                List<MyProduct> list = MyProductTools.GetNProductsFromCategory("Bikes", 3);

                Assert.AreEqual(3, list.Count);
            }
        }
    }
}
