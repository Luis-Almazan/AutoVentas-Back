using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<Categorium>> GetCategoriasAsync()
        {
            return await _categoriaRepository.GetCategoriasAsync();
        }

        public async Task<Categorium> GetCategoriaByIdAsync(decimal codCategoria)
        {
            return await _categoriaRepository.GetCategoriaByIdAsync(codCategoria);
        }

        public async Task<Categorium> CrearCategoriaAsync(Categorium nuevaCategoria)
        {
            // Obtener el valor máximo de CodCategoria
            var maxCodCategoria = await _categoriaRepository.GetMaxCodCategoriaAsync();

            // Asignar el siguiente número en la secuencia
            nuevaCategoria.CodCategoria = maxCodCategoria + 1;

            await _categoriaRepository.AddCategoriaAsync(nuevaCategoria);
            await _categoriaRepository.SaveChangesAsync();
            return nuevaCategoria;
        }

        public async Task<Categorium> ActualizarCategoriaAsync(decimal codCategoria, Categorium categoriaActualizada)
        {
            var categoriaExistente = await _categoriaRepository.GetCategoriaByIdAsync(codCategoria);
            if (categoriaExistente == null)
            {
                return null;
            }

            // Actualizar los campos de la categoría
            categoriaExistente.Nombre = categoriaActualizada.Nombre ?? categoriaExistente.Nombre;
            categoriaExistente.Descripcion = categoriaActualizada.Descripcion ?? categoriaExistente.Descripcion;

            await _categoriaRepository.UpdateCategoriaAsync(categoriaExistente);
            await _categoriaRepository.SaveChangesAsync();

            return categoriaExistente;
        }

        public async Task<bool> BorrarCategoriaAsync(decimal codCategoria)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(codCategoria);
            if (categoria == null)
            {
                return false;
            }

            await _categoriaRepository.DeleteCategoriaAsync(categoria);
            return true;
        }
    }
}
