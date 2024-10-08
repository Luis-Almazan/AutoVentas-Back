using AutoVentas_Back.DataAccess.Models;
using AutoVentas_Back.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoVentas_Back.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _clienteRepository.GetClientesAsync();
        }

        public async Task<List<Cliente>> GetClientesPorStatusAsync(decimal status)
        {
            return await _clienteRepository.GetClientesPorStatusAsync(status);
        }

        public async Task<Cliente> CrearClienteAsync(Cliente nuevoCliente)
        {
            // Obtener el valor máximo de CodCliente
            var maxCodCliente = await _clienteRepository.GetMaxCodClienteAsync();

            // Asignar el siguiente número en la secuencia
            nuevoCliente.CodCliente = maxCodCliente + 1;

            await _clienteRepository.AddClienteAsync(nuevoCliente);
            await _clienteRepository.SaveChangesAsync();
            return nuevoCliente;
        }


        public async Task<Cliente> ActualizarClienteAsync(decimal codCliente, Cliente clienteActualizado)
        {
            // Obtener el cliente existente por su código
            var clienteExistente = await _clienteRepository.GetClienteByIdAsync(codCliente);
            if (clienteExistente == null)
            {
                return null; // Retorna null si el cliente no fue encontrado
            }

            // Actualizar los campos del cliente
            clienteExistente.PrimerNombre = clienteActualizado.PrimerNombre ?? clienteExistente.PrimerNombre;
            clienteExistente.SegundoNombre = clienteActualizado.SegundoNombre ?? clienteExistente.SegundoNombre;
            clienteExistente.PrimerApellido = clienteActualizado.PrimerApellido ?? clienteExistente.PrimerApellido;
            clienteExistente.SegundoApellido = clienteActualizado.SegundoApellido ?? clienteExistente.SegundoApellido;
            clienteExistente.Nit = clienteActualizado.Nit ?? clienteExistente.Nit;
            clienteExistente.DireccionCliente = clienteActualizado.DireccionCliente ?? clienteExistente.DireccionCliente;

            // Solo actualizar si el valor es diferente de 0
            clienteExistente.CategoriaCliente = clienteActualizado.CategoriaCliente != 0 ? clienteActualizado.CategoriaCliente : clienteExistente.CategoriaCliente;
            clienteExistente.Status = clienteActualizado.Status != 0 ? clienteActualizado.Status : clienteExistente.Status;

            // Actualizar cliente en el repositorio
            await _clienteRepository.UpdateClienteAsync(clienteExistente);

            // Guardar cambios
            await _clienteRepository.SaveChangesAsync();

            // Retornar el cliente actualizado
            return clienteExistente;
        }


        public async Task<Cliente> ActualizarStatusAsync(decimal codCliente, decimal status)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(codCliente);
            if (cliente != null)
            {
                cliente.Status = status;
                await _clienteRepository.SaveChangesAsync();
            }
            return cliente;
        }

        public async Task<bool> BorrarClienteAsync(decimal codCliente)
        {
            var cliente = await _clienteRepository.GetClienteByIdAsync(codCliente);
            if (cliente == null)
            {
                return false; // Cliente no encontrado
            }

            await _clienteRepository.DeleteClienteAsync(cliente);
            return true; // Cliente borrado exitosamente
        }

    }
}
