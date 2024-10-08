using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using AutoVentas_Back.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        [Route("ObtenerProveedores")]
        public async Task<IActionResult> GetProveedores()
        {
            var proveedores = await _proveedorService.GetProveedoresAsync();
            if (!proveedores.Any())
            {
                return NotFound("No se encontraron proveedores.");
            }
            return Ok(proveedores);
        }

        [HttpGet]
        [Route("ObtenerProveedorPorId/{codProveedor:decimal}")]
        public async Task<IActionResult> GetProveedorById(decimal codProveedor)
        {
            var proveedor = await _proveedorService.GetProveedorByIdAsync(codProveedor);
            if (proveedor == null)
            {
                return NotFound($"No se encontró el proveedor con código: {codProveedor}");
            }
            return Ok(proveedor);
        }

        [HttpPost]
        [Route("CrearProveedor")]
        public async Task<IActionResult> CrearProveedor([FromBody] Proveedor nuevoProveedor)
        {
            if (nuevoProveedor == null || string.IsNullOrWhiteSpace(nuevoProveedor.Nombre))
            {
                return BadRequest("El nombre del proveedor es obligatorio.");
            }

            var proveedorCreado = await _proveedorService.CrearProveedorAsync(nuevoProveedor);
            return Ok(new { message = "Proveedor creado exitosamente", proveedor = proveedorCreado });
        }

        [HttpPut]
        [Route("ActualizarProveedor/{codProveedor:decimal}")]
        public async Task<IActionResult> ActualizarProveedor(decimal codProveedor, [FromBody] Proveedor proveedorActualizado)
        {
            var proveedor = await _proveedorService.ActualizarProveedorAsync(codProveedor, proveedorActualizado);
            if (proveedor == null)
            {
                return NotFound($"No se encontró el proveedor con código: {codProveedor}");
            }
            return Ok(new { message = "Proveedor actualizado exitosamente", proveedor });
        }

        [HttpDelete]
        [Route("BorrarProveedor/{codProveedor:decimal}")]
        public async Task<IActionResult> BorrarProveedor(decimal codProveedor)
        {
            var proveedorBorrado = await _proveedorService.BorrarProveedorAsync(codProveedor);
            if (!proveedorBorrado)
            {
                return NotFound($"No se encontró el proveedor con código: {codProveedor}");
            }
            return Ok(new { message = "Proveedor borrado exitosamente" });
        }
    }
}
