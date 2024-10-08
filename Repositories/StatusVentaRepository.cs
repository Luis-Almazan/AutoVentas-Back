using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class StatusVentaRepository : IStatusVentaRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public StatusVentaRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<StatusVentum>> GetStatusVentasAsync()
        {
            return await _queryContext.StatusVenta
                .Include(s => s.Ventum) // Cargar la relación con Ventum
                .ToListAsync();
        }

        public async Task<StatusVentum> GetStatusVentaByIdAsync(decimal codVenta)
        {
            return await _queryContext.StatusVenta
                .Include(s => s.Ventum)
                .FirstOrDefaultAsync(s => s.CodVenta == codVenta);
        }

        public async Task AddStatusVentaAsync(StatusVentum statusVenta)
        {
            await _operationContext.StatusVenta.AddAsync(statusVenta);
        }

        
        public async Task<decimal> GetMaxCodVentaAsync()
        {
            var maxCodVenta = await _queryContext.StatusVenta.MaxAsync(s => (decimal?)s.CodVenta) ?? 0;
            return maxCodVenta;
        }

        public async Task UpdateStatusVentaAsync(StatusVentum statusVenta)
        {
            _operationContext.StatusVenta.Update(statusVenta);
        }

        public async Task DeleteStatusVentaAsync(StatusVentum statusVenta)
        {
            _operationContext.StatusVenta.Remove(statusVenta);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
