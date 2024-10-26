using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionProductoController : ControllerBase
    {
        private readonly IDevolucionProductoService _devolucionProductoService;

        public DevolucionProductoController(IDevolucionProductoService devolucionProductoService)
        {
            _devolucionProductoService = devolucionProductoService;
        }

        // GET: api/DevolucionProducto/ObtenerDevoluciones
        [HttpGet]
        [Route("ObtenerDevoluciones")]
        public async Task<IActionResult> GetDevoluciones()
        {
            var devoluciones = await _devolucionProductoService.GetAllDevolucionesAsync();
            if (!devoluciones.Any())
            {
                return NotFound("No se encontraron devoluciones de producto.");
            }
            return Ok(devoluciones);
        }

        // GET: api/DevolucionProducto/ObtenerDevolucionPorId/{codDevolucion}
        [HttpGet]
        [Route("ObtenerDevolucionPorId/{codDevolucion:decimal}")]
        public async Task<IActionResult> GetDevolucionById(decimal codDevolucion)
        {
            var devolucion = await _devolucionProductoService.GetDevolucionByIdAsync(codDevolucion);
            if (devolucion == null)
            {
                return NotFound($"No se encontró la devolución de producto con código: {codDevolucion}");
            }
            return Ok(devolucion);
        }

        // POST: api/DevolucionProducto/CrearDevolucion
        [HttpPost]
        [Route("CrearDevolucion")]
        public async Task<IActionResult> CrearDevolucion([FromBody] DevolucionProducto nuevaDevolucion)
        {
            var devolucionCreada = await _devolucionProductoService.CreateDevolucionAsync(nuevaDevolucion);
            return Ok(new { message = "Devolución de producto creada exitosamente", devolucion = devolucionCreada });
        }

        // PUT: api/DevolucionProducto/ActualizarDevolucion/{codDevolucion}
        [HttpPut]
        [Route("ActualizarDevolucion/{codDevolucion:decimal}")]
        public async Task<IActionResult> ActualizarDevolucion(decimal codDevolucion, [FromBody] DevolucionProducto devolucionActualizada)
        {
            var devolucion = await _devolucionProductoService.UpdateDevolucionAsync(codDevolucion, devolucionActualizada);
            if (devolucion == null)
            {
                return NotFound($"No se encontró la devolución de producto con código: {codDevolucion}");
            }
            return Ok(new { message = "Devolución de producto actualizada exitosamente", devolucion });
        }

        // DELETE: api/DevolucionProducto/BorrarDevolucion/{codDevolucion}
        [HttpDelete]
        [Route("BorrarDevolucion/{codDevolucion:decimal}")]
        public async Task<IActionResult> BorrarDevolucion(decimal codDevolucion)
        {
            var devolucionBorrada = await _devolucionProductoService.DeleteDevolucionAsync(codDevolucion);
            if (!devolucionBorrada)
            {
                return NotFound($"No se encontró la devolución de producto con código: {codDevolucion}");
            }
            return Ok(new { message = "Devolución de producto borrada exitosamente" });
        }
    }
}
