using Academia.Interfaces;
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
        private readonly IClienteService _cliente;

        public ClienteController(IClienteService cliente)
        {
            _cliente = cliente;
        }


        [HttpGet]
        public IActionResult BuscarTodosClientes()
        {
            var listCliente = _cliente.BuscarTodosClientes();
            return Ok(listCliente);
        }

        [HttpGet("BuscarClientePorCPF")]
        public IActionResult BuscarClientePorCPF(string cpfcliente)
        {
            if (cpfcliente == "")
                return BadRequest();

            var clienteNoBanco = _cliente.BuscarClientePorCpf(cpfcliente);
          
            return Ok(clienteNoBanco);
        }

        [HttpPost]
        public IActionResult InserirCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest();

            _cliente.InserirCliente(cliente);
            return Ok();
        }

        [HttpPut]
        public IActionResult AtualizarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest();
           
            _cliente.AtualizarCliente(cliente);
            return Ok();
        }

        [HttpPatch]
        public IActionResult DesativarCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest();
            
            _cliente.DesativarCliente(cliente);
            return Ok();
        }
    }
}
