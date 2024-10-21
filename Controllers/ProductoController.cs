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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("ObtenerProductos")]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoService.GetProductosAsync();
            if (!productos.Any())
            {
                return NotFound("No se encontraron productos.");
            }
            return Ok(productos);
        }

        [HttpGet]
        [Route("ObtenerProductoPorId/{codProducto:decimal}")]
        public async Task<IActionResult> GetProductoById(decimal codProducto)
        {
            var producto = await _productoService.GetProductoByIdAsync(codProducto);
            if (producto == null)
            {
                return NotFound($"No se encontró el producto con código: {codProducto}");
            }
            return Ok(producto);
        }

        [HttpPost]
        [Route("CrearProducto")]
        public async Task<IActionResult> CrearProducto([FromBody] Producto nuevoProducto)
        {
            if (nuevoProducto == null || string.IsNullOrWhiteSpace(nuevoProducto.Descripcion))
            {
                return BadRequest("La descripción del producto es obligatoria.");
            }

            var productoCreado = await _productoService.CrearProductoAsync(nuevoProducto);
            return Ok(new { message = "Producto creado exitosamente", producto = productoCreado });
        }

        [HttpPut]
        [Route("ActualizarProducto/{codProducto:decimal}")]
        public async Task<IActionResult> ActualizarProducto(decimal codProducto, [FromBody] Producto productoActualizado)
        {
            var producto = await _productoService.ActualizarProductoAsync(codProducto, productoActualizado);
            if (producto == null)
            {
                return NotFound($"No se encontró el producto con código: {codProducto}");
            }
            return Ok(new { message = "Producto actualizado exitosamente", producto });
        }

        [HttpPost]
        [Route("ActualizarStatus")]
        public async Task<IActionResult> ActualizarStatus([FromBody] ActualizarStatus request)
        {
            var producto = await _productoService.ActualizarStatusAsync(request.CodProducto, request.Status);
            if (producto == null)
            {
                return NotFound($"No se encontró el producto con código: {request.CodProducto}");
            }

            return Ok(new { message = "Status de producto actualizado exitosamente", producto });
        }

        [HttpDelete]
        [Route("BorrarProducto/{codProducto:decimal}")]
        public async Task<IActionResult> BorrarProducto(decimal codProducto)
        {
            var productoBorrado = await _productoService.BorrarProductoAsync(codProducto);
            if (!productoBorrado)
            {
                return NotFound($"No se encontró el producto con código: {codProducto}");
            }
            return Ok(new { message = "Producto borrado exitosamente" });
        }
    }
}
