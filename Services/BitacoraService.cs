using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _bitacoraRepository;

        public BitacoraService(IBitacoraRepository bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository;
        }

        public async Task<IEnumerable<Bitacora>> GetAllBitacorasAsync()
        {
            return await _bitacoraRepository.GetAllAsync();
        }
    }
}
