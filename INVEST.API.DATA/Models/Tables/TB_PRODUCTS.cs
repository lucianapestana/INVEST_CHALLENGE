using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ECOMMERCE.API.DATA.Models.Tables
{
    public partial class TB_PRODUCTS
    {
        [Key]
        public int PRODUCT_ID { get; set; }

        [StringLength(200)]
        [Unicode(false)]
        public required string BOND_ASSET { get; set; }

        [StringLength(500)]
        [Unicode(false)]
        [Column("INDEXADOR")]
        public required string INDEX { get; set; }

        [Column(TypeName = "NUMERIC(15, 1)")]
        public required decimal TAX { get; set; }

        [StringLength(1000)]
        [Unicode(false)]
        public required string ISSUER_NAME { get; set; }

        public required long UNIT_PRICE { get; set; }

        public required long STOCK { get; set; }
    }
}
