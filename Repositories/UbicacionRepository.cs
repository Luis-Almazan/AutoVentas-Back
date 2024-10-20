using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public UbicacionRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<Ubicacion>> GetUbicacionesAsync()
        {
            return await _queryContext.Ubicacions
               // .Include(u => u.Productos) // Cargar la relación con productos
                .ToListAsync();
        }

        public async Task<Ubicacion> GetUbicacionByIdAsync(decimal codUbicacion)
        {
            return await _queryContext.Ubicacions
              //  .Include(u => u.Productos)
                .FirstOrDefaultAsync(u => u.CodUbicacion == codUbicacion);
        }

        public async Task AddUbicacionAsync(Ubicacion ubicacion)
        {
            await _operationContext.Ubicacions.AddAsync(ubicacion);
        }

        // Método para obtener el valor máximo de CodUbicacion
        public async Task<decimal> GetMaxCodUbicacionAsync()
        {
            var maxCodUbicacion = await _queryContext.Ubicacions.MaxAsync(u => (decimal?)u.CodUbicacion) ?? 0;
            return maxCodUbicacion;
        }

        public async Task UpdateUbicacionAsync(Ubicacion ubicacion)
        {
            _operationContext.Ubicacions.Update(ubicacion);
        }

        public async Task DeleteUbicacionAsync(Ubicacion ubicacion)
        {
            _operationContext.Ubicacions.Remove(ubicacion);
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
