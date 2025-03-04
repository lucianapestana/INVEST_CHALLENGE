using INVEST.API.DATA.Context;
using INVEST.API.Repository.Interfaces;
using INVEST.BUSINESSLOGIC.Models;
using Microsoft.EntityFrameworkCore;

namespace INVEST.API.Repository
{
    public class ClientRepository(InvestContext _context) : IClientRepository
    {
        private readonly InvestContext _context = _context;

        public async Task<int> GetLoginClient(LoginClient input)
        {
            try
            {
                var result = await (
                    from client in _context.TB_CLIENTS
                    where client.USERNAME.ToLower().Equals(input.Username.ToLower())
                    && client.PASSWORD.Equals(input.Password) 
                    select client
                    ).FirstOrDefaultAsync();

                return (result != null && result.CLIENT_ID > 0) ? result.CLIENT_ID : 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar o login.", ex);
            }
        }

        public async Task<List<Client>> GetClients(int? idClient = null, string? nameClient = null)
        {
            try
            {
                var result = await (
                    from clients in _context.TB_CLIENTS
                    join accountClient in _context.TB_ACCOUNTS_CLIENTS on clients.CLIENT_ID equals accountClient.CLIENT_ID
                    where
                    #region [ ID ]
                    (
                        (
                            idClient.HasValue
                            && clients.CLIENT_ID == idClient
                        ) || !idClient.HasValue
                    )
                    #endregion [ ID ]
                    &&
                    #region [ NAME ]
                    (
                        (
                            !string.IsNullOrWhiteSpace(nameClient)
                            && clients.NAME.Contains(nameClient)
                        ) || string.IsNullOrWhiteSpace(nameClient)
                    )
                    #endregion [ NAME ]
                    select new Client()
                    {
                        ClientId = clients.CLIENT_ID,
                        Name = clients.NAME,
                        AccountClient = new AccountClient()
                        {
                            AccountClientId = accountClient.ACCOUNT_CLIENT_ID,
                            Account = accountClient.ACCOUNT,
                            ClientId = accountClient.CLIENT_ID,
                            Balance = accountClient.BALANCE
                        }
                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao efetuar o login.", ex);
            }
        }
    }
}
