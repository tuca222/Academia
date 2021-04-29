using Academia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Interfaces
{
    public interface IClienteService
    {
        public IEnumerable<Cliente> BuscarTodosClientes();
        public Cliente BuscarClientePorCpf(string cpfCliente);
        public void InserirCliente(Cliente cliente);
        public void AtualizarCliente(Cliente cliente);
        public void DesativarCliente(Cliente cliente);
    }
}
