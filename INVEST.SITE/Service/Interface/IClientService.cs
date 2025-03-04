using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Service.Interface
{
    public interface IClientService
    {
        Task<ClientOutput?> ClientLogin(LoginClient input);

        Task<ClientOutput?> GetClientById(string idClient);
    }
}
