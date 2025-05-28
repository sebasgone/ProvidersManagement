using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using ProviderApi.Models;

namespace ProviderApi.Services
{
    /// <summary>
    /// Servicio que permite actualizar proveedores existentes en la base de datos.
    /// </summary>
    public static class Updater{
        
        /// <summary>
        /// Actualiza un proveedor existente con nuevos valores.
        /// </summary>
        /// <param name="provider">Proveedor con datos actualizados. El campo <c>Id</c> debe estar presente.</param>
        public static void UpdateProvider(Provider provider)
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
                        UPDATE SchemaSPA.Providers
                        SET SocialName = @SocialName,
                            CommercialName = @CommercialName,
                            TributeID = @TributeID,
                            PhoneNumber = @PhoneNumber,
                            Email = @Email,
                            WebPage = @WebPage,
                            Address = @Address,
                            Country = @Country,
                            AnnualBilling = @AnnualBilling,
                            LastEdited = @LastEdited
                        WHERE Id = @Id
                        """;

                    cmd.CommandType = CommandType.Text;

                    // Capa de seguridad de SQL Injection
                    cmd.Parameters.AddWithValue("@Id", provider.Id);
                    cmd.Parameters.AddWithValue("@SocialName", provider.SocialName);
                    cmd.Parameters.AddWithValue("@CommercialName", provider.CommercialName);
                    cmd.Parameters.AddWithValue("@TributeID", provider.TributeID);
                    cmd.Parameters.AddWithValue("@PhoneNumber", provider.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", provider.Email);
                    cmd.Parameters.AddWithValue("@WebPage", provider.WebPage);
                    cmd.Parameters.AddWithValue("@Address", provider.Address);
                    cmd.Parameters.AddWithValue("@Country", provider.Country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AnnualBilling", provider.AnnualBilling);
                    cmd.Parameters.AddWithValue("@LastEdited", DateTime.UtcNow.AddHours(-5));//UTC-5 (Perú)

                    cmd.ExecuteNonQuery(); // Ejecución de el UPDATE en SchemaSPA.Providers
                }
            }
        }    
    }
}