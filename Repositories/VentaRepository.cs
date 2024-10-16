using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public VentaRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<Ventum>> GetVentasAsync()
        {
            return await _queryContext.Venta
                .Include(v => v.CodClienteNavigation) // Cargar la relación con Cliente
                .Include(v => v.CodVentaNavigation)   // Cargar la relación con StatusVenta
                .Include(v => v.DetalleVenta)         // Cargar los detalles de la venta
                .ToListAsync();
        }

        public async Task<Ventum> GetVentaByIdAsync(decimal codVenta)
        {
            return await _queryContext.Venta
                .Include(v => v.CodClienteNavigation)
                .Include(v => v.CodVentaNavigation)
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.CodVenta == codVenta);
        }

        public async Task AddVentaAsync(Ventum venta)
        {
            await _operationContext.Venta.AddAsync(venta);
        }

        // Método para obtener el valor máximo de CodVenta
        public async Task<decimal> GetMaxCodVentaAsync()
        {
            var maxCodVenta = await _queryContext.Venta.MaxAsync(v => (decimal?)v.CodVenta) ?? 0;
            return maxCodVenta;
        }

        public async Task UpdateVentaAsync(Ventum venta)
        {
            _operationContext.Venta.Update(venta);
        }

        public async Task DeleteVentaAsync(Ventum venta)
        {
            _operationContext.Venta.Remove(venta);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
