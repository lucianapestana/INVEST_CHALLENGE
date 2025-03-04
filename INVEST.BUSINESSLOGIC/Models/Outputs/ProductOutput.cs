using INVEST.BUSINESSLOGIC.Models.Api;

namespace INVEST.BUSINESSLOGIC.Models.Outputs
{
    public class ProductOutput : Output
    {
        public Product? Product { get; set; }

        public List<Product>? Products { get; set; }
    }
}
