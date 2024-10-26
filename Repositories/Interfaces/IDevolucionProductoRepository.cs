using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public interface IDevolucionProductoRepository
    {
        Task<IEnumerable<DevolucionProducto>> GetAllAsync();
        Task<DevolucionProducto> GetByIdAsync(decimal codDevolucion);
        Task<decimal> GetMaxCodDevolucionAsync(); // Método para obtener el máximo CodDevolucion
        Task AddAsync(DevolucionProducto devolucionProducto);
        Task UpdateAsync(DevolucionProducto devolucionProducto);
        Task DeleteAsync(decimal codDevolucion);
        Task SaveChangesAsync();
    }
}
