using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IEntregaPaqueteService
    {
        Task<IEnumerable<EntregaPaquete>> GetAllEntregasAsync();
        Task<EntregaPaquete> GetEntregaByIdAsync(decimal codEntrega);
        Task<EntregaPaquete> CreateEntregaAsync(EntregaPaquete entregaPaquete);
        Task<EntregaPaquete> UpdateEntregaAsync(decimal codEntrega, EntregaPaquete entregaActualizada);
        Task<bool> DeleteEntregaAsync(decimal codEntrega);
    }
}
