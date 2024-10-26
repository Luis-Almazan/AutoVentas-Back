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
    public class NotasCreditoController : ControllerBase
    {
        private readonly INotasCreditoService _notasCreditoService;

        public NotasCreditoController(INotasCreditoService notasCreditoService)
        {
            _notasCreditoService = notasCreditoService;
        }

        // GET: api/NotasCredito/ObtenerNotasCredito
        [HttpGet]
        [Route("ObtenerNotasCredito")]
        public async Task<IActionResult> GetNotasCredito()
        {
            var notasCredito = await _notasCreditoService.GetAllNotasCreditoAsync();
            if (!notasCredito.Any())
            {
                return NotFound("No se encontraron notas de crédito.");
            }
            return Ok(notasCredito);
        }

        // GET: api/NotasCredito/ObtenerNotaCreditoPorId/{codNotaCredito}
        [HttpGet]
        [Route("ObtenerNotaCreditoPorId/{codNotaCredito:decimal}")]
        public async Task<IActionResult> GetNotaCreditoById(decimal codNotaCredito)
        {
            var notaCredito = await _notasCreditoService.GetNotaCreditoByIdAsync(codNotaCredito);
            if (notaCredito == null)
            {
                return NotFound($"No se encontró la nota de crédito con código: {codNotaCredito}");
            }
            return Ok(notaCredito);
        }

        // POST: api/NotasCredito/CrearNotaCredito
        [HttpPost]
        [Route("CrearNotaCredito")]
        public async Task<IActionResult> CrearNotaCredito([FromBody] NotasCredito nuevaNotaCredito)
        {
            if (nuevaNotaCredito == null || string.IsNullOrWhiteSpace(nuevaNotaCredito.TipoNota))
            {
                return BadRequest("El tipo de nota de crédito es obligatorio.");
            }

            var notaCreditoCreada = await _notasCreditoService.CreateNotaCreditoAsync(nuevaNotaCredito);
            return Ok(new { message = "Nota de crédito creada exitosamente", notaCredito = notaCreditoCreada });
        }

        // PUT: api/NotasCredito/ActualizarNotaCredito/{codNotaCredito}
        [HttpPut]
        [Route("ActualizarNotaCredito/{codNotaCredito:decimal}")]
        public async Task<IActionResult> ActualizarNotaCredito(decimal codNotaCredito, [FromBody] NotasCredito notaCreditoActualizada)
        {
            var notaCredito = await _notasCreditoService.UpdateNotaCreditoAsync(codNotaCredito, notaCreditoActualizada);
            if (notaCredito == null)
            {
                return NotFound($"No se encontró la nota de crédito con código: {codNotaCredito}");
            }
            return Ok(new { message = "Nota de crédito actualizada exitosamente", notaCredito });
        }

        // DELETE: api/NotasCredito/BorrarNotaCredito/{codNotaCredito}
        [HttpDelete]
        [Route("BorrarNotaCredito/{codNotaCredito:decimal}")]
        public async Task<IActionResult> BorrarNotaCredito(decimal codNotaCredito)
        {
            var notaCreditoBorrada = await _notasCreditoService.DeleteNotaCreditoAsync(codNotaCredito);
            if (!notaCreditoBorrada)
            {
                return NotFound($"No se encontró la nota de crédito con código: {codNotaCredito}");
            }
            return Ok(new { message = "Nota de crédito borrada exitosamente" });
        }
    }
}
