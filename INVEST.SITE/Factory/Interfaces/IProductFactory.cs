using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Factory.Interfaces
{
    public interface IProductFactory
    {
        Task<ProductOutput> GetAllProducts();
    }
}
