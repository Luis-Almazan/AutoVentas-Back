using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.DataAccess.Repositories
{
    public interface IBitacoraRepository
    {
        Task<IEnumerable<Bitacora>> GetAllAsync();
      }
}
