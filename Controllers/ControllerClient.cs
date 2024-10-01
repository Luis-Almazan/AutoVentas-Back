using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using AutoVentas_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerClient : ControllerBase
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;


        // Inyectar QueryContext en el constructor
        public ControllerClient(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;

        }

        // Nuevo método para obtener la lista de clientes
        [HttpGet]
        [Route("ObtenterClientes")]
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

        [HttpGet]
        [Route("ObtenerClientesPorStatus/{status:decimal}")]
        public async Task<IActionResult> GetClientesPorStatus(decimal status)
        {
            // Filtrar los clientes por el estado (status) utilizando el contexto de consultas
            var clientes = await _queryContext.Clientes
                                              .Where(c => c.Status == status)
                                              .ToListAsync();

            if (clientes == null || !clientes.Any())
            {
                return NotFound($"No se encontraron clientes con el estado: {status}");
            }

            return Ok(clientes); // Devuelve los clientes filtrados en formato JSON
        }





        // POST: api/cliente/crear
        [HttpPost]  
        [Route("CrearCliente")]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente nuevoCliente)
        {
            if (nuevoCliente == null)
            {
                return BadRequest("El cliente no puede ser nulo.");
            }

            // Validación simple de campos obligatorios (si aplica)
            if (string.IsNullOrWhiteSpace(nuevoCliente.PrimerNombre) || string.IsNullOrWhiteSpace(nuevoCliente.PrimerApellido))
            {
                return BadRequest("El primer nombre y el primer apellido son obligatorios.");
            }

            try
            {
                // Guardar el cliente en la base de datos
                _operationContext.Clientes.Add(nuevoCliente);
                await _operationContext.SaveChangesAsync();

                return Ok(new { message = "Cliente creado exitosamente", cliente = nuevoCliente });
            }
            catch (Exception ex)
            {
                // Manejar errores, incluyendo excepciones internas
                return StatusCode(500, $"Error al guardar el cliente: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        [Route("ActualizarStatus")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarStatusRequest request)
        {
            // Buscar el cliente en la base de datos utilizando el contexto de consultas (QueryContext)
            var cliente = await _queryContext.Clientes
                                             .FirstOrDefaultAsync(c => c.CodCliente == request.CodCliente);

            if (cliente == null)
            {
                return NotFound($"No se encontró el cliente con código: {request.CodCliente}");
            }

            // Utilizar OperationContext para aplicar los cambios
            _operationContext.Attach(cliente);

            // Actualizamos solo el campo Status del cliente
            cliente.Status = request.Status;

            try
            {
                // Guardar los cambios en la base de datos
                await _operationContext.SaveChangesAsync();
                return Ok(new { message = "Estado actualizado exitosamente", cliente });
            }
            catch (Exception ex)
            {
                // Capturar el mensaje de la excepción interna
                return StatusCode(500, $"Error al actualizar el estado: {ex.Message} | {ex.InnerException?.Message}");
            }
        }














    }
}

