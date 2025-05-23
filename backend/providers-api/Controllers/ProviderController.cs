using Microsoft.AspNetCore.Mvc;
using ProviderApi.Models;
using ProviderApi.Services;
using System.Collections.Generic;

namespace ProviderApi.Controllers
{
    [ApiController]
    [Route("api/backend")]//rutabase
    public class ProviderController : ControllerBase
    {
        // POST api/provider
        // Crea un nuevo proveedor
        [HttpPost]
        public IActionResult Create([FromBody] Provider p)
        {
            Manager.CreateNewProvider(p);
            
            return CreatedAtAction(
                nameof(GetByName),      // 1) nombre del método GET
                new { name = p.CommercialName },  // 2) variables de ruta para esa ruta
                p );
        }

        // GET api/provider/search/{name}
        // Busca proveedores por nombre
        [HttpGet("search/{name}")]
        public ActionResult<List<Provider>> GetByName([FromRoute] string name)
        {
            var list = Manager.SearchProviderByName(name);
            if (list == null || list.Count == 0)
                return NotFound();
            return Ok(list);
        }

         
         // GET api/provider/search/{name}
        // Busca proveedores por nombre
        [HttpGet("fetchall")]
        public ActionResult<List<Provider>> GetAllProviders()
        {
            var list = Manager.GetAllProviders();

            if (list == null || list.Count == 0)
                return NotFound(new { message = "No se encontraron proveedores." });

            return Ok(list);
        }

        // PUT api/provider
        // Actualiza un proveedor (enviamos todo el objeto con Id existente)
        [HttpPut]
        public IActionResult Update([FromBody] Provider p)
        {
            Manager.UpdateProvider(p);
            return NoContent(); // 204
        }

        // DELETE api/provider/{id}
        // Elimina un proveedor; pasamos solo el Id en la ruta
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            // Tendremos que construir un objeto Provider mínimo para borrar
            var dummy = new Provider { Id = id };
            Manager.DeleteProvider(dummy);
            return NoContent(); // 204
        }
    }
}
