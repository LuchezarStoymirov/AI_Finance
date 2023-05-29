using Microsoft.EntityFrameworkCore;
using AIF.Models;

namespace AIF.Data
{
    public partial class AifDatabaseContext : DbContext
    {
        public AifDatabaseContext(DbContextOptions<AifDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DemoModel> Demo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemoModel>(entity =>
            {
                entity.Property(e => e.Symbol).IsRequired();
                entity.Property(e => e.LastPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Password).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
