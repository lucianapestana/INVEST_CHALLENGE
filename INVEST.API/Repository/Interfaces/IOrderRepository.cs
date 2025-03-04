using INVEST.BUSINESSLOGIC.Models;

namespace INVEST.API.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> UpdateBalanceClientAndStockProduct(Order input);
    }
}
