using INVEST.API.Service;
using INVEST.API.TEST.Repository;
using INVEST.BUSINESSLOGIC.Models;
using Moq;
using Xunit;

namespace INVEST.API.TEST.Service
{
    public class ProductServiceTest
    {
        private ApiMockRepository _mock;
        private ProductService _service;

        public ProductServiceTest()
        {
            _mock = new ApiMockRepository();
            _service = new ProductService(_mock.ProductRepository.Object);
        }

        [Theory(DisplayName = "Get the products - Success.")]
        [InlineData(1)]
        [Trait("ProductService", "GetProducts")]
        public async Task GetProducts_Success(int? productId)
        {
            // Arrange
            _mock.ProductRepository.Setup(x => x.GetProducts(It.IsAny<int?>()))
            .ReturnsAsync(new List<Product>() { new Product() { ProductId = 1 } });

            // Act
            var result = await _service.GetProducts(productId);

            // Assert
            Assert.True(result != null);
            Assert.True(result?.Products?.Count > 0);
        }

        [Theory(DisplayName = "Get the products - Error.")]
        [InlineData(1)]
        [Trait("ProductService", "GetProducts")]
        public async Task GetProducts_Error(int? productId)
        {
            // Arrange
            _mock.ProductRepository.Setup(x => x.GetProducts(It.IsAny<int?>()))
            .ThrowsAsync(new Exception("Ocorreu um erro ao obter os registros"));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetProducts(productId));

            Assert.Equal("Ocorreu um erro ao obter os registros", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("Ocorreu um erro ao obter os registros", exception.InnerException.Message);
        }

        [Theory(DisplayName = "Validate the product stock in the order - Success.")]
        [InlineData(1, 2)]
        [Trait("ProductService", "ValidateProductStockOrder")]
        public async Task ValidateStockOrder_Product_Success(int productId, int productQuantity)
        {
            // Arrange
            _mock.ProductRepository.Setup(x => x.GetProducts(It.IsAny<int>()))
            .ReturnsAsync(new List<Product>() { new Product() { ProductId = 1, Stock = 20 } });

            // Act
            var result = await _service.ValidateProductStockOrder(productId, productQuantity);

            // Assert
            Assert.True(result?.Products?.Count > 0);
            Assert.True(result?.Products?.FirstOrDefault()?.Stock >= 0);
            Assert.False(result.Errors.Count() > 0);
        }

        [Theory(DisplayName = "Validate the product stock in the order - Error Stock.")]
        [InlineData(1, 50)]
        [Trait("ProductService", "ValidateProductStockOrder")]
        public async Task ValidateStockOrder_Product_StockError(int productId, int productQuantity)
        {
            // Arrange
            _mock.ProductRepository.Setup(x => x.GetProducts(It.IsAny<int?>()))
            .ReturnsAsync(new List<Product>() { new Product() { ProductId = 1, Stock = 10 } });

            // Act
            var result = await _service.ValidateProductStockOrder(productId, productQuantity);

            // Assert
            Assert.True(result.Errors.Any());
            Assert.Equal("A quantidade informada é maior do que a disponível em estoque.", result.Errors.First().Message);
        }

        [Theory(DisplayName = "Validate the product stock in the order - Error Invalid Product.")]
        [InlineData(0, 100)]
        [Trait("ProductService", "ValidateProductStockOrder")]
        public async Task ValidateStockOrder_Product_ProductIdError(int productId, int productQuantity)
        {
            // Arrange
            _mock.ProductRepository.Setup(x => x.GetProducts(It.IsAny<int?>()))
            .ReturnsAsync(new List<Product>());

            // Act
            var result = await _service.ValidateProductStockOrder(productId, productQuantity);

            // Assert
            Assert.True(result?.Products?.Count == 0);
        }
    }
}
