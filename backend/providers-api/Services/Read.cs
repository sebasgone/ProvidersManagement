using System;
using System.Data;            
using System.Linq;            
using System.Data.SqlClient;
using System.Collections.Generic;
using ProviderApi.Models;
 

namespace ProviderApi.Services
{
    public static class Researcher{
        
        public static List<Provider> SearchProviderByName(string name)
        {
            var connectionString = 
              "Server=172.21.16.1,1433;Database=DbSPA;User Id=sa;Password=Sg72178478!;TrustServerCertificate=True;";

            // 5) Llamamos al método que trae los proveedores
            var providers = GetAllProviders(connectionString);

            // Extraer coincidencias por nombre
            return providers
                    .Where(p => p.CommercialName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }
    

        public static List<Provider> GetAllProviders(string ConnectionString)
        {
            // Se crea la lista donde guardaremos cada fila leída
            var result = new List<Provider>();

            // Gestor de conexión al servidor
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open(); // Apertura el socket TCP con SQL Server

                // Definimos comando SQL a ejecutar
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SchemaSPA.Providers";
                    cmd.CommandType = CommandType.Text; // texto raw

                    // ExecuteReader devuelve un SqlDataReader: un cursor sobre filas
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Mientras se tengan filas...
                        while (reader.Read())
                        {
                            // 14) reader["Columna"] o reader.GetXxx(i) recupera cada valor
                            var provider = new Provider
                            {
                                Id             = reader.GetInt32(reader.GetOrdinal("Id")),
                                SocialName     = reader.GetString(reader.GetOrdinal("SocialName")),
                                CommercialName = reader.GetString(reader.GetOrdinal("CommercialName")),
                                TributeID      = reader.GetString(reader.GetOrdinal("TributeID")),
                                PhoneNumber    = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Email          = reader.GetString(reader.GetOrdinal("Email")),
                                WebPage        = reader.GetString(reader.GetOrdinal("WebPage")),
                                Address        = reader.GetString(reader.GetOrdinal("Address")), 
                                Country        = reader.GetString(reader.GetOrdinal("Country")),
                                AnnualBilling  = reader.GetDecimal(reader.GetOrdinal("AnnualBilling")),
                                LastEdited     = reader.GetDateTime(reader.GetOrdinal("LastEdited"))
                            };

                            //Añadimos el objeto a la lista
                            result.Add(provider);
                        }
                    }
                }
            }

            return result;
        }
    }
}