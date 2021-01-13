using System.Collections.Generic;
using Task4Data.Database;

namespace Task4Service.ServiceClasses
{
    public interface IDataRepository
    {
        void CreateProduct(Product product);
        Product ReadProduct(int productId);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
        IEnumerable<Product> ReadAllProducts();
    }
}