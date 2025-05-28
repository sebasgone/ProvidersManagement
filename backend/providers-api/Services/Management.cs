using ProviderApi.Models;
using System.Collections.Generic;


namespace ProviderApi.Services
{
    /// <summary>
    /// Servicio centralizado para operaciones CRUD sobre proveedores.
    /// </summary>
    public static class Manager{

          /// <summary>
          /// Agrega un nuevo proveedor a la base de datos.
          /// </summary>          
          public static void CreateNewProvider(Provider provider)
          {
            Creator.CreateNewProvider(provider);
          }

          /// <summary>
          /// Retorna todos los proveedores registrados.
          /// </summary>          
          public static List<Provider> GetAllProviders()
          {
            return Researcher.GetAllProviders();
          }

          /// <summary>
          /// Busca proveedores por nombre comercial.
          /// </summary>
          public static List<Provider> SearchProviderByName(string name)
          {
            return Researcher.SearchProviderByName(name);
          } 

          /// <summary>
          /// Actualiza la información de un proveedor.
          /// </summary>
          public static void UpdateProvider( Provider provider)
          {
            Updater.UpdateProvider(provider);
          }

          /// <summary>
          /// Elimina un proveedor por su ID.
          /// </summary>
          public static void DeleteProvider(Provider provider)
          {
            Eraser.DeleteProvider(provider);
          }   
    }
}