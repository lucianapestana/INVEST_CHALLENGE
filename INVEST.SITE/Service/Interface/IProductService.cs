using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.SITE.Service.Interface
{
    public interface IProductService
    {
        Task<ProductOutput?> GetAllProducts();
    }
}
