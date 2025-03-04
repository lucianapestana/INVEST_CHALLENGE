using System.ComponentModel.DataAnnotations;

namespace INVEST.BUSINESSLOGIC.Models
{
    public class Order
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int AccountClientId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public long Stock { get; set; }
    }
}
