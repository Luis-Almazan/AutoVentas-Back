using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IBitacoraService
    {
        Task<IEnumerable<Bitacora>> GetAllBitacorasAsync();
    }
}
