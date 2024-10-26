using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class DevolucionProductoService : IDevolucionProductoService
    {
        private readonly IDevolucionProductoRepository _devolucionProductoRepository;

        public DevolucionProductoService(IDevolucionProductoRepository devolucionProductoRepository)
        {
            _devolucionProductoRepository = devolucionProductoRepository;
        }

        public async Task<IEnumerable<DevolucionProducto>> GetAllDevolucionesAsync()
        {
            return await _devolucionProductoRepository.GetAllAsync();
        }

        public async Task<DevolucionProducto> GetDevolucionByIdAsync(decimal codDevolucion)
        {
            return await _devolucionProductoRepository.GetByIdAsync(codDevolucion);
        }

        public async Task<DevolucionProducto> CreateDevolucionAsync(DevolucionProducto devolucionProducto)
        {
            // Obtener el máximo CodDevolucion actual y sumarle 1
            var maxCodDevolucion = await _devolucionProductoRepository.GetMaxCodDevolucionAsync();
            devolucionProducto.CodDevolucion = maxCodDevolucion + 1;

            await _devolucionProductoRepository.AddAsync(devolucionProducto);
            await _devolucionProductoRepository.SaveChangesAsync();
            return devolucionProducto;
        }

        public async Task<DevolucionProducto> UpdateDevolucionAsync(decimal codDevolucion, DevolucionProducto devolucionActualizada)
        {
            var devolucionProducto = await _devolucionProductoRepository.GetByIdAsync(codDevolucion);
            if (devolucionProducto == null) return null;

            // Actualiza los campos
            devolucionProducto.CodNotaCredito = devolucionActualizada.CodNotaCredito;
            devolucionProducto.Cantidad = devolucionActualizada.Cantidad;
            devolucionProducto.MotivoDevolucion = devolucionActualizada.MotivoDevolucion;

            _devolucionProductoRepository.UpdateAsync(devolucionProducto);
            await _devolucionProductoRepository.SaveChangesAsync();

            return devolucionProducto;
        }

        public async Task<bool> DeleteDevolucionAsync(decimal codDevolucion)
        {
            var devolucionProducto = await _devolucionProductoRepository.GetByIdAsync(codDevolucion);
            if (devolucionProducto == null) return false;

            await _devolucionProductoRepository.DeleteAsync(codDevolucion);
            await _devolucionProductoRepository.SaveChangesAsync();
            return true;
        }
    }
}
