using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace _2280602731_NguyenThiHuongSen
{
    public partial class CovidModel : DbContext
    {
        public CovidModel()
            : base("name=CovidModel")
        {
        }

        public virtual DbSet<BenhNhan> BenhNhans { get; set; }
        public virtual DbSet<TinhTrang> TinhTrangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TinhTrang>()
                .HasMany(e => e.BenhNhans)
                .WithRequired(e => e.TinhTrang)
                .WillCascadeOnDelete(false);
        }
    }
}
