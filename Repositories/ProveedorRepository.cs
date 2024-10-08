using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public ProveedorRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Operaciones de lectura usando QueryContext
        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            return await _queryContext.Proveedors
                .Include(p => p.Productos)  // Cargar la relación con productos
                .ToListAsync();
        }

        public async Task<Proveedor> GetProveedorByIdAsync(decimal codProveedor)
        {
            return await _queryContext.Proveedors
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(p => p.CodProveedor == codProveedor);
        }

        // Operaciones de escritura usando OperationContext
        public async Task AddProveedorAsync(Proveedor proveedor)
        {
            await _operationContext.Proveedors.AddAsync(proveedor);
        }

        public async Task UpdateProveedorAsync(Proveedor proveedor)
        {
            _operationContext.Proveedors.Update(proveedor);
        }

        public async Task DeleteProveedorAsync(Proveedor proveedor)
        {
            _operationContext.Proveedors.Remove(proveedor);
        }

        public async Task<decimal> GetMaxCodProveedorAsync()
        {
            var maxCodProveedor = await _queryContext.Proveedors.MaxAsync(p => (decimal?)p.CodProveedor) ?? 0;
            return maxCodProveedor;
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
