using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Database;

namespace Task3
{
    class MyProductDataContext
    {
        public static List<MyProduct> MyProducts { get; private set; }


        public MyProductDataContext(List<Product> productsList)
        {
            MyProducts = productsList.Select(product => new MyProduct(product)).ToList();
        }
    }
}
