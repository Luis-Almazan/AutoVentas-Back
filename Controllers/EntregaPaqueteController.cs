using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaPaqueteController : ControllerBase
    {
        private readonly IEntregaPaqueteService _entregaPaqueteService;

        public EntregaPaqueteController(IEntregaPaqueteService entregaPaqueteService)
        {
            _entregaPaqueteService = entregaPaqueteService;
        }

        // GET: api/EntregaPaquete/ObtenerEntregas
        [HttpGet]
        [Route("ObtenerEntregas")]
        public async Task<IActionResult> GetEntregas()
        {
            var entregas = await _entregaPaqueteService.GetAllEntregasAsync();
            if (!entregas.Any())
            {
                return NotFound("No se encontraron entregas de paquetes.");
            }
            return Ok(entregas);
        }

        // GET: api/EntregaPaquete/ObtenerEntregaPorId/{codEntrega}
        [HttpGet]
        [Route("ObtenerEntregaPorId/{codEntrega:decimal}")]
        public async Task<IActionResult> GetEntregaById(decimal codEntrega)
        {
            var entrega = await _entregaPaqueteService.GetEntregaByIdAsync(codEntrega);
            if (entrega == null)
            {
                return NotFound($"No se encontró la entrega de paquete con código: {codEntrega}");
            }
            return Ok(entrega);
        }

        // POST: api/EntregaPaquete/CrearEntrega
        [HttpPost]
        [Route("CrearEntrega")]
        public async Task<IActionResult> CrearEntrega([FromBody] EntregaPaquete nuevaEntrega)
        {
            var entregaCreada = await _entregaPaqueteService.CreateEntregaAsync(nuevaEntrega);
            return Ok(new { message = "Entrega de paquete creada exitosamente", entrega = entregaCreada });
        }

        // PUT: api/EntregaPaquete/ActualizarEntrega/{codEntrega}
        [HttpPut]
        [Route("ActualizarEntrega/{codEntrega:decimal}")]
        public async Task<IActionResult> ActualizarEntrega(decimal codEntrega, [FromBody] EntregaPaquete entregaActualizada)
        {
            var entrega = await _entregaPaqueteService.UpdateEntregaAsync(codEntrega, entregaActualizada);
            if (entrega == null)
            {
                return NotFound($"No se encontró la entrega de paquete con código: {codEntrega}");
            }
            return Ok(new { message = "Entrega de paquete actualizada exitosamente", entrega });
        }

        // DELETE: api/EntregaPaquete/BorrarEntrega/{codEntrega}
        [HttpDelete]
        [Route("BorrarEntrega/{codEntrega:decimal}")]
        public async Task<IActionResult> BorrarEntrega(decimal codEntrega)
        {
            var entregaBorrada = await _entregaPaqueteService.DeleteEntregaAsync(codEntrega);
            if (!entregaBorrada)
            {
                return NotFound($"No se encontró la entrega de paquete con código: {codEntrega}");
            }
            return Ok(new { message = "Entrega de paquete borrada exitosamente" });
        }
    }
}
