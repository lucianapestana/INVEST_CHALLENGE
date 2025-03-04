using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service.Interfaces
{
    public interface IClientService
    {
        Task<ClientOutput> GetLoginClient(LoginClient input);

        Task<ClientOutput> GetClients(int? idClient = null, string? nameClient = null);
    }
}
