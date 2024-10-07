using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(decimal codProducto);
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(Producto producto);
        Task SaveChangesAsync();
    }
}
