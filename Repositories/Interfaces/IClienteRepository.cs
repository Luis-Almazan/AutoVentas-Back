using AutoVentas_Back.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetClientesAsync();
        Task<List<Cliente>> GetClientesPorStatusAsync(decimal status);
        Task<Cliente> GetClienteByIdAsync(decimal codCliente);
        Task AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(Cliente cliente);
        Task SaveChangesAsync();
    }
}
