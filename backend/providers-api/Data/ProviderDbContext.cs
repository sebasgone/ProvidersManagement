using Microsoft.EntityFrameworkCore;
using ProviderApi.Models;

namespace ProviderApi.Data
{
    public class ProviderDbContext : DbContext
    {
        public ProviderDbContext(DbContextOptions<ProviderDbContext> opts)
            : base(opts) { }

        // public DbSet<User>     Users     { get; set; } = null!;
        public DbSet<Provider> Providers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.HasDefaultSchema("SchemaSPA"); // Se define el schema 
            
            model.Entity<Provider>()
                .Property(p => p.AnnualBilling)
                .HasColumnType("decimal(18,2)"); // Configuración para valores monetarios
            
            base.OnModelCreating(model);
        }
    }
}