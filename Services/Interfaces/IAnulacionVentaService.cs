using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IAnulacionVentaService
    {
        Task<IEnumerable<AnulacionVenta>> GetAllAnulacionesAsync();
        Task<AnulacionVenta> GetAnulacionByIdAsync(decimal codAnulacion);
        Task<AnulacionVenta> CreateAnulacionAsync(AnulacionVenta anulacionVenta);
        Task<AnulacionVenta> UpdateAnulacionAsync(decimal codAnulacion, AnulacionVenta anulacionActualizada);
        Task<bool> DeleteAnulacionAsync(decimal codAnulacion);
    }
}
