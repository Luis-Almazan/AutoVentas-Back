using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Models.Utils;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Route("ObtenerClientes")]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _clienteService.GetClientesAsync();
            if (!clientes.Any())
            {
                return NotFound("No se encontraron clientes.");
            }
            return Ok(clientes);
        }

        [HttpGet]
        [Route("ObtenerClientesPorStatus/{status:decimal}")]
        public async Task<IActionResult> GetClientesPorStatus(decimal status)
        {
            var clientes = await _clienteService.GetClientesPorStatusAsync(status);
            if (!clientes.Any())
            {
                return NotFound($"No se encontraron clientes con el estado: {status}");
            }
            return Ok(clientes);
        }

        [HttpPost]
        [Route("CrearCliente")]
        public async Task<IActionResult> CrearCliente([FromBody] Cliente nuevoCliente)
        {
            if (nuevoCliente == null || string.IsNullOrWhiteSpace(nuevoCliente.PrimerNombre) || string.IsNullOrWhiteSpace(nuevoCliente.PrimerApellido))
            {
                return BadRequest("El primer nombre y el primer apellido son obligatorios.");
            }

            var clienteCreado = await _clienteService.CrearClienteAsync(nuevoCliente);
            return Ok(new { message = "Cliente creado exitosamente", cliente = clienteCreado });
        }

        [HttpPut]
        [Route("ActualizarCliente/{codCliente:decimal}")]
        public async Task<IActionResult> ActualizarCliente(decimal codCliente, [FromBody] Cliente clienteActualizado)
        {
            var cliente = await _clienteService.ActualizarClienteAsync(codCliente, clienteActualizado);
            if (cliente == null)
            {
                return NotFound($"No se encontró el cliente con código: {codCliente}");
            }
            return Ok(new { message = "Cliente actualizado exitosamente", cliente });
        }

        [HttpPost]
        [Route("ActualizarStatus")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarStatusRequest request)
        {
            var cliente = await _clienteService.ActualizarStatusAsync(request.CodCliente, request.Status);
            if (cliente == null)
            {
                return NotFound($"No se encontró el cliente con código: {request.CodCliente}");
            }
            return Ok(new { message = "Estado actualizado exitosamente", cliente });
        }

        [HttpDelete]
        [Route("BorrarCliente/{codCliente:decimal}")]
        public async Task<IActionResult> BorrarCliente(decimal codCliente)
        {
            var clienteBorrado = await _clienteService.BorrarClienteAsync(codCliente);

            if (!clienteBorrado)
            {
                return NotFound($"No se encontró el cliente con código: {codCliente}");
            }

            return Ok(new { message = "Cliente borrado exitosamente" });
        }

    }
}
