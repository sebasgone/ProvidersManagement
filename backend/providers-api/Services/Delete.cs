using System;
using System.Data;
using System.Data.SqlClient;

using ProviderApi.Models;

namespace ProviderApi.Services
{
    public static class Eraser{
        
        public static void DeleteProvider(Provider provider)
        {
            // Elimina a un proveedor del sistema.
            var connectionString = 
              "Server=172.21.16.1,1433;Database=DbSPA;User Id=sa;Password=Sg72178478!;TrustServerCertificate=True;";
            
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = """
                        DELETE FROM SchemaSPA.Providers
                        WHERE Id = @Id
                        """;

                    cmd.CommandType = CommandType.Text;

                    // Capa de seguridad para SQL Injection
                    cmd.Parameters.AddWithValue("@Id", provider.Id);

                    int filasAfectadas = cmd.ExecuteNonQuery(); // Ejecuta el DELETE

                    if (filasAfectadas > 0)
                        Console.WriteLine($"Proveedor con Id {provider.Id} eliminado exitosamente.");
                    else
                        Console.WriteLine($"No se encontró un proveedor con Id {provider.Id}.");
                }
            }
        }
    }
}