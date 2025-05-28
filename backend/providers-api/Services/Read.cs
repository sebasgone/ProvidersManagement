using System;
using System.Data;            
using System.Linq;            
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using ProviderApi.Models;

 

namespace ProviderApi.Services
{
     /// <summary>
    /// Servicio que permite consultar proveedores desde la base de datos.
    /// </summary>
    public static class Researcher{
        
        /// <summary>
        /// Busca proveedores cuyo nombre comercial contenga el texto indicado.
        /// </summary>
        /// <param name="name">Texto a buscar en el nombre comercial</param>
        /// <returns>Lista de proveedores coincidentes</returns>
        public static List<Provider> SearchProviderByName(string name)
        {
            var config = new ConfigurationBuilder() // Obtener credenciales de servidor 
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            // Método que trae los proveedores
            var providers = GetAllProviders();

            // Extraer coincidencias por nombre
            return providers
                    .Where(p => p.CommercialName.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }
    
        /// <summary>
        /// Obtiene todos los proveedores registrados en la base de datos.
        /// </summary>
        /// <returns>Lista completa de proveedores</returns>
        public static List<Provider> GetAllProviders()
        {
            // Se crea la lista donde guardaremos cada fila leída
            var result = new List<Provider>();

            var config = new ConfigurationBuilder() // Obtener credenciales de servidor 
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            // Gestor de conexión al servidor
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Apertura el socket TCP con SQL Server

                // Definimos comando SQL a ejecutar
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM SchemaSPA.Providers";
                    cmd.CommandType = CommandType.Text;

                    // ExecuteReader cursor
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
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

                            // Añadimos el objeto a la lista
                            result.Add(provider);
                        }
                    }
                }
            }

            return result;
        }
    }
}