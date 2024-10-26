using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public class EntregaPaqueteRepository : IEntregaPaqueteRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public EntregaPaqueteRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Consultas utilizando QueryContext
        public async Task<IEnumerable<EntregaPaquete>> GetAllAsync()
        {
            return await _queryContext.Set<EntregaPaquete>().ToListAsync();
        }

        public async Task<EntregaPaquete> GetByIdAsync(decimal codEntrega)
        {
            return await _queryContext.Set<EntregaPaquete>().FindAsync(codEntrega);
        }

        public async Task<decimal> GetMaxCodEntregaAsync()
        {
            return await _queryContext.Set<EntregaPaquete>().MaxAsync(ep => ep.CodEntrega);
        }

        // Operaciones utilizando OperationContext
        public async Task AddAsync(EntregaPaquete entregaPaquete)
        {
            await _operationContext.Set<EntregaPaquete>().AddAsync(entregaPaquete);
        }

        public Task UpdateAsync(EntregaPaquete entregaPaquete)
        {
            _operationContext.Set<EntregaPaquete>().Update(entregaPaquete);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(decimal codEntrega)
        {
            var entregaPaquete = await GetByIdAsync(codEntrega);
            if (entregaPaquete != null)
            {
                _operationContext.Set<EntregaPaquete>().Remove(entregaPaquete);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
