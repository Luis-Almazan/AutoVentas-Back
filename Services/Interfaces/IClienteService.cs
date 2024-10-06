using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetClientesAsync();
        Task<List<Cliente>> GetClientesPorStatusAsync(decimal status);
        Task<Cliente> CrearClienteAsync(Cliente nuevoCliente);
        Task<Cliente> ActualizarClienteAsync(decimal codCliente, Cliente clienteActualizado);
        Task<Cliente> ActualizarStatusAsync(decimal codCliente, decimal status);
        Task<bool> BorrarClienteAsync(decimal codCliente);
    }
}

