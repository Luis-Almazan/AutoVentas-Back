using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IProductoService
    {
        Task<List<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(decimal codProducto);
        Task<Producto> CrearProductoAsync(Producto nuevoProducto);
        Task<Producto> ActualizarProductoAsync(decimal codProducto, Producto productoActualizado);
        Task<Producto> ActualizarStatusAsync(decimal codProducto, decimal status);
        Task<bool> BorrarProductoAsync(decimal codProducto);
    }
}
