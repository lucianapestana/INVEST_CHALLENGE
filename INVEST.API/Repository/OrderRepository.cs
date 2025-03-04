using INVEST.API.DATA.Context;
using INVEST.API.Repository.Interfaces;
using INVEST.BUSINESSLOGIC.Models;

namespace INVEST.API.Repository
{
    public class OrderRepository(InvestContext _context) : IOrderRepository
    {
        private readonly InvestContext _context = _context;

        public async Task<bool> UpdateBalanceClientAndStockProduct(Order input)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = await _context.TB_PRODUCTS.FindAsync(input.ProductId);
                    product.STOCK = input.Stock;
                    await _context.SaveChangesAsync();

                    var accountClient = await _context.TB_ACCOUNTS_CLIENTS.FindAsync(input.AccountClientId);
                    accountClient.BALANCE = input.Balance;
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }
    }
}
