using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class DetalleVentaService : IDetalleVentaService
    {
        private readonly IDetalleVentaRepository _detalleVentaRepository;

        public DetalleVentaService(IDetalleVentaRepository detalleVentaRepository)
        {
            _detalleVentaRepository = detalleVentaRepository;
        }

        public async Task<List<DetalleVentum>> GetDetallesVentaAsync()
        {
            return await _detalleVentaRepository.GetDetallesVentaAsync();
        }

        public async Task<DetalleVentum> GetDetalleVentaByIdAsync(decimal codDetalleVenta)
        {
            return await _detalleVentaRepository.GetDetalleVentaByIdAsync(codDetalleVenta);
        }

        public async Task<DetalleVentum> CrearDetalleVentaAsync(DetalleVentum nuevoDetalleVenta)
        {
            // Obtener el valor máximo de CodDetalleVenta
            var maxCodDetalleVenta = await _detalleVentaRepository.GetMaxCodDetalleVentaAsync();

            // Asignar el siguiente número en la secuencia
            nuevoDetalleVenta.CodDetalleVenta = maxCodDetalleVenta + 1;

            await _detalleVentaRepository.AddDetalleVentaAsync(nuevoDetalleVenta);
            await _detalleVentaRepository.SaveChangesAsync();
            return nuevoDetalleVenta;
        }

        public async Task<DetalleVentum> ActualizarDetalleVentaAsync(decimal codDetalleVenta, DetalleVentum detalleVentaActualizado)
        {
            var detalleExistente = await _detalleVentaRepository.GetDetalleVentaByIdAsync(codDetalleVenta);
            if (detalleExistente == null)
            {
                return null;
            }

            // Actualizar los campos del detalle de venta
            detalleExistente.CodProducto = detalleVentaActualizado.CodProducto;
            detalleExistente.Cantidad = detalleVentaActualizado.Cantidad ?? detalleExistente.Cantidad;
            detalleExistente.Subtotal = detalleVentaActualizado.Subtotal ?? detalleExistente.Subtotal;
            detalleExistente.Status = detalleVentaActualizado.Status ?? detalleExistente.Status;
            detalleExistente.CodDevolucionProducto = detalleVentaActualizado.CodDevolucionProducto;

            await _detalleVentaRepository.UpdateDetalleVentaAsync(detalleExistente);
            await _detalleVentaRepository.SaveChangesAsync();

            return detalleExistente;
        }

        public async Task<bool> BorrarDetalleVentaAsync(decimal codDetalleVenta)
        {
            var detalle = await _detalleVentaRepository.GetDetalleVentaByIdAsync(codDetalleVenta);
            if (detalle == null)
            {
                return false;
            }

            await _detalleVentaRepository.DeleteDetalleVentaAsync(detalle);
            return true;
        }
    }
}
