using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public class NotasCreditoRepository : INotasCreditoRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public NotasCreditoRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Consultas utilizando QueryContext
        public async Task<IEnumerable<NotasCredito>> GetAllAsync()
        {
            return await _queryContext.Set<NotasCredito>().ToListAsync();
        }

        public async Task<NotasCredito> GetByIdAsync(decimal codNotaCredito)
        {
            return await _queryContext.Set<NotasCredito>().FindAsync(codNotaCredito);
        }

        public async Task<decimal> GetMaxCodNotaCreditoAsync()
        {
            return await _queryContext.Set<NotasCredito>().MaxAsync(nc => nc.CodNotaCredito);
        }

        // Operaciones utilizando OperationContext
        public async Task AddAsync(NotasCredito notaCredito)
        {
            await _operationContext.Set<NotasCredito>().AddAsync(notaCredito);
        }

        public Task UpdateAsync(NotasCredito notaCredito)
        {
            _operationContext.Set<NotasCredito>().Update(notaCredito);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(decimal codNotaCredito)
        {
            var notaCredito = await GetByIdAsync(codNotaCredito);
            if (notaCredito != null)
            {
                _operationContext.Set<NotasCredito>().Remove(notaCredito);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
