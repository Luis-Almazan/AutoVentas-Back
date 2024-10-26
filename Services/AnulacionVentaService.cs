using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class AnulacionVentaService : IAnulacionVentaService
    {
        private readonly IAnulacionVentaRepository _anulacionVentaRepository;

        public AnulacionVentaService(IAnulacionVentaRepository anulacionVentaRepository)
        {
            _anulacionVentaRepository = anulacionVentaRepository;
        }

        public async Task<IEnumerable<AnulacionVenta>> GetAllAnulacionesAsync()
        {
            return await _anulacionVentaRepository.GetAllAsync();
        }

        public async Task<AnulacionVenta> GetAnulacionByIdAsync(decimal codAnulacion)
        {
            return await _anulacionVentaRepository.GetByIdAsync(codAnulacion);
        }

        public async Task<AnulacionVenta> CreateAnulacionAsync(AnulacionVenta anulacionVenta)
        {
            // Obtener el máximo CodAnulacion actual y sumarle 1
            var maxCodAnulacion = await _anulacionVentaRepository.GetMaxCodAnulacionAsync();
            anulacionVenta.CodAnulacion = maxCodAnulacion + 1;

            await _anulacionVentaRepository.AddAsync(anulacionVenta);
            await _anulacionVentaRepository.SaveChangesAsync();
            return anulacionVenta;
        }

        public async Task<AnulacionVenta> UpdateAnulacionAsync(decimal codAnulacion, AnulacionVenta anulacionActualizada)
        {
            var anulacionVenta = await _anulacionVentaRepository.GetByIdAsync(codAnulacion);
            if (anulacionVenta == null) return null;

            // Actualiza los campos
            anulacionVenta.CodNotaCredito = anulacionActualizada.CodNotaCredito;
            anulacionVenta.CodVenta = anulacionActualizada.CodVenta;
            anulacionVenta.MotivoAnulacion = anulacionActualizada.MotivoAnulacion;

            _anulacionVentaRepository.UpdateAsync(anulacionVenta);
            await _anulacionVentaRepository.SaveChangesAsync();

            return anulacionVenta;
        }

        public async Task<bool> DeleteAnulacionAsync(decimal codAnulacion)
        {
            var anulacionVenta = await _anulacionVentaRepository.GetByIdAsync(codAnulacion);
            if (anulacionVenta == null) return false;

            await _anulacionVentaRepository.DeleteAsync(codAnulacion);
            await _anulacionVentaRepository.SaveChangesAsync();
            return true;
        }
    }
}
