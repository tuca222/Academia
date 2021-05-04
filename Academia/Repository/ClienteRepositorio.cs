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

        public ClienteRepositorio(IEnderecoClienteRepositorio enderecoClienteRepositorio, IConfiguration configuration)
        {
            _enderecoClienteRepositorio = enderecoClienteRepositorio;
            _configuration = configuration;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            try
            {              
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AtualizaCliente";

                    cmd.Parameters.AddWithValue("@CPFCliente", cliente.CPFCliente);
                    cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                    cmd.Parameters.AddWithValue("@StatusCliente", cliente.StatusCliente);
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                    cmd.Connection = connection;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    var enderecoCliente = cliente.enderecoCliente;
                    enderecoCliente.IdCliente = cliente.IdCliente;

                    _enderecoClienteRepositorio.AtualizarEnderecoCliente(enderecoCliente);

                    connection.Close();
                    connection.Dispose();
                }
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

                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BuscaClientePorCpf";
                    cmd.Parameters.AddWithValue("@CPFCliente", cpfCliente);
                    cmd.Connection = connection;

                    cmd.Connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        cliente = new Cliente();

                        cliente.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                        cliente.CPFCliente = dataReader["CPFCliente"].ToString();
                        cliente.NomeCliente = dataReader["NomeCliente"].ToString();
                        cliente.StatusCliente = Convert.ToBoolean(dataReader["StatusCliente"]);
                        cliente.enderecoCliente = _enderecoClienteRepositorio.BuscarEnderecoPorIdCliente(cliente.IdCliente);
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return cliente;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Cliente> BuscarTodosClientes()
        {
            try
            {
                List<Cliente> listaClientes = new List<Cliente>();
                Cliente cliente = null;


                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BuscaTodosClientes";
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            cliente = new Cliente();

                            cliente.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                            cliente.CPFCliente = dataReader["CpfCliente"].ToString();
                            cliente.NomeCliente = dataReader["NomeCliente"].ToString();
                            cliente.StatusCliente = Convert.ToBoolean(dataReader["StatusCliente"]);

                            listaClientes.Add(cliente);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return listaClientes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DesativarCliente(Cliente cliente)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DesativaCliente";
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                cmd.Connection = connection;
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();
            }
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsereCliente";

                    cmd.Parameters.AddWithValue("@CPFCliente", cliente.CPFCliente);
                    cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                    cmd.Parameters.AddWithValue("@StatusCliente", cliente.StatusCliente);

                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    int idCliente = Convert.ToInt32(cmd.ExecuteScalar());

                    var enderecoCliente = cliente.enderecoCliente;
                    enderecoCliente.IdCliente = idCliente;

                    _enderecoClienteRepositorio.InserirEnderecoCliente(enderecoCliente);

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
