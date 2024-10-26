using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public class DevolucionProductoRepository : IDevolucionProductoRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public DevolucionProductoRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Consultas utilizando QueryContext
        public async Task<IEnumerable<DevolucionProducto>> GetAllAsync()
        {
            return await _queryContext.Set<DevolucionProducto>().ToListAsync();
        }

        public async Task<DevolucionProducto> GetByIdAsync(decimal codDevolucion)
        {
            return await _queryContext.Set<DevolucionProducto>().FindAsync(codDevolucion);
        }

        public async Task<decimal> GetMaxCodDevolucionAsync()
        {
            return await _queryContext.Set<DevolucionProducto>().MaxAsync(dp => dp.CodDevolucion);
        }

        // Operaciones utilizando OperationContext
        public async Task AddAsync(DevolucionProducto devolucionProducto)
        {
            await _operationContext.Set<DevolucionProducto>().AddAsync(devolucionProducto);
        }

        public Task UpdateAsync(DevolucionProducto devolucionProducto)
        {
            _operationContext.Set<DevolucionProducto>().Update(devolucionProducto);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(decimal codDevolucion)
        {
            var devolucionProducto = await GetByIdAsync(codDevolucion);
            if (devolucionProducto != null)
            {
                _operationContext.Set<DevolucionProducto>().Remove(devolucionProducto);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}

