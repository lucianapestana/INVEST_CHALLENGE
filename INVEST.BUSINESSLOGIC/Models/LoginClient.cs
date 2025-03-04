using System.ComponentModel.DataAnnotations;

namespace INVEST.BUSINESSLOGIC.Models
{
    public class LoginClient
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Limite da propriedade {0} ultrapassa o valor máximo de {1} caracteres.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Limite da propriedade {0} ultrapassa o valor máximo de {1} caracteres.")]
        public required string Password { get; set; }
    }
}
