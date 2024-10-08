using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface ICategoriaService
    {
        Task<List<Categorium>> GetCategoriasAsync();
        Task<Categorium> GetCategoriaByIdAsync(decimal codCategoria);
        Task<Categorium> CrearCategoriaAsync(Categorium nuevaCategoria);
        Task<Categorium> ActualizarCategoriaAsync(decimal codCategoria, Categorium categoriaActualizada);
        Task<bool> BorrarCategoriaAsync(decimal codCategoria);
    }
}