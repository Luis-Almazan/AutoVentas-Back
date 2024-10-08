using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IStatusVentaRepository
    {
        Task<List<StatusVentum>> GetStatusVentasAsync();
        Task<StatusVentum> GetStatusVentaByIdAsync(decimal codVenta);
        Task AddStatusVentaAsync(StatusVentum statusVenta);
        Task<decimal> GetMaxCodVentaAsync(); 
        Task UpdateStatusVentaAsync(StatusVentum statusVenta);
        Task DeleteStatusVentaAsync(StatusVentum statusVenta);
        Task SaveChangesAsync();
    }
}
