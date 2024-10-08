using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            return await _productoRepository.GetProductosAsync();
        }

        public async Task<Producto> GetProductoByIdAsync(decimal codProducto)
        {
            return await _productoRepository.GetProductoByIdAsync(codProducto);
        }

        public async Task<Producto> CrearProductoAsync(Producto nuevoProducto)
        {
            // Obtener el valor máximo de CodProducto
            var maxCodProducto = await _productoRepository.GetMaxCodProductoAsync();

            // Asignar el siguiente número en la secuencia
            nuevoProducto.CodProducto = maxCodProducto + 1;

            await _productoRepository.AddProductoAsync(nuevoProducto);
            await _productoRepository.SaveChangesAsync();
            return nuevoProducto;
        }

        public async Task<Producto> ActualizarProductoAsync(decimal codProducto, Producto productoActualizado)
        {
            var productoExistente = await _productoRepository.GetProductoByIdAsync(codProducto);
            if (productoExistente == null)
            {
                return null;
            }

            // Actualizar los campos del producto
            productoExistente.Descripcion = productoActualizado.Descripcion ?? productoExistente.Descripcion;
            productoExistente.CodProveedor = productoActualizado.CodProveedor;
            productoExistente.FechaVencimiento = productoActualizado.FechaVencimiento ?? productoExistente.FechaVencimiento;
            productoExistente.Ubicacion = productoActualizado.Ubicacion;
            productoExistente.Existencia = productoActualizado.Existencia ?? productoExistente.Existencia;
            productoExistente.Precio = productoActualizado.Precio ?? productoExistente.Precio;
            productoExistente.Status = productoActualizado.Status ?? productoExistente.Status;

            await _productoRepository.UpdateProductoAsync(productoExistente);
            await _productoRepository.SaveChangesAsync();

            return productoExistente;
        }

        public async Task<bool> BorrarProductoAsync(decimal codProducto)
        {
            var producto = await _productoRepository.GetProductoByIdAsync(codProducto);
            if (producto == null)
            {
                return false; // Producto no encontrado
            }

            await _productoRepository.DeleteProductoAsync(producto);
            return true; // Producto eliminado exitosamente
        }
    }
}
