using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Task4Data.Database;
using Task4Service.ClassWrapper;

namespace Task4Service.ServiceClasses
{
    public class DataRepository : IDataRepository
    {
        private readonly DataSourceDataContext _context;

        public DataRepository(DataSourceDataContext context)
        {
            _context = context;
        }

        public void CreateProduct(ProductCategoryPlaceholder productCategoryPlaceholder)
        {
            Task.Run(() =>
            {
                _context.ProductCategories.InsertOnSubmit(productCategoryPlaceholder.GetProductCategory());
                _context.SubmitChanges();
            });
        }

        public ProductCategoryPlaceholder ReadProduct(int productCategoryId)
        {
            ProductCategory result =
                _context.ProductCategories.FirstOrDefault(cat => cat.ProductCategoryID == productCategoryId);

            return new ProductCategoryPlaceholder(result);
        }

        public void DeleteProduct(int productId)
        {
            Task.Run(() =>
            {
                _context.ProductCategories.DeleteOnSubmit(ReadProduct(productId).GetProductCategory());
                _context.SubmitChanges();
            });
        }

        public void UpdateProduct(ProductCategoryPlaceholder productCategory)
        {
            Task.Run(() =>
            {
                ProductCategory productCategoryToUpdate = _context.ProductCategories.FirstOrDefault(category =>
                    category.ProductCategoryID == productCategory.GetProductCategory().ProductCategoryID);
                if (productCategoryToUpdate != null)
                    foreach (PropertyInfo info in productCategoryToUpdate.GetType().GetProperties())
                    {
                        if (info.CanWrite)
                        {
                            info.SetValue(productCategoryToUpdate, info.GetValue(productCategory));
                        }
                    }
            });
        }

        public IEnumerable<ProductCategoryPlaceholder> ReadAllProducts()
        {
            List<ProductCategory> productCategories = new List<ProductCategory>(_context.ProductCategories);
            List<ProductCategoryPlaceholder> placeholders = new List<ProductCategoryPlaceholder>();
            foreach (var productCategory in productCategories)
            {
                placeholders.Add(new ProductCategoryPlaceholder(productCategory));
            }
            return placeholders;
        }
    }
}