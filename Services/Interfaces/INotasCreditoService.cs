using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface INotasCreditoService
    {
        Task<IEnumerable<NotasCredito>> GetAllNotasCreditoAsync();
        Task<NotasCredito> GetNotaCreditoByIdAsync(decimal codNotaCredito);
        Task<NotasCredito> CreateNotaCreditoAsync(NotasCredito notaCredito);
        Task<NotasCredito> UpdateNotaCreditoAsync(decimal codNotaCredito, NotasCredito notaCredito);
        Task<bool> DeleteNotaCreditoAsync(decimal codNotaCredito);
    }
}
