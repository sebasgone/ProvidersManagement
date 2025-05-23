using ProviderApi.Models;
using System.Collections.Generic;


namespace ProviderApi.Services
{
    public static class Manager{
          //Método para agregar un nuevo proveedor
          public static void CreateNewProvider(Provider provider)
          {
            Creator.CreateNewProvider(provider);
          }
          
          //Método para extraer lista de proveedores
          public static List<Provider> GetAllProviders()
          {
            var connectionString = 
              "Server=172.21.16.1,1433;Database=DbSPA;User Id=sa;Password=Sg72178478!;TrustServerCertificate=True;";
            return Researcher.GetAllProviders(connectionString);
          }

          //Método para ver a un proveedor (búsqueda por nombre)
          public static List<Provider> SearchProviderByName(string name)
          {
            return Researcher.SearchProviderByName(name);
          } 

          //Método para editar un proveedor(búsqueda por nombre)
          public static void UpdateProvider( Provider provider)
          {
            Updater.UpdateProvider(provider);
          }

          //Método para eliminar un proveedor(búsqueda por nombre)
          public static void DeleteProvider(Provider provider)
          {
            Eraser.DeleteProvider(provider);
          }   
    }
}