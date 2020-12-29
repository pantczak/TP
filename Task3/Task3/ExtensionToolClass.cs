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

        //TODO ADD STRING RETURN 
    }
}