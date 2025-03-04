using INVEST.BUSINESSLOGIC.Models.Api;

namespace INVEST.BUSINESSLOGIC.Models.Outputs
{
    public class ClientOutput : Output
    {
        public Client? LoginClient { get; set; }

        public List<Client>? Clients { get; set; }
    }
}
