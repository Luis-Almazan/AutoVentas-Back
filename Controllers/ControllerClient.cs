using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoVentas_Back.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ControllerClient : ControllerBase
    {
        private readonly QueryContext _queryContext;

        // Inyectar QueryContext en el constructor
        public ControllerClient(QueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        // Nuevo método para obtener la lista de clientes
        [HttpGet]
        [Route("clientes")]
        public async Task<IActionResult> GetClientes()
        {
            // Consulta a la base de datos utilizando el contexto de consultas
            var clientes = await _queryContext.Clientes.ToListAsync();

            if (clientes == null || !clientes.Any())
            {
                return NotFound("No se encontraron clientes.");
            }

            return Ok(clientes); // Devuelve los clientes en formato JSON
        }
    }
}

