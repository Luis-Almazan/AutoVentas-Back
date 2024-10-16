using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IVentaService
    {
        Task<List<Ventum>> GetVentasAsync();
        Task<Ventum> GetVentaByIdAsync(decimal codVenta);
        Task<Ventum> CrearVentaAsync(Ventum nuevaVenta);
        Task<Ventum> ActualizarVentaAsync(decimal codVenta, Ventum ventaActualizada);
        Task<bool> BorrarVentaAsync(decimal codVenta);
    }
}
