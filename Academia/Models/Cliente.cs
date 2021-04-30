using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string CPFCliente { get; set; }
        public string NomeCliente { get; set; }
        public bool StatusCliente { get; set; }
        public EnderecoCliente enderecoCliente { get; set; }

        public Cliente()
        {

        }
        public Cliente(string cpfCliente, string nomeCliente)
        {
            CPFCliente = cpfCliente;
            NomeCliente = nomeCliente;
            StatusCliente = true;
        }
    }
}
