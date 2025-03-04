using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Factory.Interfaces;
using INVEST.SITE.Service.Interface;

namespace INVEST.SITE.Factory
{
    public class OrderFactory : IOrderFactory
    {
        private readonly IOrderService _orderService;

        public OrderFactory(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<OrderOutput?> OrderClient(OrderInput input)
        {
            try
            {
                var output = new OrderOutput();

                output = await _orderService.OrderClient(input);

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os registros", ex);
            }
        }
    }
}
