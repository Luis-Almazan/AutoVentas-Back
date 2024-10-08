using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using AutoVentas_Back.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<List<Proveedor>> GetProveedoresAsync()
        {
            return await _proveedorRepository.GetProveedoresAsync();
        }

        public async Task<Proveedor> GetProveedorByIdAsync(decimal codProveedor)
        {
            return await _proveedorRepository.GetProveedorByIdAsync(codProveedor);
        }

        public async Task<Proveedor> CrearProveedorAsync(Proveedor nuevoProveedor)
        {
            // Obtener el valor máximo de CodProveedor
            var maxCodProveedor = await _proveedorRepository.GetMaxCodProveedorAsync();

            // Asignar el siguiente número en la secuencia
            nuevoProveedor.CodProveedor = maxCodProveedor + 1;

            await _proveedorRepository.AddProveedorAsync(nuevoProveedor);
            await _proveedorRepository.SaveChangesAsync();
            return nuevoProveedor;
        }

        public async Task<Proveedor> ActualizarProveedorAsync(decimal codProveedor, Proveedor proveedorActualizado)
        {
            var proveedorExistente = await _proveedorRepository.GetProveedorByIdAsync(codProveedor);
            if (proveedorExistente == null)
            {
                return null;
            }

            // Actualizar los campos del proveedor
            proveedorExistente.Nombre = proveedorActualizado.Nombre ?? proveedorExistente.Nombre;
            proveedorExistente.Descripcion = proveedorActualizado.Descripcion ?? proveedorExistente.Descripcion;

            await _proveedorRepository.UpdateProveedorAsync(proveedorExistente);
            await _proveedorRepository.SaveChangesAsync();

            return proveedorExistente;
        }



        public async Task<bool> BorrarProveedorAsync(decimal codProveedor)
        {
            var proveedor = await _proveedorRepository.GetProveedorByIdAsync(codProveedor);
            if (proveedor == null)
            {
                return false; // Proveedor no encontrado
            }

            await _proveedorRepository.DeleteProveedorAsync(proveedor);
            return true; // Proveedor eliminado exitosamente
        }
    }
}
