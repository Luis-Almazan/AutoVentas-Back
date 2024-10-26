using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IDetalleVentaService
    {
        Task<List<DetalleVentum>> GetDetallesVentaAsync();
        Task<DetalleVentum> GetDetalleVentaByIdAsync(decimal codDetalleVenta);
        Task<DetalleVentum> CrearDetalleVentaAsync(DetalleVentum nuevoDetalleVenta);
        Task CrearDetallesVentaAsync(List<DetalleVentum> detallesVenta);
        Task<DetalleVentum> ActualizarDetalleVentaAsync(decimal codDetalleVenta, DetalleVentum detalleVentaActualizado);
        Task<bool> BorrarDetalleVentaAsync(decimal codDetalleVenta);
    }
}
