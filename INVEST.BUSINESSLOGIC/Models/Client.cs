namespace INVEST.BUSINESSLOGIC.Models
{
    public class Client
    {
        public required int ClientId { get; set; }

        public string? Name { get; set; }

        public AccountClient? AccountClient { get; set; }
    }
}
