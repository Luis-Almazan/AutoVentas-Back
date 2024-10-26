using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnulacionVentaController : ControllerBase
    {
        private readonly IAnulacionVentaService _anulacionVentaService;

        public AnulacionVentaController(IAnulacionVentaService anulacionVentaService)
        {
            _anulacionVentaService = anulacionVentaService;
        }

        // GET: api/AnulacionVenta/ObtenerAnulaciones
        [HttpGet]
        [Route("ObtenerAnulaciones")]
        public async Task<IActionResult> GetAnulaciones()
        {
            var anulaciones = await _anulacionVentaService.GetAllAnulacionesAsync();
            if (!anulaciones.Any())
            {
                return NotFound("No se encontraron anulaciones de venta.");
            }
            return Ok(anulaciones);
        }

        // GET: api/AnulacionVenta/ObtenerAnulacionPorId/{codAnulacion}
        [HttpGet]
        [Route("ObtenerAnulacionPorId/{codAnulacion:decimal}")]
        public async Task<IActionResult> GetAnulacionById(decimal codAnulacion)
        {
            var anulacion = await _anulacionVentaService.GetAnulacionByIdAsync(codAnulacion);
            if (anulacion == null)
            {
                return NotFound($"No se encontró la anulación de venta con código: {codAnulacion}");
            }
            return Ok(anulacion);
        }

        // POST: api/AnulacionVenta/CrearAnulacion
        [HttpPost]
        [Route("CrearAnulacion")]
        public async Task<IActionResult> CrearAnulacion([FromBody] AnulacionVenta nuevaAnulacion)
        {
            var anulacionCreada = await _anulacionVentaService.CreateAnulacionAsync(nuevaAnulacion);
            return Ok(new { message = "Anulación de venta creada exitosamente", anulacion = anulacionCreada });
        }

        // PUT: api/AnulacionVenta/ActualizarAnulacion/{codAnulacion}
        [HttpPut]
        [Route("ActualizarAnulacion/{codAnulacion:decimal}")]
        public async Task<IActionResult> ActualizarAnulacion(decimal codAnulacion, [FromBody] AnulacionVenta anulacionActualizada)
        {
            var anulacion = await _anulacionVentaService.UpdateAnulacionAsync(codAnulacion, anulacionActualizada);
            if (anulacion == null)
            {
                return NotFound($"No se encontró la anulación de venta con código: {codAnulacion}");
            }
            return Ok(new { message = "Anulación de venta actualizada exitosamente", anulacion });
        }

        // DELETE: api/AnulacionVenta/BorrarAnulacion/{codAnulacion}
        [HttpDelete]
        [Route("BorrarAnulacion/{codAnulacion:decimal}")]
        public async Task<IActionResult> BorrarAnulacion(decimal codAnulacion)
        {
            var anulacionBorrada = await _anulacionVentaService.DeleteAnulacionAsync(codAnulacion);
            if (!anulacionBorrada)
            {
                return NotFound($"No se encontró la anulación de venta con código: {codAnulacion}");
            }
            return Ok(new { message = "Anulación de venta borrada exitosamente" });
        }
    }
}
