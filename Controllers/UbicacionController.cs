using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionService _ubicacionService;

        public UbicacionController(IUbicacionService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        // GET: api/Ubicacion/ObtenerUbicaciones
        [HttpGet]
        [Route("ObtenerUbicaciones")]
        public async Task<IActionResult> GetUbicaciones()
        {
            var ubicaciones = await _ubicacionService.GetUbicacionesAsync();
            if (!ubicaciones.Any())
            {
                return NotFound("No se encontraron ubicaciones.");
            }
            return Ok(ubicaciones);
        }

        // GET: api/Ubicacion/ObtenerUbicacionPorId/5
        [HttpGet]
        [Route("ObtenerUbicacionPorId/{codUbicacion:decimal}")]
        public async Task<IActionResult> GetUbicacionById(decimal codUbicacion)
        {
            var ubicacion = await _ubicacionService.GetUbicacionByIdAsync(codUbicacion);
            if (ubicacion == null)
            {
                return NotFound($"No se encontró la ubicación con código: {codUbicacion}");
            }
            return Ok(ubicacion);
        }

        // POST: api/Ubicacion/CrearUbicacion
        [HttpPost]
        [Route("CrearUbicacion")]
        public async Task<IActionResult> CrearUbicacion([FromBody] Ubicacion nuevaUbicacion)
        {
            if (nuevaUbicacion == null || string.IsNullOrWhiteSpace(nuevaUbicacion.Nombre))
            {
                return BadRequest("El nombre de la ubicación es obligatorio.");
            }

            var ubicacionCreada = await _ubicacionService.CrearUbicacionAsync(nuevaUbicacion);
            return CreatedAtAction(nameof(GetUbicacionById), new { codUbicacion = ubicacionCreada.CodUbicacion }, ubicacionCreada);
        }

        // PUT: api/Ubicacion/ActualizarUbicacion/5
        [HttpPut]
        [Route("ActualizarUbicacion/{codUbicacion:decimal}")]
        public async Task<IActionResult> ActualizarUbicacion(decimal codUbicacion, [FromBody] Ubicacion ubicacionActualizada)
        {
            if (ubicacionActualizada == null || string.IsNullOrWhiteSpace(ubicacionActualizada.Nombre))
            {
                return BadRequest("El nombre de la ubicación es obligatorio.");
            }

            var ubicacion = await _ubicacionService.ActualizarUbicacionAsync(codUbicacion, ubicacionActualizada);
            if (ubicacion == null)
            {
                return NotFound($"No se encontró la ubicación con código: {codUbicacion}");
            }
            return Ok(new { message = "Ubicación actualizada exitosamente", ubicacion });
        }

        // DELETE: api/Ubicacion/BorrarUbicacion/5
        [HttpDelete]
        [Route("BorrarUbicacion/{codUbicacion:decimal}")]
        public async Task<IActionResult> BorrarUbicacion(decimal codUbicacion)
        {
            var ubicacionBorrada = await _ubicacionService.BorrarUbicacionAsync(codUbicacion);
            if (!ubicacionBorrada)
            {
                return NotFound($"No se encontró la ubicación con código: {codUbicacion}");
            }
            return Ok(new { message = "Ubicación borrada exitosamente" });
        }
    }
}
