using INVEST.BUSINESSLOGIC.Models;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Factory.Interfaces
{
    public interface IOrderFactory
    {
        Task<OrderOutput?> OrderClient(OrderInput input);
    }
}
