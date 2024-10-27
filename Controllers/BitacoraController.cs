using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        private readonly IBitacoraService _bitacoraService;

        public BitacoraController(IBitacoraService bitacoraService)
        {
            _bitacoraService = bitacoraService;
        }

        // GET: api/Bitacora/ObtenerBitacoras
        [HttpGet]
        [Route("ObtenerBitacoras")]
        public async Task<IActionResult> GetBitacoras()
        {
            var bitacoras = await _bitacoraService.GetAllBitacorasAsync();
            if (bitacoras == null || !bitacoras.Any())
            {
                return NotFound("No se encontraron registros en la bitácora.");
            }
            return Ok(bitacoras);
        }

    }
}
