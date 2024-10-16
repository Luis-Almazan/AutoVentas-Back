using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly IDetalleVentaService _detalleVentaService;

        public DetalleVentaController(IDetalleVentaService detalleVentaService)
        {
            _detalleVentaService = detalleVentaService;
        }

        // GET: api/DetalleVenta/ObtenerDetallesVenta
        [HttpGet]
        [Route("ObtenerDetallesVenta")]
        public async Task<IActionResult> GetDetallesVenta()
        {
            var detalles = await _detalleVentaService.GetDetallesVentaAsync();
            if (!detalles.Any())
            {
                return NotFound("No se encontraron detalles de ventas.");
            }
            return Ok(detalles);
        }

        // GET: api/DetalleVenta/ObtenerDetalleVentaPorId/5
        [HttpGet]
        [Route("ObtenerDetalleVentaPorId/{codDetalleVenta:decimal}")]
        public async Task<IActionResult> GetDetalleVentaById(decimal codDetalleVenta)
        {
            var detalle = await _detalleVentaService.GetDetalleVentaByIdAsync(codDetalleVenta);
            if (detalle == null)
            {
                return NotFound($"No se encontró el detalle de venta con código: {codDetalleVenta}");
            }
            return Ok(detalle);
        }

        // POST: api/DetalleVenta/CrearDetalleVenta
        [HttpPost]
        [Route("CrearDetalleVenta")]
        public async Task<IActionResult> CrearDetalleVenta([FromBody] DetalleVentum nuevoDetalleVenta)
        {
            if (nuevoDetalleVenta == null || nuevoDetalleVenta.CodProducto == 0 || nuevoDetalleVenta.CodVenta == 0)
            {
                return BadRequest("El código del producto y el código de la venta son obligatorios.");
            }

            var detalleCreado = await _detalleVentaService.CrearDetalleVentaAsync(nuevoDetalleVenta);
            return CreatedAtAction(nameof(GetDetalleVentaById), new { codDetalleVenta = detalleCreado.CodDetalleVenta }, detalleCreado);
        }

        // PUT: api/DetalleVenta/ActualizarDetalleVenta/5
        [HttpPut]
        [Route("ActualizarDetalleVenta/{codDetalleVenta:decimal}")]
        public async Task<IActionResult> ActualizarDetalleVenta(decimal codDetalleVenta, [FromBody] DetalleVentum detalleActualizado)
        {
            if (detalleActualizado == null || detalleActualizado.CodProducto == 0 || detalleActualizado.CodVenta == 0)
            {
                return BadRequest("El código del producto y el código de la venta son obligatorios.");
            }

            var detalle = await _detalleVentaService.ActualizarDetalleVentaAsync(codDetalleVenta, detalleActualizado);
            if (detalle == null)
            {
                return NotFound($"No se encontró el detalle de venta con código: {codDetalleVenta}");
            }
            return Ok(new { message = "Detalle de venta actualizado exitosamente", detalle });
        }

        // DELETE: api/DetalleVenta/BorrarDetalleVenta/5
        [HttpDelete]
        [Route("BorrarDetalleVenta/{codDetalleVenta:decimal}")]
        public async Task<IActionResult> BorrarDetalleVenta(decimal codDetalleVenta)
        {
            var detalleBorrado = await _detalleVentaService.BorrarDetalleVentaAsync(codDetalleVenta);
            if (!detalleBorrado)
            {
                return NotFound($"No se encontró el detalle de venta con código: {codDetalleVenta}");
            }
            return Ok(new { message = "Detalle de venta borrado exitosamente" });
        }
    }
}
