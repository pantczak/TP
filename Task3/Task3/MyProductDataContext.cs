using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Database;

namespace Task3
{
    public class MyProductDataContext
    {
        public static List<MyProduct> MyProducts { get; private set; }


        public MyProductDataContext(DataClasses1DataContext productsList)
        {
            MyProducts = productsList.Products.AsEnumerable().Select(product => new MyProduct(product)).ToList();
        }
    }
}
