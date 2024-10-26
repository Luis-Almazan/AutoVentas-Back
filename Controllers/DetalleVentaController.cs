using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        [HttpPost]
        [Route("CrearDetallesVenta")]
        public async Task<IActionResult> CrearDetallesVenta([FromBody] List<DetalleVentum> detallesVenta)
        {
            // Validación de la lista de detalles
            if (detallesVenta == null || !detallesVenta.Any())
            {
                return BadRequest("La lista de detalles de venta está vacía o es nula.");
            }

            // Validación individual de cada detalle en la lista
            foreach (var detalle in detallesVenta)
            {
                if (detalle.CodProducto == 0 || detalle.CodVenta == 0)
                {
                    return BadRequest("Cada detalle de venta debe tener un código de producto y un código de venta válido.");
                }
            }

            try
            {
                // Llamar al servicio para guardar cada detalle de venta
                await _detalleVentaService.CrearDetallesVentaAsync(detallesVenta);
                return Ok(detallesVenta);
            }
            catch (DbUpdateException ex)
            {
                // Capturar y loguear la excepción de la base de datos
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, "Ocurrió un error al guardar los detalles de venta.");
            }
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
