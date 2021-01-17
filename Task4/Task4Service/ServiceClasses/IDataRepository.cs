using System.Collections.Generic;
using Task4Data.Database;
using Task4Service.ClassWrapper;

namespace Task4Service.ServiceClasses
{
    public interface IDataRepository
    {
        void CreateProduct(ProductCategoryPlaceholder productCategory);
        ProductCategoryPlaceholder ReadProduct(int productCategoryId);
        void DeleteProduct(int productCategoryId);
        void UpdateProduct(ProductCategoryPlaceholder productCategory);
        IEnumerable<ProductCategoryPlaceholder> ReadAllProducts();
    }
}