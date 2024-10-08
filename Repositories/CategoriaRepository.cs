using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public CategoriaRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<Categorium>> GetCategoriasAsync()
        {
            return await _queryContext.Categoria.ToListAsync();
        }

        public async Task<Categorium> GetCategoriaByIdAsync(decimal codCategoria)
        {
            return await _queryContext.Categoria.FirstOrDefaultAsync(c => c.CodCategoria == codCategoria);
        }

        public async Task AddCategoriaAsync(Categorium categoria)
        {
            await _operationContext.Categoria.AddAsync(categoria);
        }

        // Método para obtener el valor máximo de CodCategoria
        public async Task<decimal> GetMaxCodCategoriaAsync()
        {
            var maxCodCategoria = await _queryContext.Categoria.MaxAsync(c => (decimal?)c.CodCategoria) ?? 0;
            return maxCodCategoria;
        }

        public async Task UpdateCategoriaAsync(Categorium categoria)
        {
            _operationContext.Categoria.Update(categoria);
        }

        public async Task DeleteCategoriaAsync(Categorium categoria)
        {
            _operationContext.Categoria.Remove(categoria);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
