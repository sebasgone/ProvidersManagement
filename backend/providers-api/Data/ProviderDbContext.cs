using Microsoft.EntityFrameworkCore;
using ProviderApi.Models;

namespace ProviderApi.Data
{
    /// <summary>
    /// Contexto de base de datos para el módulo de proveedores.
    /// Gestiona la conexión y el mapeo entre clases C# y tablas SQL Server.
    /// </summary>
    public class ProviderDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto.
        /// </summary>
        /// <param name="opts">Opciones de configuración del contexto.</param>
        public ProviderDbContext(DbContextOptions<ProviderDbContext> opts)
            : base(opts) { }

        
        /// <summary>
        /// Representa la tabla de proveedores en la base de datos.
        /// </summary>
        public DbSet<Provider> Providers { get; set; } = null!;

        /// <summary>
        /// Configura el modelo de datos, incluyendo el esquema y tipos personalizados.
        /// </summary>
        /// <param name="model">Constructor del modelo de entidades.</param>
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