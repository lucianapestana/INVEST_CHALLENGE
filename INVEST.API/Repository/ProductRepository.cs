using INVEST.API.DATA.Context;
using INVEST.API.Repository.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using Microsoft.EntityFrameworkCore;

namespace INVEST.API.Repository
{
    public class ProductRepository(InvestContext _context) : IProductRepository
    {
        private readonly InvestContext _context = _context;

        public async Task<List<Product>> GetProducts(int? productId = null)
        {
            try
            {
                var result = await (
                    from products in _context.TB_PRODUCTS
                    where
                    (
                        (
                            productId.HasValue
                            && products.PRODUCT_ID == productId
                        ) || !productId.HasValue
                    )
                    orderby products.TAX descending
                    select new Product()
                    {
                        ProductId = products.PRODUCT_ID,
                        BondAsset = products.BOND_ASSET,
                        Index = products.INDEX,
                        Tax = products.TAX,
                        IssuerName = products.ISSUER_NAME,
                        UnitPrice = products.UNIT_PRICE,
                        Stock = products.STOCK
                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }

        public async Task<bool> UpdateProduct(Product input)
        {
            try
            {
                var productExists = await _context.TB_PRODUCTS.FindAsync(input.ProductId);

                if (productExists != null)
                {
                    productExists.BOND_ASSET = input.BondAsset;
                    productExists.INDEX = input.Index;
                    productExists.TAX = input.Tax;
                    productExists.ISSUER_NAME = input.IssuerName;
                    productExists.UNIT_PRICE = input.UnitPrice;
                    productExists.STOCK = input.Stock;

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os registros.", ex);
            }
        }
    }
}
