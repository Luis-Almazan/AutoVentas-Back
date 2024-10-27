using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly QueryContext _queryContext;

        public BitacoraRepository(QueryContext queryContext)
        {
            _queryContext = queryContext;
        }

        public async Task<IEnumerable<Bitacora>> GetAllAsync()
        {
            return await _queryContext.Bitacora.ToListAsync();
        }
    }
}
