using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categorium>> GetCategoriasAsync();
        Task<Categorium> GetCategoriaByIdAsync(decimal codCategoria);
        Task AddCategoriaAsync(Categorium categoria);
        Task<decimal> GetMaxCodCategoriaAsync(); 
        Task UpdateCategoriaAsync(Categorium categoria);
        Task DeleteCategoriaAsync(Categorium categoria);
        Task SaveChangesAsync();
    }
}
