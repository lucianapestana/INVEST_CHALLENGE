using INVEST.API.Repository.Interfaces;
using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOutput> GetProducts(int? productId = null)
        {
            try
            {
                var output = new ProductOutput();

                output.Products = await _productRepository.GetProducts(productId: productId);

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }

        public async Task<ProductOutput> ValidateProductStockOrder(int productId, int productQuantity)
        {
            try
            {
                var output = new ProductOutput();

                output.Products = await _productRepository.GetProducts(productId: productId);

                if (output.Products != null && output.Products.Count > 0)
                {
                    output.Product = output.Products.FirstOrDefault();

                    var newStock = output.Product.Stock - productQuantity;

                    if (newStock < 0)
                    {
                        output.Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Message = "A quantidade informada é maior do que a disponível em estoque."
                            }
                        };

                        return output;
                    }

                    output.Product.Stock = newStock;

                    var totalStock = output.Product.UnitPrice * productQuantity;
                    output.Product.TotalProduct = totalStock;
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao validar os registros", ex);
            }
        }
    }
}
