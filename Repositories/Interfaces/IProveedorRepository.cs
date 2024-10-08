using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IProveedorRepository
    {
        Task<List<Proveedor>> GetProveedoresAsync();
        Task<decimal> GetMaxCodProveedorAsync();
        Task<Proveedor> GetProveedorByIdAsync(decimal codProveedor);
        Task AddProveedorAsync(Proveedor proveedor);
        Task UpdateProveedorAsync(Proveedor proveedor);
        Task DeleteProveedorAsync(Proveedor proveedor);
        Task SaveChangesAsync();
    }
}
