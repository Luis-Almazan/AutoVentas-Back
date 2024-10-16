using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;

        public VentaService(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public async Task<List<Ventum>> GetVentasAsync()
        {
            return await _ventaRepository.GetVentasAsync();
        }

        public async Task<Ventum> GetVentaByIdAsync(decimal codVenta)
        {
            return await _ventaRepository.GetVentaByIdAsync(codVenta);
        }

        public async Task<Ventum> CrearVentaAsync(Ventum nuevaVenta)
        {
            // Obtener el valor máximo de CodVenta
            var maxCodVenta = await _ventaRepository.GetMaxCodVentaAsync();

            // Asignar el siguiente número en la secuencia
            nuevaVenta.CodVenta = maxCodVenta + 1;

            await _ventaRepository.AddVentaAsync(nuevaVenta);
            await _ventaRepository.SaveChangesAsync();
            return nuevaVenta;
        }

        public async Task<Ventum> ActualizarVentaAsync(decimal codVenta, Ventum ventaActualizada)
        {
            var ventaExistente = await _ventaRepository.GetVentaByIdAsync(codVenta);
            if (ventaExistente == null)
            {
                return null;
            }

            // Actualizar los campos de la venta
            ventaExistente.CodCliente = ventaActualizada.CodCliente;
            ventaExistente.FechaVenta = ventaActualizada.FechaVenta ?? ventaExistente.FechaVenta;
            ventaExistente.TotalVenta = ventaActualizada.TotalVenta ?? ventaExistente.TotalVenta;
            ventaExistente.StatusVenta = ventaActualizada.StatusVenta;
            ventaExistente.CodAnulacionVenta = ventaActualizada.CodAnulacionVenta ?? ventaExistente.CodAnulacionVenta;

            await _ventaRepository.UpdateVentaAsync(ventaExistente);
            await _ventaRepository.SaveChangesAsync();

            return ventaExistente;
        }

        public async Task<bool> BorrarVentaAsync(decimal codVenta)
        {
            var venta = await _ventaRepository.GetVentaByIdAsync(codVenta);
            if (venta == null)
            {
                return false;
            }

            await _ventaRepository.DeleteVentaAsync(venta);
            return true;
        }
    }
}
