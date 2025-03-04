using INVEST.API.Repository.Interfaces;
using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientOutput> GetLoginClient(LoginClient input)
        {
            try
            {
                var output = new ClientOutput();

                int id = await _clientRepository.GetLoginClient(input);

                if (id > 0)
                {
                    output.LoginClient = new Client() { ClientId = id };
                };

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar o login", ex);
            }
        }

        public async Task<ClientOutput> GetClients(int? idClient = null, string? nameClient = null)
        {
            try
            {
                var output = new ClientOutput();

                output.Clients = await _clientRepository.GetClients(idClient, nameClient);

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }
    }
}
