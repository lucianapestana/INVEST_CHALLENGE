using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Service.Interface
{
    public interface IOrderService
    {
        Task<OrderOutput?> OrderClient(OrderInput input);
    }
}
