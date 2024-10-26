using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IDevolucionProductoService
    {
        Task<IEnumerable<DevolucionProducto>> GetAllDevolucionesAsync();
        Task<DevolucionProducto> GetDevolucionByIdAsync(decimal codDevolucion);
        Task<DevolucionProducto> CreateDevolucionAsync(DevolucionProducto devolucionProducto);
        Task<DevolucionProducto> UpdateDevolucionAsync(decimal codDevolucion, DevolucionProducto devolucionActualizada);
        Task<bool> DeleteDevolucionAsync(decimal codDevolucion);
    }
}
