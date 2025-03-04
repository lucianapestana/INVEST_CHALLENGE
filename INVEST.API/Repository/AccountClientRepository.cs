using INVEST.API.DATA.Context;
using INVEST.API.Repository.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using Microsoft.EntityFrameworkCore;

namespace INVEST.API.Repository
{
    public class AccountClientRepository(InvestContext _context) : IAccountClientRepository
    {
        private readonly InvestContext _context = _context;

        public async Task<List<AccountClient>> GetAccountsClients
            (
                int? accountClientId = null,
                string? account = null,
                int? clientId = null
            )
        {
            try
            {
                var result = await (
                    from accountsClients in _context.TB_ACCOUNTS_CLIENTS
                    where
                    #region [ ID ]
                    (
                        (
                            accountClientId.HasValue
                            && accountsClients.ACCOUNT_CLIENT_ID == accountClientId
                        ) || !accountClientId.HasValue
                    )
                    #endregion [ ID ]
                    &&
                    #region [ ACCOUNT ]
                    (
                        (
                            !string.IsNullOrWhiteSpace(account)
                            && accountsClients.ACCOUNT.Equals(account)
                        ) || string.IsNullOrWhiteSpace(account)
                    )
                    #endregion [ ACCOUNT ]
                    &&
                    #region [ CLIENT ID ]
                    (
                        (
                            clientId.HasValue
                            && accountsClients.CLIENT_ID == clientId
                        ) || !clientId.HasValue
                    )
                    #endregion [ CLIENT ID ]
                    select new AccountClient()
                    {
                        AccountClientId = accountsClients.ACCOUNT_CLIENT_ID,
                        Account = accountsClients.ACCOUNT,
                        ClientId = accountsClients.CLIENT_ID,
                        Balance = accountsClients.BALANCE
                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os registros", ex);
            }
        }

        public async Task<bool> UpdateAccountClient(AccountClient input)
        {
            try
            {
                var accountClientExists = await _context.TB_ACCOUNTS_CLIENTS.FindAsync(input.AccountClientId);

                if (accountClientExists != null)
                {
                    accountClientExists.ACCOUNT = input.Account;
                    accountClientExists.CLIENT_ID = input.ClientId;
                    accountClientExists.BALANCE = input.Balance;

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
