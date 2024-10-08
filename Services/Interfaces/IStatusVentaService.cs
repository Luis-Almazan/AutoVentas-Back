using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IStatusVentaService
    {
        Task<List<StatusVentum>> GetStatusVentasAsync();
        Task<StatusVentum> GetStatusVentaByIdAsync(decimal codVenta);
        Task<StatusVentum> CrearStatusVentaAsync(StatusVentum nuevoStatusVenta);
        Task<StatusVentum> ActualizarStatusVentaAsync(decimal codVenta, StatusVentum statusVentaActualizado);
        Task<bool> BorrarStatusVentaAsync(decimal codVenta);
    }
}
