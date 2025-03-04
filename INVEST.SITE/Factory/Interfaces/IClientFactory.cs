using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Factory.Interfaces
{
    public interface IClientFactory
    {
        Task<ClientOutput?> ClientLogin(LoginClient input);

        Task<ClientOutput?> GetClientById(string idClient);
    }
}
