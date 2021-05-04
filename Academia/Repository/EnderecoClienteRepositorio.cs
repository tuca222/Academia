using Academia.Interfaces;
using Academia.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Academia.Repository
{
    public class EnderecoClienteRepositorio : IEnderecoClienteRepositorio
    {
        private readonly IConfiguration _configuration;

        public EnderecoClienteRepositorio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AtualizarEnderecoCliente(EnderecoCliente enderecoCliente)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AtualizaEnderecoCliente";

                    cmd.Parameters.AddWithValue("@LogradouroCliente", enderecoCliente.LogradouroCliente);
                    cmd.Parameters.AddWithValue("@BairroCliente", enderecoCliente.BairroCliente);
                    cmd.Parameters.AddWithValue("@IdCliente", enderecoCliente.IdCliente);

                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public EnderecoCliente BuscarEnderecoPorIdCliente(int idCliente)
        {
            try
            {
                EnderecoCliente enderecoCliente = null;

                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BuscaEnderecoPorIdCliente";

                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        enderecoCliente = new EnderecoCliente();

                        enderecoCliente.IdEnderecoCliente = Convert.ToInt32(dataReader["IdEnderecoCliente"]);
                        enderecoCliente.LogradouroCliente = dataReader["LogradouroCliente"].ToString();
                        enderecoCliente.BairroCliente = dataReader["BairroCliente"].ToString();
                        enderecoCliente.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                    }
                    connection.Close();
                    connection.Dispose();
                }
                return enderecoCliente;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void InserirEnderecoCliente(EnderecoCliente enderecoCliente)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "InsereEnderecoCliente";

                    cmd.Parameters.AddWithValue("@LogradouroCliente", enderecoCliente.LogradouroCliente);
                    cmd.Parameters.AddWithValue("@BairroCliente", enderecoCliente.BairroCliente);
                    cmd.Parameters.AddWithValue("@IdCliente", enderecoCliente.IdCliente);
                    cmd.Connection = connection;
                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();
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
