using System.ComponentModel.DataAnnotations;

namespace INVEST.BUSINESSLOGIC.Models.Api
{
    public class Error
    {
        [MinLength(1), MaxLength(1024)]
        public string? Object { get; set; }

        public string? Message { get; set; }
    }
}
