using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IUbicacionService
    {
        Task<List<Ubicacion>> GetUbicacionesAsync();
        Task<Ubicacion> GetUbicacionByIdAsync(decimal codUbicacion);
        Task<Ubicacion> CrearUbicacionAsync(Ubicacion nuevaUbicacion);
        Task<Ubicacion> ActualizarUbicacionAsync(decimal codUbicacion, Ubicacion ubicacionActualizada);
        Task<bool> BorrarUbicacionAsync(decimal codUbicacion);
    }
}
