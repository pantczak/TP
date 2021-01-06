using Task4Data.Database;

namespace Task4Service.ServiceClasses
{
    public interface IDataRepository
    {
        bool CreateProduct(Product product);
        Product ReadProduct(int productId);
        bool DeleteProduct(int productId);
        bool UpdateProduct(Product product);
    }
}