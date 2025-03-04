using INVEST.BUSINESSLOGIC.Constants;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Service.Interface;
using Newtonsoft.Json;

namespace INVEST.SITE.Service
{
    public class ProductService : IProductService
    {
        private readonly IRestService _restService;

        public ProductService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<ProductOutput?> GetAllProducts()
        {
            var output = new ProductOutput();

            var response = await _restService.ExecuteGetAsync(url: RestPath.GetAllProducts);

            output = JsonConvert.DeserializeObject<ProductOutput>(response);

            return output;
        }
    }
}
