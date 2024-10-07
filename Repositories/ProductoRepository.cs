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
            return await _queryContext.Productos
                .Include(p => p.CodProveedorNavigation)  // Cargar la relación con el proveedor
                .Include(p => p.UbicacionNavigation)     // Cargar la relación con la ubicación
                .ToListAsync();
        }

        public async Task<Producto> GetProductoByIdAsync(decimal codProducto)
        {
            return await _queryContext.Productos
                .Include(p => p.CodProveedorNavigation)
                .Include(p => p.UbicacionNavigation)
                .FirstOrDefaultAsync(p => p.CodProducto == codProducto);
        }

        // Operaciones de escritura usando OperationContext
        public async Task AddProductoAsync(Producto producto)
        {
            await _operationContext.Productos.AddAsync(producto);
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
