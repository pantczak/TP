using System;
using System.Collections.Generic;
using Task4Service.ClassWrapper;
using Task4Service.ServiceClasses;

namespace Task4GUIModel
{
    public class ProperProductCategoryModel : IModel
    {
        private readonly IDataRepository _repository;

        public ProperProductCategoryModel()
        {
            _repository = new DataRepository();
        }

        public void AddProductCategory(int productCategoryId, string name,
            DateTime modifiedDate)
        {
            _repository.CreateProduct(
                ProductCategoryConverter.NewCategoryPlaceholder(productCategoryId, name, modifiedDate));
        }

        public void DeleteProductCategory(int productCategoryId)
        {
            _repository.DeleteProduct(productCategoryId);
        }

        public List<ProductCategoryPlaceholder> GetAllProductCategories()
        {
            return (List<ProductCategoryPlaceholder>) _repository.ReadAllProducts();
        }

        public void UpdateProductCategory(int productCategoryId, string name,
            DateTime modifiedDate)
        {
            _repository.UpdateProduct(
                ProductCategoryConverter.NewCategoryPlaceholder(productCategoryId, name, modifiedDate));
        }

        public ProductCategoryPlaceholder GetProductCategory(int productCategoryId)
        {
            return _repository.ReadProduct(productCategoryId);
        }
    }
}