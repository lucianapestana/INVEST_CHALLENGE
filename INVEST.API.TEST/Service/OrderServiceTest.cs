using INVEST.API.Service;
using INVEST.API.TEST.Repository;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using Moq;
using Xunit;

namespace INVEST.API.TEST.Service
{
    public class OrderServiceTest
    {
        //OrderClient
        private ApiMockRepository _mock;
        private OrderService _service;

        public OrderServiceTest()
        {
            _mock = new ApiMockRepository();
            _service = new OrderService
                (
                    _mock.AccountClientService.Object,
                    _mock.ProductService.Object,
                    _mock.OrderRepository.Object
                );
        }

        [Theory(DisplayName = "Submit the client's order - Success.")]
        [InlineData(1, 1, 10)]
        [Trait("OrderService", "OrderClient")]
        public async Task OrderClient_Success(int accountClientId, int productId, int productQuantity)
        {
            // Arrange
            var _input = new OrderInput()
            {
                AccountClientId = accountClientId,
                ProductId = productId,
                ProductQuantity = productQuantity
            };

            _mock.ProductService.Setup(x => x.ValidateProductStockOrder(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(new ProductOutput() { Product = new Product() { TotalProduct = 100 } });

            _mock.AccountClientService.Setup(x => x.ValidateBalanceAccountClientOrder(It.IsAny<int>(), It.IsAny<decimal>()))
            .ReturnsAsync(new AccountClientOutput() { AccountClient = new AccountClient() { AccountClientId = 1 } });

            _mock.OrderRepository.Setup(x => x.UpdateBalanceClientAndStockProduct(It.IsAny<Order>()))
            .ReturnsAsync(true);

            // Act
            var result = await _service.OrderClient(_input);
            var _outputOrder = result.Order;

            // Assert
            Assert.True(result != null);
            Assert.True(_outputOrder?.AccountClientId > 0 && _outputOrder.ProductId > 0);
            Assert.True(_outputOrder.Balance >= 0 && _outputOrder.Stock >= 0);
        }

        [Theory(DisplayName = "Submit the client's order - Error.")]
        [InlineData(1, 1, 200, 100, "A quantidade informada é maior do que a disponível em estoque.")]
        [InlineData(1, 1, 20, 2000, "Saldo insuficiente! O total da compra é maior que o saldo disponível.")]
        [InlineData(1, 0, 20, 100, "Ocorreu um erro ao atualizar os registros.")]
        [Trait("OrderService", "OrderClient")]
        public async Task OrderClient_Error(int accountClientId, int productId, int productQuantity, int totalProducts, string msgError)
        {
            // Arrange
            var _input = new OrderInput()
            {
                AccountClientId = accountClientId,
                ProductId = productId,
                ProductQuantity = productQuantity
            };

            var _order = new Order()
            {
                AccountClientId = accountClientId,
                Balance = 1000,
                ProductId = productId,
                Stock = 50
            };

            if (_order.Stock < productQuantity)
            {
                _mock.ProductService.Setup(x => x.ValidateProductStockOrder(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ProductOutput() { Errors = new List<Error>() { new Error() { Message = "A quantidade informada é maior do que a disponível em estoque." } } });
            }
            else
            {
                _mock.ProductService.Setup(x => x.ValidateProductStockOrder(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ProductOutput() { Product = new Product() { TotalProduct = 100 } });
            }

            if (_order.Balance < totalProducts)
            {
                _mock.AccountClientService.Setup(x => x.ValidateBalanceAccountClientOrder(It.IsAny<int>(), It.IsAny<decimal>()))
                .ReturnsAsync(new AccountClientOutput() { Errors = new List<Error>() { new Error() { Message = "Saldo insuficiente! O total da compra é maior que o saldo disponível." } } });
            }
            else
            {
                _mock.AccountClientService.Setup(x => x.ValidateBalanceAccountClientOrder(It.IsAny<int>(), It.IsAny<decimal>()))
                .ReturnsAsync(new AccountClientOutput() { AccountClient = new AccountClient() { AccountClientId = 1 } });
            }

            if (productId == 0)
            {
                _mock.OrderRepository.Setup(x => x.UpdateBalanceClientAndStockProduct(It.IsAny<Order>()))
                .ReturnsAsync(false);
            }
            else
            {
                _mock.OrderRepository.Setup(x => x.UpdateBalanceClientAndStockProduct(It.IsAny<Order>()))
                .ReturnsAsync(true);
            }

            // Act
            var result = await _service.OrderClient(_input);

            // Assert
            Assert.True(result.Errors.Any());
            Assert.Equal(msgError, result.Errors?.FirstOrDefault()?.Message);
        }
    }
}
