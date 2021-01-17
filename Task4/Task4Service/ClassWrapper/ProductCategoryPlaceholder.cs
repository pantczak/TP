using System;
using Task4Data.Database;

namespace Task4Service.ClassWrapper
{
    public class ProductCategoryPlaceholder
    {
        private readonly ProductCategory _productCategory;

        public ProductCategoryPlaceholder(ProductCategory productCategory)
        {
            _productCategory = productCategory;
        }

        internal ProductCategory GetProductCategory()
        {
            return this._productCategory;
        }

        public int ProductCategoryId => _productCategory.ProductCategoryID;

        public string Name
        {
            get => _productCategory.Name;
            set => _productCategory.Name = value;
        }

        public DateTime ModifiedDate
        {
            get => _productCategory.ModifiedDate;
            set => _productCategory.ModifiedDate = value;
        }
    }
}