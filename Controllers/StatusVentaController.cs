using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusVentaController : ControllerBase
    {
        private readonly IStatusVentaService _statusVentaService;

        public StatusVentaController(IStatusVentaService statusVentaService)
        {
            _statusVentaService = statusVentaService;
        }

        // GET: api/StatusVenta/ObtenerStatusVentas
        [HttpGet]
        [Route("ObtenerStatusVentas")]
        public async Task<IActionResult> GetStatusVentas()
        {
            var statusVentas = await _statusVentaService.GetStatusVentasAsync();
            if (!statusVentas.Any())
            {
                return NotFound("No se encontraron estados de ventas.");
            }
            return Ok(statusVentas);
        }

        // GET: api/StatusVenta/ObtenerStatusVentaPorId/5
        [HttpGet]
        [Route("ObtenerStatusVentaPorId/{codVenta:decimal}")]
        public async Task<IActionResult> GetStatusVentaById(decimal codVenta)
        {
            var statusVenta = await _statusVentaService.GetStatusVentaByIdAsync(codVenta);
            if (statusVenta == null)
            {
                return NotFound($"No se encontró el estado de venta con código: {codVenta}");
            }
            return Ok(statusVenta);
        }

        // POST: api/StatusVenta/CrearStatusVenta
        [HttpPost]
        [Route("CrearStatusVenta")]
        public async Task<IActionResult> CrearStatusVenta([FromBody] StatusVentum nuevoStatusVenta)
        {
            if (nuevoStatusVenta == null || string.IsNullOrWhiteSpace(nuevoStatusVenta.Nombre))
            {
                return BadRequest("El nombre del estado de venta es obligatorio.");
            }

            var statusVentaCreado = await _statusVentaService.CrearStatusVentaAsync(nuevoStatusVenta);
            return CreatedAtAction(nameof(GetStatusVentaById), new { codVenta = statusVentaCreado.CodVenta }, statusVentaCreado);
        }

        // PUT: api/StatusVenta/ActualizarStatusVenta/5
        [HttpPut]
        [Route("ActualizarStatusVenta/{codVenta:decimal}")]
        public async Task<IActionResult> ActualizarStatusVenta(decimal codVenta, [FromBody] StatusVentum statusVentaActualizado)
        {
            if (statusVentaActualizado == null || string.IsNullOrWhiteSpace(statusVentaActualizado.Nombre))
            {
                return BadRequest("El nombre del estado de venta es obligatorio.");
            }

            var statusVenta = await _statusVentaService.ActualizarStatusVentaAsync(codVenta, statusVentaActualizado);
            if (statusVenta == null)
            {
                return NotFound($"No se encontró el estado de venta con código: {codVenta}");
            }
            return Ok(new { message = "Estado de venta actualizado exitosamente", statusVenta });
        }

        // DELETE: api/StatusVenta/BorrarStatusVenta/5
        [HttpDelete]
        [Route("BorrarStatusVenta/{codVenta:decimal}")]
        public async Task<IActionResult> BorrarStatusVenta(decimal codVenta)
        {
            var statusVentaBorrada = await _statusVentaService.BorrarStatusVentaAsync(codVenta);
            if (!statusVentaBorrada)
            {
                return NotFound($"No se encontró el estado de venta con código: {codVenta}");
            }
            return Ok(new { message = "Estado de venta borrado exitosamente" });
        }
    }
}
