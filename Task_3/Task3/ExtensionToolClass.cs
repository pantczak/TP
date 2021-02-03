using System.Collections.Generic;
using System.Linq;
using Task3.Database;

namespace Task3
{
    public static class ExtensionToolClass
    {
        public static List<Product> GetProductsWithNoCategory(this List<Product> productsList)
        {
            List<Product> productsWithNoCategoryList = (from product in productsList
                where product.ProductSubcategory == null
                select product).ToList();

            return productsWithNoCategoryList;
        }

        public static List<Product> GetProductsWithNoCategoryImperative(this List<Product> productsList)
        {
            return productsList.Where(prod => prod.ProductSubcategory == null).ToList();
        }


        public static List<Product> GetProductsAsPage(this List<Product> productsList, int pageNumber,
            int productsPerPage)
        {
            List<Product> productsPage = new List<Product>(from product in productsList
                select product).Skip(productsPerPage * (pageNumber - 1)).Take(productsPerPage).ToList();
            return productsPage;
        }

        public static List<Product> GetProductsAsPageImperative(this List<Product> productsList, int pageNumber,
            int productsPerPage)
        {
            List<Product> productsPage = new List<Product>(productsList.Skip(productsPerPage * (pageNumber - 1))
                .Take(productsPerPage).ToList());

            return productsPage;
        }

        public static string GetProductVendorAsPair(this List<Product> productsList, List<ProductVendor> productVendorsList)
        {
            var query = (from product in productsList
                from productVendor in productVendorsList
                where productVendor.ProductID.Equals(product.ProductID)
                select product.Name + " - " + productVendor.Vendor.Name).ToList();

            string result = "";

            foreach (var line in query)
            {
                result += line + '\n';
            }

            return result;
        }

        public static string GetProductVendorAsPairImperative(this List<Product> productsList, List<ProductVendor> productVendorsList)
        {
            var query = productsList.Join(productVendorsList,
                product => product.ProductID,
                productVendor => productVendor.ProductID,
                (product, productVendor) => product.Name + " - " + productVendor.Vendor.Name).ToList();

            string result = "";

            foreach (var line in query)
            {
                result += line + '\n';
            }

            return result;
        }

        
    }
}