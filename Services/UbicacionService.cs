using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class UbicacionService : IUbicacionService
    {
        private readonly IUbicacionRepository _ubicacionRepository;

        public UbicacionService(IUbicacionRepository ubicacionRepository)
        {
            _ubicacionRepository = ubicacionRepository;
        }

        public async Task<List<Ubicacion>> GetUbicacionesAsync()
        {
            return await _ubicacionRepository.GetUbicacionesAsync();
        }

        public async Task<Ubicacion> GetUbicacionByIdAsync(decimal codUbicacion)
        {
            return await _ubicacionRepository.GetUbicacionByIdAsync(codUbicacion);
        }

        public async Task<Ubicacion> CrearUbicacionAsync(Ubicacion nuevaUbicacion)
        {
            // Obtener el valor máximo de CodUbicacion
            var maxCodUbicacion = await _ubicacionRepository.GetMaxCodUbicacionAsync();

            // Asignar el siguiente número en la secuencia
            nuevaUbicacion.CodUbicacion = maxCodUbicacion + 1;

            await _ubicacionRepository.AddUbicacionAsync(nuevaUbicacion);
            await _ubicacionRepository.SaveChangesAsync();
            return nuevaUbicacion;
        }

        public async Task<Ubicacion> ActualizarUbicacionAsync(decimal codUbicacion, Ubicacion ubicacionActualizada)
        {
            var ubicacionExistente = await _ubicacionRepository.GetUbicacionByIdAsync(codUbicacion);
            if (ubicacionExistente == null)
            {
                return null;
            }

            // Actualizar los campos de la ubicación
            ubicacionExistente.Nombre = ubicacionActualizada.Nombre ?? ubicacionExistente.Nombre;
            ubicacionExistente.Descripcion = ubicacionActualizada.Descripcion ?? ubicacionExistente.Descripcion;

            await _ubicacionRepository.UpdateUbicacionAsync(ubicacionExistente);
            await _ubicacionRepository.SaveChangesAsync();

            return ubicacionExistente;
        }

        public async Task<bool> BorrarUbicacionAsync(decimal codUbicacion)
        {
            var ubicacion = await _ubicacionRepository.GetUbicacionByIdAsync(codUbicacion);
            if (ubicacion == null)
            {
                return false;
            }

            await _ubicacionRepository.DeleteUbicacionAsync(ubicacion);
            return true;
        }
    }
}
