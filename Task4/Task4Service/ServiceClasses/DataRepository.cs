using System;
using System.Linq;
using System.Net.Sockets;
using Task4Data.Database;

namespace Task4Service.ServiceClasses
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private readonly DataSourceDataContext _context;

        public DataRepository(DataSourceDataContext context)
        {
            _context = context;
        }

        public bool CreateProduct(Product product)
        {
            try
            {
                _context.Products.InsertOnSubmit(product);
                _context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Product ReadProduct(int productId)
        {
            IQueryable<Product> result =
                from product in _context.Products where product.ProductID == productId select product;

            return !result.Any() ? null : result.First();
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                _context.Products.DeleteOnSubmit(ReadProduct(productId));
                _context.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                Product productToUpdate = ReadProduct(product.ProductID);
                productToUpdate.Name = product.Name;
                productToUpdate.ProductNumber = product.ProductNumber;
                productToUpdate.MakeFlag = product.MakeFlag;
                productToUpdate.FinishedGoodsFlag = product.FinishedGoodsFlag;
                productToUpdate.Color = product.Color;
                productToUpdate.SafetyStockLevel = product.SafetyStockLevel;
                productToUpdate.ReorderPoint = product.ReorderPoint;
                productToUpdate.StandardCost = product.StandardCost;
                productToUpdate.ListPrice = product.ListPrice;
                productToUpdate.Size = product.Size;
                productToUpdate.SizeUnitMeasureCode = product.SizeUnitMeasureCode;
                productToUpdate.WeightUnitMeasureCode = product.WeightUnitMeasureCode;
                productToUpdate.Weight = product.Weight;
                productToUpdate.DaysToManufacture = product.DaysToManufacture;
                productToUpdate.ProductLine = product.ProductLine;
                productToUpdate.Class = product.Class;
                productToUpdate.Style = product.Style;
                productToUpdate.ProductSubcategoryID = product.ProductSubcategoryID;
                productToUpdate.ProductModelID = product.ProductModelID;
                productToUpdate.SellStartDate = product.SellStartDate;
                productToUpdate.SellEndDate = product.SellEndDate;
                productToUpdate.DiscontinuedDate = product.DiscontinuedDate;
                productToUpdate.rowguid = product.rowguid;
                productToUpdate.ModifiedDate = DateTime.Today;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose() //TODO remove or keep
        {
            _context?.Dispose();
        }
    }
}