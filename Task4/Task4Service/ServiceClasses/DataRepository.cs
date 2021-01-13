using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using Task4Data.Database;

namespace Task4Service.ServiceClasses
{
    public class DataRepository : IDataRepository
    {
        private readonly DataSourceDataContext _context;

        public DataRepository(DataSourceDataContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            Task.Run(() =>
            {
                _context.Products.InsertOnSubmit(product);
                _context.SubmitChanges();
            });
        }

        public Product ReadProduct(int productId)
        {
            IQueryable<Product> result =
                from product in _context.Products where product.ProductID == productId select product;

            return !result.Any() ? null : result.First();
        }

        public void DeleteProduct(int productId)
        {
            Task.Run(() =>
            {
                _context.Products.DeleteOnSubmit(ReadProduct(productId));
                _context.SubmitChanges();
            });
        }

        public void UpdateProduct(Product product)
        {
            Task.Run(() =>
            {
                Product productToUpdate = _context.Products.FirstOrDefault(prod => prod.ProductID == product.ProductID);
                if (productToUpdate != null)
                    foreach (PropertyInfo info in productToUpdate.GetType().GetProperties())
                    {
                        if (info.CanWrite)
                        {
                            info.SetValue(productToUpdate, info.GetValue(product));
                        }
                    }
            });
        }

        public IEnumerable<Product> ReadAllProducts()
        {
            List<Product> productsList = new List<Product>(_context.Products);
            return productsList;
        }
    }
}