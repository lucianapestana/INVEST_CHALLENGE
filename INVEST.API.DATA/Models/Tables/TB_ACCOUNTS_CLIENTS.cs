using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace INVEST.API.DATA.Models.Tables
{
    public partial class TB_ACCOUNTS_CLIENTS
    {
        [Key]
        public int ACCOUNT_CLIENT_ID { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public required string ACCOUNT { get; set; }

        public required int CLIENT_ID { get; set; }

        [Column(TypeName = "NUMERIC(15, 2)")]
        public required decimal BALANCE { get; set; }
    }
}
