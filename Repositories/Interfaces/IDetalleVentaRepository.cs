using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IDetalleVentaRepository
    {
        Task<List<DetalleVentum>> GetDetallesVentaAsync();
        Task<DetalleVentum> GetDetalleVentaByIdAsync(decimal codDetalleVenta);
        Task AddDetalleVentaAsync(DetalleVentum detalleVenta);
        Task<decimal> GetMaxCodDetalleVentaAsync(); // Método para obtener el valor máximo de CodDetalleVenta
        Task UpdateDetalleVentaAsync(DetalleVentum detalleVenta);
        Task DeleteDetalleVentaAsync(DetalleVentum detalleVenta);
        Task SaveChangesAsync();
    }
}
