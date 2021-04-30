using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Models
{
    public class EnderecoCliente
    {
        public int IdEnderecoCliente { get; set; }
        public string LogradouroCliente { get; set; }
        public string BairroCliente { get; set; }
        public int IdCliente { get; set; }

    }
}
