using INVEST.BUSINESSLOGIC.Models.Api;

namespace INVEST.BUSINESSLOGIC.Models.Outputs
{
    public class AccountClientOutput : Output
    {
        public AccountClient? AccountClient { get; set; }

        public List<AccountClient>? AccountsClients { get; set; }
    }
}
