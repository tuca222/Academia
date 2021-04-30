using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Academia.Interfaces;
using Academia.Models;
using Academia.Useful;

namespace Academia.Repository
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        public void AtualizarCliente(Cliente cliente)
        {
            try
            {
                string consulta = String.Format("Update Cliente set CPFCliente = @CPFCliente, " +
                    "NomeCliente = @NomeCliente, StatusCliente = @StatusCliente where IdCliente = @IdCliente");

                SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@CPFCliente", cliente.CPFCliente);
                    cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                    cmd.Parameters.AddWithValue("@StatusCliente", cliente.StatusCliente);
                    cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);

                    connection.Open();
                    cmd.ExecuteNonQuery();
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

                string consulta = String.Format("Select IdCliente, CPFCliente, NomeCliente, " +
                    "StatusCliente from Cliente where CPFCliente = @CPFCliente");

                SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@CPFCliente", cpfCliente);

                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        cliente = new Cliente();

                        cliente.CPFCliente = dataReader["CPFCliente"].ToString();
                        cliente.NomeCliente = dataReader["NomeCliente"].ToString();
                        cliente.StatusCliente = Convert.ToBoolean(dataReader["StatusCliente"]);
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

                string consulta = "Select IdCliente, CPFCliente, NomeCliente, StatusCliente from Cliente";

                SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    connection.Open();

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
            string consulta = "Update Cliente set StatusCliente = 0 where IdCliente = @IdCliente";

            SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);

            using(SqlCommand cmd = new SqlCommand(consulta, connection))
            {
                cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                connection.Open();

                cmd.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                string consulta = String.Format("Insert into Cliente (CPFCliente, NomeCliente, StatusCliente) " +
                "Values (@CPFCliente, @NomeCliente, @StatusCliente)");

                SqlConnection connection = new SqlConnection(DataBaseHelper.stringConnection);

                using (SqlCommand cmd = new SqlCommand(consulta, connection))
                {
                    cmd.Parameters.AddWithValue("@CPFCliente", cliente.CPFCliente);
                    cmd.Parameters.AddWithValue("@NomeCliente", cliente.NomeCliente);
                    cmd.Parameters.AddWithValue("@StatusCliente", cliente.StatusCliente);

                    connection.Open();
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
