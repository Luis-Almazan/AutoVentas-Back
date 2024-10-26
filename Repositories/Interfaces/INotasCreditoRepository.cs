using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public interface INotasCreditoRepository
    {
        Task<IEnumerable<NotasCredito>> GetAllAsync();
        Task<NotasCredito> GetByIdAsync(decimal codNotaCredito);
        Task<decimal> GetMaxCodNotaCreditoAsync();
        Task AddAsync(NotasCredito notaCredito);
        Task UpdateAsync(NotasCredito notaCredito);
        Task DeleteAsync(decimal codNotaCredito);
        Task SaveChangesAsync();
    }
}
