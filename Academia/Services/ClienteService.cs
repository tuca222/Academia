using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Interfaces;
using Academia.Models;
using Academia.Repository;

namespace Academia.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteService(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _clienteRepositorio.AtualizarCliente(cliente);
        }

        public Cliente BuscarClientePorCpf(string cpfCliente)
        {
            return _clienteRepositorio.BuscarClientePorCpf(cpfCliente);
        }

        public IEnumerable<Cliente> BuscarTodosClientes()
        {
            return _clienteRepositorio.BuscarTodosClientes();
        }

        public void DesativarCliente(Cliente cliente)
        {
            _clienteRepositorio.DesativarCliente(cliente);
        }

        public void InserirCliente(Cliente cliente)
        {
            _clienteRepositorio.InserirCliente(cliente);
        }
    }
}
