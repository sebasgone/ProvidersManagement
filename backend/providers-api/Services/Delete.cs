using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


using ProviderApi.Models;

namespace ProviderApi.Services
{
    /// <summary>
    /// Servicio responsable de eliminar proveedores de la base de datos.
    /// </summary>
    public static class Eraser{
        
        /// <summary>
        /// Elimina un proveedor existente por su ID.
        /// </summary>
        /// <param name="provider">Instancia con el ID del proveedor a eliminar.</param>
        public static void DeleteProvider(Provider provider)
        {
            var config = new ConfigurationBuilder() // Obtener credenciales de servidor 
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");            
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