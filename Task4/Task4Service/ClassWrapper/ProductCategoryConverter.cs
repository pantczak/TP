using System;
using Task4Data.Database;

namespace Task4Service.ClassWrapper
{
    public static class ProductCategoryConverter
    {
        public static ProductCategoryPlaceholder NewCategoryPlaceholder(int productCategoryId, string name,
            DateTime modifiedDate)
        {
            ProductCategory category = new ProductCategory
            {
                ProductCategoryID = productCategoryId,
                Name = name,
                ModifiedDate = modifiedDate,
                rowguid = new Guid()
            };
            return new ProductCategoryPlaceholder(category);
        }
    }
}