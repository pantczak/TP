using System;
using System.Collections.Generic;
using Task4Service.ClassWrapper;

namespace Task4GUIModel
{
    public interface IModel
    {
        void AddProductCategory(int productCategoryId, string name,
            DateTime modifiedDate);

        void DeleteProductCategory(int productCategoryId);
        List<ProductCategoryPlaceholder> GetAllProductCategories();
        ProductCategoryPlaceholder GetProductCategory(int productCategoryId);
        void UpdateProductCategory(int productCategoryId, string name,
            DateTime modifiedDate);
    }
}