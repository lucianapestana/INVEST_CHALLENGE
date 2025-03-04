using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductOutput> GetProducts(int? productId = null);

        Task<ProductOutput> ValidateProductStockOrder(int productId, int productQuantity);
    }
}
