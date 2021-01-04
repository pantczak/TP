using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Database;

namespace Task3
{
    class MyProductTools
    {
        public static List<MyProduct> GetProductsByName(string namePart)
        {
                List<MyProduct> productList = new List<MyProduct>(from product in MyProductDataContext.MyProducts
                                                              where product.Name.Contains(namePart)
                                                              select product);
                return productList;

        }
        public static List<MyProduct> GetProductsWithNRecentReviews(int howManyReviews)
        {

                List<MyProduct> productList = new List<MyProduct>(from product in MyProductDataContext.MyProducts
                                                              where product.ProductReviews.Count == howManyReviews
                                                              select product);
                return productList;
        }

        public static List<MyProduct> GetNProductsFromCategory(string categoryName, int n)
        {
                List<MyProduct> productList = new List<MyProduct>((from product in MyProductDataContext.MyProducts
                                                               where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                                                               select product).Take(n).ToList());

                return productList;
        }
    }
        }
    }
}
