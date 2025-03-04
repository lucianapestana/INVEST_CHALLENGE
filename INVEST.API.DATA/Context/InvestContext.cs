using ECOMMERCE.API.DATA.Models.Tables;
using INVEST.API.DATA.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace INVEST.API.DATA.Context
{
    public partial class InvestContext(DbContextOptions<InvestContext> options) : DbContext(options)
    {
        #region [ TABLE ]

        public virtual DbSet<TB_ACCOUNTS_CLIENTS> TB_ACCOUNTS_CLIENTS { get; set; }
        public virtual DbSet<TB_CLIENTS> TB_CLIENTS { get; set; }
        public virtual DbSet<TB_PRODUCTS> TB_PRODUCTS { get; set; }
        
        #endregion [ TABLE ]

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_ACCOUNTS_CLIENTS>(entity =>
            {
                entity.HasKey(e => e.ACCOUNT_CLIENT_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.ACCOUNT_CLIENT_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TB_CLIENTS>(entity =>
            {
                entity.HasKey(e => e.CLIENT_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.CLIENT_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TB_PRODUCTS>(entity =>
            {
                entity.HasKey(e => e.PRODUCT_ID).HasAnnotation("SqlServer:FillFactor", 100);
                entity.Property(e => e.PRODUCT_ID).ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
