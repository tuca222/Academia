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
        private readonly ClienteRepositorio clienteRepositorio = new ClienteRepositorio();

        public void AtualizarCliente(Cliente cliente)
        {
            clienteRepositorio.AtualizarCliente(cliente);
        }

        public Cliente BuscarClientePorCpf(string cpfCliente)
        {
            return clienteRepositorio.BuscarClientePorCpf(cpfCliente);
        }

        public IEnumerable<Cliente> BuscarTodosClientes()
        {
            return clienteRepositorio.BuscarTodosClientes();
        }

        public void DesativarCliente(Cliente cliente)
        {
            clienteRepositorio.DesativarCliente(cliente);
        }

        public void InserirCliente(Cliente cliente)
        {
            clienteRepositorio.InserirCliente(cliente);
        }
    }
}
