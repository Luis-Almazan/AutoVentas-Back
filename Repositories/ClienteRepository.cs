using AutoVentas_Back.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly QueryContext _queryContext;
        private readonly OperationContext _operationContext;

        public ClienteRepository(QueryContext queryContext, OperationContext operationContext)
        {
            _queryContext = queryContext;
            _operationContext = operationContext;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _queryContext.Clientes.ToListAsync();
        }

        public async Task<List<Cliente>> GetClientesPorStatusAsync(decimal status)
        {
            return await _queryContext.Clientes.Where(c => c.Status == status).ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(decimal codCliente)
        {
            return await _operationContext.Clientes.FirstOrDefaultAsync(c => c.CodCliente == codCliente);
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            await _operationContext.Clientes.AddAsync(cliente);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _operationContext.Clientes.Update(cliente);
        }

        public async Task DeleteClienteAsync(Cliente cliente)
        {
            _operationContext.Clientes.Remove(cliente);
            await _operationContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _operationContext.SaveChangesAsync();
        }
    }
}
