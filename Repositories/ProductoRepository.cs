using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly QueryContext _queryContext;         // Contexto de consulta
        private readonly OperationContext _operationContext; // Contexto de operaciones

        public ProductoRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        // Operaciones de lectura usando QueryContext
        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _queryContext.Productos.ToListAsync();
        }

        public async Task<Producto> GetProductoByIdAsync(decimal codProducto)
        {
            return await _queryContext.Productos.FirstOrDefaultAsync(p => p.CodProducto == codProducto);
        }

        // Operaciones de escritura usando OperationContext
        public async Task AddProductoAsync(Producto producto)
        {
            await _operationContext.Productos.AddAsync(producto);
        }
        public async Task<decimal> GetMaxCodProductoAsync()
        {
            var maxCodProducto = await _queryContext.Productos.MaxAsync(p => (decimal?)p.CodProducto) ?? 0;
            return maxCodProducto;
        }
        public async Task UpdateProductoAsync(Producto producto)
        {
            _operationContext.Productos.Update(producto);
        }

        public async Task DeleteProductoAsync(Producto producto)
        {
            _operationContext.Productos.Remove(producto);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
