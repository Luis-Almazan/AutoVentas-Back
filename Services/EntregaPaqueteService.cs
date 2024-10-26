using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class EntregaPaqueteService : IEntregaPaqueteService
    {
        private readonly IEntregaPaqueteRepository _entregaPaqueteRepository;

        public EntregaPaqueteService(IEntregaPaqueteRepository entregaPaqueteRepository)
        {
            _entregaPaqueteRepository = entregaPaqueteRepository;
        }

        public async Task<IEnumerable<EntregaPaquete>> GetAllEntregasAsync()
        {
            return await _entregaPaqueteRepository.GetAllAsync();
        }

        public async Task<EntregaPaquete> GetEntregaByIdAsync(decimal codEntrega)
        {
            return await _entregaPaqueteRepository.GetByIdAsync(codEntrega);
        }

        public async Task<EntregaPaquete> CreateEntregaAsync(EntregaPaquete entregaPaquete)
        {
            // Obtener el máximo CodEntrega actual y sumarle 1
            var maxCodEntrega = await _entregaPaqueteRepository.GetMaxCodEntregaAsync();
            entregaPaquete.CodEntrega = maxCodEntrega + 1;

            await _entregaPaqueteRepository.AddAsync(entregaPaquete);
            await _entregaPaqueteRepository.SaveChangesAsync();
            return entregaPaquete;
        }

        public async Task<EntregaPaquete> UpdateEntregaAsync(decimal codEntrega, EntregaPaquete entregaActualizada)
        {
            var entregaPaquete = await _entregaPaqueteRepository.GetByIdAsync(codEntrega);
            if (entregaPaquete == null) return null;

            // Actualiza los campos
            entregaPaquete.Descripcion = entregaActualizada.Descripcion;
            entregaPaquete.Observaciones = entregaActualizada.Observaciones;
            entregaPaquete.CodCliente = entregaActualizada.CodCliente;
            entregaPaquete.CodVenta = entregaActualizada.CodVenta;

            _entregaPaqueteRepository.UpdateAsync(entregaPaquete);
            await _entregaPaqueteRepository.SaveChangesAsync();

            return entregaPaquete;
        }

        public async Task<bool> DeleteEntregaAsync(decimal codEntrega)
        {
            var entregaPaquete = await _entregaPaqueteRepository.GetByIdAsync(codEntrega);
            if (entregaPaquete == null) return false;

            await _entregaPaqueteRepository.DeleteAsync(codEntrega);
            await _entregaPaqueteRepository.SaveChangesAsync();
            return true;
        }
    }
}
