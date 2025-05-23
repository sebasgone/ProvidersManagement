using System;
using System.Data;
using System.Data.SqlClient;
using ProviderApi.Models;

namespace ProviderApi.Services
{
    public static class Creator{
        
        public static void CreateNewProvider(Provider provider)
        {
            var connectionString = 
              "Server=172.21.16.1,1433;Database=DbSPA;User Id=sa;Password=Sg72178478!;TrustServerCertificate=True;";
            
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = """
                    INSERT INTO SchemaSPA.Providers (
                        SocialName, CommercialName,
                        TributeID, PhoneNumber,
                        Email, WebPage,
                        Address, Country,
                        AnnualBilling, LastEdited) 
                    VALUES (@SocialName, @CommercialName, @TributeID, @PhoneNumber, 
                            @Email, @WebPage, @Address, @Country, 
                            @AnnualBilling, @LastEdited)
                    """;

                // Capa de seguridad para SQL Injection
                cmd.Parameters.AddWithValue("@SocialName", provider.SocialName);
                cmd.Parameters.AddWithValue("@CommercialName", provider.CommercialName);
                cmd.Parameters.AddWithValue("@TributeID", provider.TributeID);
                cmd.Parameters.AddWithValue("@PhoneNumber", provider.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", provider.Email);
                cmd.Parameters.AddWithValue("@WebPage", provider.WebPage);
                cmd.Parameters.AddWithValue("@Address", provider.Address);
                cmd.Parameters.AddWithValue("@Country", provider.Country ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AnnualBilling", provider.AnnualBilling);
                cmd.Parameters.AddWithValue("@LastEdited", provider.LastEdited);

                cmd.ExecuteNonQuery(); // Ejecución de la inserción en SchemaSPA.providers
                }
            }
        }
    }
}