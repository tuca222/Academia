using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Academia.Interfaces;
using Academia.Models;
using Microsoft.Extensions.Configuration;

namespace Academia.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IEnderecoClienteRepositorio _enderecoClienteRepositorio;

        private readonly IConfiguration _configuration;

        private readonly IRepositoryConnection _repositoryConnection;

        private readonly Dictionary<string, string> dados = new Dictionary<string, string>();

        public ClienteRepositorio(IEnderecoClienteRepositorio enderecoClienteRepositorio,
                                  IConfiguration configuration,
                                  IRepositoryConnection repositoryConnection)
        {
            _enderecoClienteRepositorio = enderecoClienteRepositorio;
            _configuration = configuration;
            _repositoryConnection = repositoryConnection;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            try
            {
                dados.Add("@CPFCliente", cliente.CPFCliente);
                dados.Add("@NomeCliente", cliente.NomeCliente);
                dados.Add("@StatusCliente", cliente.StatusCliente.ToString());
                dados.Add("@IdCliente", cliente.IdCliente.ToString());
                _repositoryConnection.CommandExecucaoSimples("AtualizaCliente", dados);

                var enderecoCliente = cliente.enderecoCliente;
                enderecoCliente.IdCliente = cliente.IdCliente;

                _enderecoClienteRepositorio.AtualizarEnderecoCliente(enderecoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cliente BuscarClientePorCpf(string cpfCliente)
        {
            try
            {
                Cliente cliente = null;

                dados.Add("@CPFCliente", cpfCliente);

                var leitura = _repositoryConnection.CommandBusca("BuscaClientePorCpf", dados);


                while (leitura.Read())
                {
                    cliente = new Cliente();

                    cliente.IdCliente = Convert.ToInt32(leitura["IdCliente"]);
                    cliente.CPFCliente = leitura["CpfCliente"].ToString();
                    cliente.NomeCliente = leitura["NomeCliente"].ToString();
                    cliente.StatusCliente = Convert.ToBoolean(leitura["StatusCliente"]);
                    cliente.enderecoCliente = _enderecoClienteRepositorio.BuscarEnderecoPorIdCliente(cliente.IdCliente);
                }

                leitura.Close();
                leitura.Dispose();

                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<Cliente> BuscarTodosClientes()
        {
            try
            {
                List<Cliente> listaClientes = new List<Cliente>();
                Cliente cliente = null;

                SqlDataReader dataReader = _repositoryConnection.CommandBusca("BuscaTodosClientes", dados);
                var leitura = dataReader;


                if (leitura.HasRows)
                {
                    while (leitura.Read())
                    {
                        cliente = new Cliente();

                        cliente.IdCliente = Convert.ToInt32(leitura["IdCliente"]);
                        cliente.CPFCliente = leitura["CpfCliente"].ToString();
                        cliente.NomeCliente = leitura["NomeCliente"].ToString();
                        cliente.StatusCliente = Convert.ToBoolean(leitura["StatusCliente"]);

                        listaClientes.Add(cliente);
                    }
                }

                leitura.Close();
                leitura.Dispose();

                return listaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesativarCliente(Cliente cliente)
        {
            try
            {
                dados.Add("@IdCliente", cliente.IdCliente.ToString());
                _repositoryConnection.CommandExecucaoSimples("DesativaCliente", dados);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                dados.Add("@CPFCliente", cliente.CPFCliente);
                dados.Add("@NomeCliente", cliente.NomeCliente);
                dados.Add("@StatusCliente", cliente.StatusCliente.ToString());
                int numeroRegistro = _repositoryConnection.CommandInserir("InsereCliente", dados);

                var enderecoCliente = cliente.enderecoCliente;
                enderecoCliente.IdCliente = numeroRegistro;

                _enderecoClienteRepositorio.InserirEnderecoCliente(enderecoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
