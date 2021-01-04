using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Database;

namespace Task3
{
    public class MyProduct : Product
    {
        public MyProduct(Product product)
        {
            foreach (var property in product.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(this, property.GetValue(product));
                }
            }
        }
    }
}
