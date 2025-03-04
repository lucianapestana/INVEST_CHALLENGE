using INVEST.BUSINESSLOGIC.Constants;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Service.Interface;
using Newtonsoft.Json;

namespace INVEST.SITE.Service
{
    public class ClientService : IClientService
    {
        private readonly IRestService _restService;
       
        public ClientService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<ClientOutput?> ClientLogin(LoginClient input)
        {
            var output = new ClientOutput();

            var response = await _restService.ExecutePostAsync(url: RestPath.PostLoginPath, payload: input);

            output = JsonConvert.DeserializeObject<ClientOutput>(response);

            return output;
        }

        public async Task<ClientOutput?> GetClientById(string idClient)
        {
            var output = new ClientOutput();

            var response = await _restService.ExecuteGetAsync(url: string.Format(RestPath.GetClientPath, idClient));

            output = JsonConvert.DeserializeObject<ClientOutput>(response);

            return output;
        }
    }
}
