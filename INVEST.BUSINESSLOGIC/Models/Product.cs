namespace INVEST.BUSINESSLOGIC.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string BondAsset { get; set; }

        public string Index { get; set; }

        public decimal Tax { get; set; }

        public string IssuerName { get; set; }

        public long UnitPrice { get; set; }

        public long Stock { get; set; }

        public decimal? TotalProduct {  get; set; }
    }
}
