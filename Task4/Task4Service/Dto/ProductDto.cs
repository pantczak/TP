using Task4Data.Database;

namespace Task4Service.Dto
{
    public class ProductDto
    {
        private Product _product;

        public ProductDto(Product product)
        {
            _product = product;
        }
    }
}