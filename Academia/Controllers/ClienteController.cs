using Academia.Models;
using Academia.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class ClienteController : Controller
    {
        private readonly ClienteService clienteService = new ClienteService();
        Cliente cliente = null;

        [HttpGet]
        public IActionResult BuscarTodosClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            if (clienteService.BuscarTodosClientes() != null)
            {
                foreach (Cliente clienteNoBanco in clienteService.BuscarTodosClientes())
                {
                    cliente = new Cliente();

                    cliente.IdCliente = clienteNoBanco.IdCliente;
                    cliente.CPFCliente = clienteNoBanco.CPFCliente;
                    cliente.NomeCliente = clienteNoBanco.NomeCliente;
                    cliente.StatusCliente = clienteNoBanco.StatusCliente;

                    listaClientes.Add(cliente);
                }
            }
            return Ok(listaClientes);
        }

        [HttpGet("BuscarClientePorCPF")]
        public IActionResult BuscarClientePorCPF(string cpfcliente)
        {
            if (clienteService.BuscarClientePorCpf(cpfcliente) != null)
            {
                var clienteNoBanco = clienteService.BuscarClientePorCpf(cpfcliente);
                cliente = new Cliente();

                cliente.IdCliente = clienteNoBanco.IdCliente;
                cliente.CPFCliente = clienteNoBanco.CPFCliente;
                cliente.NomeCliente = clienteNoBanco.NomeCliente;
                cliente.StatusCliente = clienteNoBanco.StatusCliente;
                cliente.enderecoCliente = clienteNoBanco.enderecoCliente;
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult InserirCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();

            }
            clienteService.InserirCliente(cliente);
            return Ok();
        }

        [HttpPut]
        public IActionResult AtualizarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            clienteService.AtualizarCliente(cliente);
            return Ok();
        }

        [HttpPatch]
        public IActionResult DesativarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            clienteService.DesativarCliente(cliente);
            return Ok();
        }
    }
}
