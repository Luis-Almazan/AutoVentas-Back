using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class StatusVentaService : IStatusVentaService
    {
        private readonly IStatusVentaRepository _statusVentaRepository;

        public StatusVentaService(IStatusVentaRepository statusVentaRepository)
        {
            _statusVentaRepository = statusVentaRepository;
        }

        public async Task<List<StatusVentum>> GetStatusVentasAsync()
        {
            return await _statusVentaRepository.GetStatusVentasAsync();
        }

        public async Task<StatusVentum> GetStatusVentaByIdAsync(decimal codVenta)
        {
            return await _statusVentaRepository.GetStatusVentaByIdAsync(codVenta);
        }

        public async Task<StatusVentum> CrearStatusVentaAsync(StatusVentum nuevoStatusVenta)
        {
            // Obtener el valor máximo de CodVenta
            var maxCodVenta = await _statusVentaRepository.GetMaxCodVentaAsync();

            // Asignar el siguiente número en la secuencia
            nuevoStatusVenta.CodVenta = maxCodVenta + 1;

            await _statusVentaRepository.AddStatusVentaAsync(nuevoStatusVenta);
            await _statusVentaRepository.SaveChangesAsync();
            return nuevoStatusVenta;
        }

        public async Task<StatusVentum> ActualizarStatusVentaAsync(decimal codVenta, StatusVentum statusVentaActualizado)
        {
            var statusVentaExistente = await _statusVentaRepository.GetStatusVentaByIdAsync(codVenta);
            if (statusVentaExistente == null)
            {
                return null;
            }

            // Actualizar los campos de status venta
            statusVentaExistente.Nombre = statusVentaActualizado.Nombre ?? statusVentaExistente.Nombre;
            statusVentaExistente.Descripcion = statusVentaActualizado.Descripcion ?? statusVentaExistente.Descripcion;

            await _statusVentaRepository.UpdateStatusVentaAsync(statusVentaExistente);
            await _statusVentaRepository.SaveChangesAsync();

            return statusVentaExistente;
        }

        public async Task<bool> BorrarStatusVentaAsync(decimal codVenta)
        {
            var statusVenta = await _statusVentaRepository.GetStatusVentaByIdAsync(codVenta);
            if (statusVenta == null)
            {
                return false;
            }

            await _statusVentaRepository.DeleteStatusVentaAsync(statusVenta);
            return true;
        }
    }
}
