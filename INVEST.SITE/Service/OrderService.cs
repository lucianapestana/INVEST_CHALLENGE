using INVEST.BUSINESSLOGIC.Constants;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Service.Interface;
using Newtonsoft.Json;

namespace INVEST.SITE.Service
{
    public class OrderService: IOrderService
    {
        private readonly IRestService _restService;

        public OrderService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<OrderOutput?> OrderClient(OrderInput input)
        {
            var output = new OrderOutput();

            var response = await _restService.ExecutePostAsync(url: RestPath.PostOrderPath, payload: input);

            output = JsonConvert.DeserializeObject<OrderOutput>(response);

            return output;
        }

    }
}
