using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Sockets;
using Task3.Database;

namespace Task3
{
    public static class ToolClass
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productList = new List<Product>(from product in context.Products
                    where product.Name.Contains(namePart)
                    select product);
                return productList;
            }
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productList = new List<Product>(from product in context.Products
                    join productVendor in context.ProductVendors on product.ProductID equals
                        productVendor.ProductID
                    where productVendor.Vendor.Name == vendorName
                    select product);
                return productList;
            }
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<string> productNameList = new List<string>(from product in context.Products
                    join productVendor in context.ProductVendors on product.ProductID equals
                        productVendor.ProductID
                    where productVendor.Vendor.Name == vendorName
                    select product.Name);
                return productNameList;
            }
        }

        public static string GetProductVendorByProductName(string productName)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                return (from product in context.Products
                    join productVendor in context.ProductVendors on product.ProductID equals productVendor
                        .ProductID
                    where product.Name == productName
                    select productVendor.Vendor.Name).First();
            }
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productList = new List<Product>(from product in context.Products
                    where product.ProductReviews.Count == howManyReviews
                    select product);
                return productList;
            }
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productList = new List<Product>((from review in context.ProductReviews
                    orderby review.ReviewDate descending
                    group review.Product by review.ProductID
                    into tuple
                    select tuple.First()).Take(howManyProducts));
                return productList;
            }
        }

        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                List<Product> productList = new List<Product>((from product in context.Products
                    where product.ProductSubcategory.ProductCategory.Name.Equals(categoryName)
                    select product).Take(n).ToList());

                return productList;
            }
        }


        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            using (DataClasses1DataContext context = new DataClasses1DataContext())
            {
                return (int) (from product in context.Products
                    where product.ProductSubcategory.ProductCategory.Name == category.Name
                    select product.StandardCost).Sum();
            }
        }
    }
}