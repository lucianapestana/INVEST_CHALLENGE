using System.Text;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;
using INVEST.SITE.Factory.Interfaces;
using INVEST.SITE.Service.Interface;

namespace INVEST.SITE.Factory
{
    public class ClientFactory : IClientFactory
    {
        private readonly IClientService _clientService;

        public ClientFactory(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<ClientOutput?> ClientLogin(LoginClient input)
        {
            var output = new ClientOutput();

            output = await _clientService.ClientLogin(input);

            return output;
        }

        public async Task<ClientOutput?> GetClientById(string idClient)
        {

            byte[] bytes = Convert.FromBase64String(idClient);
            string idClientDecrypt = Encoding.UTF8.GetString(bytes);

            var output = new ClientOutput();

            output = await _clientService.GetClientById(idClientDecrypt);

            return output;
        }
    }
}
