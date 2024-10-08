using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<List<Proveedor>> GetProveedoresAsync();
        Task<Proveedor> GetProveedorByIdAsync(decimal codProveedor);
        Task<Proveedor> CrearProveedorAsync(Proveedor nuevoProveedor);
        Task<Proveedor> ActualizarProveedorAsync(decimal codProveedor, Proveedor proveedorActualizado);
        Task<bool> BorrarProveedorAsync(decimal codProveedor);
    }
}
