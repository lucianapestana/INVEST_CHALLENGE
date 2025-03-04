using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service.Interfaces
{
    public interface IAccountClientService
    {
        Task<AccountClientOutput> ValidateBalanceAccountClientOrder(int accountClientId, decimal totalProduct);
    }
}
