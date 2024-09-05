using Microsoft.AspNetCore.Mvc;

namespace AutoVentas_Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class version : ControllerBase
    {

        [HttpGet]
        public IActionResult GetVersion()
        {
            // Puedes devolver la versión como una cadena simple o un objeto JSON
            var version = new { Version = "1.0.0" };
            return Ok(version);
        }
    }
}
