using INVEST.BUSINESSLOGIC.Models;

namespace INVEST.API.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task<int> GetLoginClient(LoginClient input);

        Task<List<Client>> GetClients(int? idClient = null, string? nameClient = null);
    }
}
