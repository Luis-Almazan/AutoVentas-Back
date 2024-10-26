using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class NotasCreditoService : INotasCreditoService
    {
        private readonly INotasCreditoRepository _notasCreditoRepository;

        public NotasCreditoService(INotasCreditoRepository notasCreditoRepository)
        {
            _notasCreditoRepository = notasCreditoRepository;
        }

        public async Task<IEnumerable<NotasCredito>> GetAllNotasCreditoAsync()
        {
            return await _notasCreditoRepository.GetAllAsync();
        }

        public async Task<NotasCredito> GetNotaCreditoByIdAsync(decimal codNotaCredito)
        {
            return await _notasCreditoRepository.GetByIdAsync(codNotaCredito);
        }

        public async Task<NotasCredito> CreateNotaCreditoAsync(NotasCredito notaCredito)
        {
            // Obtener el máximo CodNotaCredito actual y sumarle 1
            var maxCodNotaCredito = await _notasCreditoRepository.GetMaxCodNotaCreditoAsync();
            notaCredito.CodNotaCredito = maxCodNotaCredito + 1;

            await _notasCreditoRepository.AddAsync(notaCredito);
            await _notasCreditoRepository.SaveChangesAsync();
            return notaCredito;
        }

        public async Task<NotasCredito> UpdateNotaCreditoAsync(decimal codNotaCredito, NotasCredito notaCreditoActualizada)
        {
            var notaCredito = await _notasCreditoRepository.GetByIdAsync(codNotaCredito);
            if (notaCredito == null) return null;

            // Actualiza los campos
            notaCredito.CodCliente = notaCreditoActualizada.CodCliente;
            notaCredito.TipoNota = notaCreditoActualizada.TipoNota;
            notaCredito.FechaNota = notaCreditoActualizada.FechaNota;
            notaCredito.Total = notaCreditoActualizada.Total;
            notaCredito.CodVenta = notaCreditoActualizada.CodVenta;

            _notasCreditoRepository.UpdateAsync(notaCredito);
            await _notasCreditoRepository.SaveChangesAsync();

            return notaCredito;
        }

        public async Task<bool> DeleteNotaCreditoAsync(decimal codNotaCredito)
        {
            var notaCredito = await _notasCreditoRepository.GetByIdAsync(codNotaCredito);
            if (notaCredito == null) return false;

            await _notasCreditoRepository.DeleteAsync(codNotaCredito);
            await _notasCreditoRepository.SaveChangesAsync();
            return true;
        }
    }
}
