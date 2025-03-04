using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_CLIENTS
    {
        [Key]
        public int CLIENT_ID { get; set; }

        [StringLength(1000)]
        [Unicode(false)]
        public required string NAME { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public required string USERNAME { get; set; }
        
        [StringLength(20)]
        [Unicode(false)]
        public required string PASSWORD { get; set; }
    }
}
