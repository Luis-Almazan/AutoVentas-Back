using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IVentaRepository
    {
        Task<List<Ventum>> GetVentasAsync();
        Task<Ventum> GetVentaByIdAsync(decimal codVenta);
        Task AddVentaAsync(Ventum venta);
        Task<decimal> GetMaxCodVentaAsync(); // Método para obtener el valor máximo de CodVenta
        Task UpdateVentaAsync(Ventum venta);
        Task DeleteVentaAsync(Ventum venta);
        Task SaveChangesAsync();
    }
}
