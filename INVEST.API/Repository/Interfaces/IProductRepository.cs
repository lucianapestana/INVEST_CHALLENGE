using INVEST.BUSINESSLOGIC.Models;

namespace INVEST.API.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts(int? productId = null);

        Task<bool> UpdateProduct(Product input);
    }
}
