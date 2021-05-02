using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Models;

namespace Academia.Interfaces
{
    public interface IEnderecoClienteRepositorio
    {
        public EnderecoCliente BuscarEnderecoPorIdCliente(int idCliente);
        public void InserirEnderecoCliente(EnderecoCliente enderecoCliente);
    }
}
