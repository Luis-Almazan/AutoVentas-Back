using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: api/Categoria/ObtenerCategorias
        [HttpGet]
        [Route("ObtenerCategorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategoriasAsync();
            if (!categorias.Any())
            {
                return NotFound("No se encontraron categorías.");
            }
            return Ok(categorias);
        }

        // GET: api/Categoria/ObtenerCategoriaPorId/5
        [HttpGet]
        [Route("ObtenerCategoriaPorId/{codCategoria:decimal}")]
        public async Task<IActionResult> GetCategoriaById(decimal codCategoria)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(codCategoria);
            if (categoria == null)
            {
                return NotFound($"No se encontró la categoría con código: {codCategoria}");
            }
            return Ok(categoria);
        }

        // POST: api/Categoria/CrearCategoria
        [HttpPost]
        [Route("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria([FromBody] Categorium nuevaCategoria)
        {
            if (nuevaCategoria == null || string.IsNullOrWhiteSpace(nuevaCategoria.Nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            var categoriaCreada = await _categoriaService.CrearCategoriaAsync(nuevaCategoria);
            return CreatedAtAction(nameof(GetCategoriaById), new { codCategoria = categoriaCreada.CodCategoria }, categoriaCreada);
        }

        // PUT: api/Categoria/ActualizarCategoria/5
        [HttpPut]
        [Route("ActualizarCategoria/{codCategoria:decimal}")]
        public async Task<IActionResult> ActualizarCategoria(decimal codCategoria, [FromBody] Categorium categoriaActualizada)
        {
            if (categoriaActualizada == null || string.IsNullOrWhiteSpace(categoriaActualizada.Nombre))
            {
                return BadRequest("El nombre de la categoría es obligatorio.");
            }

            var categoria = await _categoriaService.ActualizarCategoriaAsync(codCategoria, categoriaActualizada);
            if (categoria == null)
            {
                return NotFound($"No se encontró la categoría con código: {codCategoria}");
            }
            return Ok(new { message = "Categoría actualizada exitosamente", categoria });
        }

        // DELETE: api/Categoria/BorrarCategoria/5
        [HttpDelete]
        [Route("BorrarCategoria/{codCategoria:decimal}")]
        public async Task<IActionResult> BorrarCategoria(decimal codCategoria)
        {
            var categoriaBorrada = await _categoriaService.BorrarCategoriaAsync(codCategoria);
            if (!categoriaBorrada)
            {
                return NotFound($"No se encontró la categoría con código: {codCategoria}");
            }
            return Ok(new { message = "Categoría borrada exitosamente" });
        }
    }
}
