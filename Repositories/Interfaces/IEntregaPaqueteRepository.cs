using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public interface IEntregaPaqueteRepository
    {
        Task<IEnumerable<EntregaPaquete>> GetAllAsync();
        Task<EntregaPaquete> GetByIdAsync(decimal codEntrega);
        Task<decimal> GetMaxCodEntregaAsync(); // Método para obtener el máximo CodEntrega
        Task AddAsync(EntregaPaquete entregaPaquete);
        Task UpdateAsync(EntregaPaquete entregaPaquete);
        Task DeleteAsync(decimal codEntrega);
        Task SaveChangesAsync();
    }
}
