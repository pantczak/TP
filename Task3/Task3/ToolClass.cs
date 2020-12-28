using System.Collections.Generic;
using System.Linq;
using Task3.Database;

namespace Task3
{
    public static class ToolClass
    {
        public static List<Product> GetProductsByName(string namePart)
        {
            DataClasses1DataContext productionDataContext = new DataClasses1DataContext();
            List<Product> productsList = new List<Product>(from product in productionDataContext.Products
                where product.Name.Contains(namePart)
                select product);
            return productsList;
        }
    }
}