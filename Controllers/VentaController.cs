using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        // GET: api/Venta/ObtenerVentas
        [HttpGet]
        [Route("ObtenerVentas")]
        public async Task<IActionResult> GetVentas()
        {
            var ventas = await _ventaService.GetVentasAsync();
            if (!ventas.Any())
            {
                return NotFound("No se encontraron ventas.");
            }
            return Ok(ventas);
        }

        // GET: api/Venta/ObtenerVentaPorId/5
        [HttpGet]
        [Route("ObtenerVentaPorId/{codVenta:decimal}")]
        public async Task<IActionResult> GetVentaById(decimal codVenta)
        {
            var venta = await _ventaService.GetVentaByIdAsync(codVenta);
            if (venta == null)
            {
                return NotFound($"No se encontró la venta con código: {codVenta}");
            }
            return Ok(venta);
        }

        // POST: api/Venta/CrearVenta
        [HttpPost]
        [Route("CrearVenta")]
        public async Task<IActionResult> CrearVenta([FromBody] Ventum nuevaVenta)
        {
            if (nuevaVenta == null || nuevaVenta.CodCliente == 0)
            {
                return BadRequest("El código del cliente es obligatorio.");
            }

            var ventaCreada = await _ventaService.CrearVentaAsync(nuevaVenta);
            return CreatedAtAction(nameof(GetVentaById), new { codVenta = ventaCreada.CodVenta }, ventaCreada);
        }

        // PUT: api/Venta/ActualizarVenta/5
        [HttpPut]
        [Route("ActualizarVenta/{codVenta:decimal}")]
        public async Task<IActionResult> ActualizarVenta(decimal codVenta, [FromBody] Ventum ventaActualizada)
        {
            if (ventaActualizada == null || ventaActualizada.CodCliente == 0)
            {
                return BadRequest("El código del cliente es obligatorio.");
            }

            var venta = await _ventaService.ActualizarVentaAsync(codVenta, ventaActualizada);
            if (venta == null)
            {
                return NotFound($"No se encontró la venta con código: {codVenta}");
            }
            return Ok(new { message = "Venta actualizada exitosamente", venta });
        }

        // DELETE: api/Venta/BorrarVenta/5
        [HttpDelete]
        [Route("BorrarVenta/{codVenta:decimal}")]
        public async Task<IActionResult> BorrarVenta(decimal codVenta)
        {
            var ventaBorrada = await _ventaService.BorrarVentaAsync(codVenta);
            if (!ventaBorrada)
            {
                return NotFound($"No se encontró la venta con código: {codVenta}");
            }
            return Ok(new { message = "Venta borrada exitosamente" });
        }
    }
}
