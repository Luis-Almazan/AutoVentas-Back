using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public interface IAnulacionVentaRepository
    {
        Task<IEnumerable<AnulacionVenta>> GetAllAsync();
        Task<AnulacionVenta> GetByIdAsync(decimal codAnulacion);
        Task<decimal> GetMaxCodAnulacionAsync(); // Nuevo método para obtener el máximo CodAnulacion
        Task AddAsync(AnulacionVenta anulacionVenta);
        Task UpdateAsync(AnulacionVenta anulacionVenta);
        Task DeleteAsync(decimal codAnulacion);
        Task SaveChangesAsync();
    }
}
