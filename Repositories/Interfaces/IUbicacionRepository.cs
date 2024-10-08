using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IUbicacionRepository
    {
        Task<List<Ubicacion>> GetUbicacionesAsync();
        Task<Ubicacion> GetUbicacionByIdAsync(decimal codUbicacion);
        Task AddUbicacionAsync(Ubicacion ubicacion);
        Task<decimal> GetMaxCodUbicacionAsync(); 
        Task UpdateUbicacionAsync(Ubicacion ubicacion);
        Task DeleteUbicacionAsync(Ubicacion ubicacion);
        Task SaveChangesAsync();
    }
}
