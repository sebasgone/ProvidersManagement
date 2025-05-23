using Microsoft.AspNetCore.Mvc;

namespace ProviderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        // GET /search
        [HttpGet]
        public IActionResult Get()
            => Ok(new { message = "API viva" });
    }
}
