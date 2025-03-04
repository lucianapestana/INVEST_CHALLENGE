using INVEST.API.Repository.Interfaces;
using INVEST.API.Service.Interfaces;
using INVEST.BUSINESSLOGIC.Models.Api;
using INVEST.BUSINESSLOGIC.Models.Outputs;

namespace INVEST.API.Service
{
    public class AccountClientService : IAccountClientService
    {
        private readonly IAccountClientRepository _accountClientRepository;

        public AccountClientService(IAccountClientRepository accountClientRepository)
        {
            _accountClientRepository = accountClientRepository;
        }

        public async Task<AccountClientOutput> ValidateBalanceAccountClientOrder(int accountClientId, decimal totalProduct)
        {
            try
            {
                var output = new AccountClientOutput();

                output.AccountsClients = await _accountClientRepository.GetAccountsClients(accountClientId: accountClientId);

                if (output.AccountsClients != null && output.AccountsClients.Count > 0)
                {
                    output.AccountClient = output.AccountsClients.FirstOrDefault();

                    var newBalance = output.AccountClient?.Balance - totalProduct;

                    if (newBalance < 0)
                    {
                        output.Errors = new List<Error>()
                        {
                            new Error()
                            {
                                Message = "Saldo insuficiente! O total da compra é maior que o saldo disponível."
                            }
                        };

                        return output;
                    }

                    output.AccountClient.Balance = newBalance.Value;
                }

                return output;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao validar os registros", ex);
            }
        }

    }
}
