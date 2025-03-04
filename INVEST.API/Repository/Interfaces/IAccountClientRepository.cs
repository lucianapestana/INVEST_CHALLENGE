using INVEST.BUSINESSLOGIC.Models;

namespace INVEST.API.Repository.Interfaces
{
    public interface IAccountClientRepository
    {
        Task<List<AccountClient>> GetAccountsClients
            (
                int? accountClientId = null,
                string? account = null,
                int? clientId = null
            );

        Task<bool> UpdateAccountClient(AccountClient input);
    }
}
