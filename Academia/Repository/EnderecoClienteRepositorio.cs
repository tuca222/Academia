using Academia.Interfaces;
using Academia.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Academia.Useful;

namespace Academia.Repository
{
    public class EnderecoClienteRepositorio : IEnderecoClienteRepositorio
    {
        public EnderecoCliente BuscarEnderecoPorIdCliente(int idCliente)
        {
            try
            {
                EnderecoCliente enderecoCliente = null;

                string consulta = String.Format("Select IdEnderecoCliente, LogradouroCliente, " +
                    "BairroCliente, IdCliente from Endereco where IdCliente = @IdCliente");

                SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);
                connection.Open();

                using(SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

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
    }
}
