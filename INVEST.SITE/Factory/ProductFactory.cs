using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Factory.Interfaces;
using INVEST.SITE.Service.Interface;

namespace INVEST.SITE.Factory
{
    public class ProductFactory : IProductFactory
    {
        private readonly IProductService _productService;

        public ProductFactory(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<ProductOutput> GetAllProducts()
        {
            var output = new ProductOutput();

            output = await _productService.GetAllProducts();

            return output;
        }
    }
}
