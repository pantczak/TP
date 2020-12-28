using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Task3.Database;

namespace Task3
{
    public static class ToolClass
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<Product> productList = new List<Product>(from product in productionDataContext.Products
                where product.Name.Contains(namePart)
                select product);
            return productList;
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<Product> productList = new List<Product>(from product in productionDataContext.Products
                join productVendor in productionDataContext.ProductVendors on product.ProductID equals
                    productVendor.ProductID
                where productVendor.Vendor.Name == vendorName
                select product);
            return productList;
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<string> productNameList = new List<string>(from product in productionDataContext.Products
                join productVendor in productionDataContext.ProductVendors on product.ProductID equals
                    productVendor.ProductID
                where productVendor.Vendor.Name == vendorName
                select product.Name);
            return productNameList;
        }

        public static string GetProductVendorByProductName(string productName)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            return (from product in productionDataContext.Products
                join productVendor in productionDataContext.ProductVendors on product.ProductID equals productVendor
                    .ProductID
                where product.Name == productName
                select productVendor.Vendor.Name).First();
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<Product> productList = new List<Product>(from product in productionDataContext.Products
                where product.ProductReviews.Count == howManyReviews
                select product);
            return productList;
        }

        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<Product> productList = new List<Product>((from review in productionDataContext.ProductReviews
                orderby review.ReviewDate descending
                group review.Product by review.ProductID
                into tuple
                select tuple.First()).Take(howManyProducts));
            return productList;
        }

        public static int GetTotalStandardCostByCategory(ProductCategory category)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            return (int) (from product in productionDataContext.Products
                where product.ProductSubcategory.ProductCategory.Name == category.Name
                select product.StandardCost).Sum();
        }
    }
}