using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class DetalleVentaRepository : IDetalleVentaRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public DetalleVentaRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<DetalleVentum>> GetDetallesVentaAsync()
        {
            return await _queryContext.DetalleVenta
                .Include(d => d.CodProductoNavigation) // Cargar la relación con Producto
                .Include(d => d.CodVentaNavigation)    // Cargar la relación con Venta
                .Include(d => d.CodDevolucionProductoNavigation) // Cargar la relación con DevoluciónProducto
                .ToListAsync();
        }

        public async Task<DetalleVentum> GetDetalleVentaByIdAsync(decimal codDetalleVenta)
        {
            return await _queryContext.DetalleVenta
                .Include(d => d.CodProductoNavigation)
                .Include(d => d.CodVentaNavigation)
                .Include(d => d.CodDevolucionProductoNavigation)
                .FirstOrDefaultAsync(d => d.CodDetalleVenta == codDetalleVenta);
        }

        public async Task AddDetalleVentaAsync(DetalleVentum detalleVenta)
        {
            await _operationContext.DetalleVenta.AddAsync(detalleVenta);
        }

        // Método para obtener el valor máximo de CodDetalleVenta
        public async Task<decimal> GetMaxCodDetalleVentaAsync()
        {
            var maxCodDetalleVenta = await _queryContext.DetalleVenta.MaxAsync(d => (decimal?)d.CodDetalleVenta) ?? 0;
            return maxCodDetalleVenta;
        }

        public async Task UpdateDetalleVentaAsync(DetalleVentum detalleVenta)
        {
            _operationContext.DetalleVenta.Update(detalleVenta);
        }

        public async Task DeleteDetalleVentaAsync(DetalleVentum detalleVenta)
        {
            _operationContext.DetalleVenta.Remove(detalleVenta);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
