using Microsoft.AspNetCore.Mvc;
using ProviderApi.Models;
using ProviderApi.Services;

namespace ProviderApi.Controllers
{
    [ApiController]
    [Route("api/backend")]//Ruta base
    public class ProviderController : ControllerBase
    {
         /// <summary>
        /// Crea un nuevo proveedor.
        /// </summary>
        [HttpPost]
        public IActionResult Create([FromBody] Provider p)
        {
            Manager.CreateNewProvider(p);
            
            return CreatedAtAction(
                nameof(GetByName),      
                new { name = p.CommercialName },
                p );
        }

        /// <summary>
        /// Busca proveedores por nombre.
        /// </summary>
        /// <param name="name">Nombre comercial del proveedor</param>
        [HttpGet("search/{name}")]
        public ActionResult<List<Provider>> GetByName([FromRoute] string name)
        {
            var list = Manager.SearchProviderByName(name);
            if (list == null || list.Count == 0)
                return NotFound();
            return Ok(list);
        }

        /// <summary>
        /// Retorna la lista de todos los proveedores registrados.
        /// </summary>         
        [HttpGet("fetchall")]
        public ActionResult<List<Provider>> GetAllProviders()
        {
            var list = Manager.GetAllProviders();

            if (list == null || list.Count == 0)
                return NotFound(new { message = "No se encontraron proveedores." });

            return Ok(list);
        }

        /// <summary>
        /// Actualiza un proveedor existente.
        /// </summary>
        [HttpPut]
        public IActionResult Update([FromBody] Provider p)
        {
            Manager.UpdateProvider(p);
            return NoContent(); // 204
        }

        /// <summary>
        /// Elimina un proveedor por su ID.
        /// </summary>
        /// <param name="id">ID del proveedor</param>
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // Se tiene que construir un objeto Provider mínimo para borrar
            var dummy = new Provider { Id = id };
            Manager.DeleteProvider(dummy);
            return NoContent(); 
        }
    }
}
