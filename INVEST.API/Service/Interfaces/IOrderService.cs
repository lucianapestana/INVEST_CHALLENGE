using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderOutput> OrderClient(OrderInput input);
    }
}
