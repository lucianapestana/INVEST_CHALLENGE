using INVEST.API.Repository.Interfaces;
using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service
{
    public class OrderService : IOrderService
    {
        private readonly IAccountClientService _accountClientService;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;

        public OrderService
            (
                IAccountClientService accountClientService,
                IProductService productService,
                IOrderRepository orderRepository
            )
        {
            _accountClientService = accountClientService;
            _productService = productService;
            _orderRepository = orderRepository;
        }

        public async Task<OrderOutput> OrderClient(OrderInput input)
        {
            try
            {
                var output = new OrderOutput();

                var outputProduct = await _productService.ValidateProductStockOrder(input.ProductId, input.ProductQuantity);

                if (outputProduct.Errors != null)
                {
                    output.Errors.AddRange(outputProduct.Errors);
                    return output;
                }

                if (outputProduct.Product != null && outputProduct.Product.TotalProduct.HasValue)
                {
                    var outputAccount = await _accountClientService.ValidateBalanceAccountClientOrder(input.AccountClientId, outputProduct.Product.TotalProduct.Value);

                    if (outputAccount.Errors != null)
                    {
                        output.Errors.AddRange(outputAccount.Errors);
                        return output;
                    }

                    if (outputAccount.AccountClient != null)
                    {
                        output.Order = new Order()
                        {
                            AccountClientId = input.AccountClientId,
                            Balance = outputAccount.AccountClient.Balance,
                            ProductId = input.ProductId,
                            Stock = outputProduct.Product.Stock
                        };

                        var outputOrder = await _orderRepository.UpdateBalanceClientAndStockProduct(output.Order);

                        if (!outputOrder)
                        {
                            output.Errors = new List<Error>() { new Error() { Message = "Ocorreu um erro ao atualizar os registros." } };
                        }
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os registros", ex);
            }
        }
    }
}
