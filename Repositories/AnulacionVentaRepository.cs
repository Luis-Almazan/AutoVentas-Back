using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public class AnulacionVentaRepository : IAnulacionVentaRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public AnulacionVentaRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Consultas utilizando QueryContext
        public async Task<IEnumerable<AnulacionVenta>> GetAllAsync()
        {
            return await _queryContext.Set<AnulacionVenta>().ToListAsync();
        }

        public async Task<AnulacionVenta> GetByIdAsync(decimal codAnulacion)
        {
            return await _queryContext.Set<AnulacionVenta>().FindAsync(codAnulacion);
        }

        public async Task<decimal> GetMaxCodAnulacionAsync()
        {
            return await _queryContext.Set<AnulacionVenta>().MaxAsync(av => av.CodAnulacion);
        }

        // Operaciones utilizando OperationContext
        public async Task AddAsync(AnulacionVenta anulacionVenta)
        {
            await _operationContext.Set<AnulacionVenta>().AddAsync(anulacionVenta);
        }

        public Task UpdateAsync(AnulacionVenta anulacionVenta)
        {
            _operationContext.Set<AnulacionVenta>().Update(anulacionVenta);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(decimal codAnulacion)
        {
            var anulacionVenta = await GetByIdAsync(codAnulacion);
            if (anulacionVenta != null)
            {
                _operationContext.Set<AnulacionVenta>().Remove(anulacionVenta);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
