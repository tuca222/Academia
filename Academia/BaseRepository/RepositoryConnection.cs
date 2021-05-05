﻿using Academia.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.BaseRepository
{
    public class RepositoryConnection : IRepositoryConnection
    {
        private readonly IConfiguration _configuration;

        public RepositoryConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Conexao()
        {
            SqlConnection _conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return _conn;
        }

        public SqlDataReader CommandBusca(string nomeProcedure, Dictionary<string, string> Parametros)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = nomeProcedure;

                foreach (var val in Parametros)
                {
                    cmd.Parameters.AddWithValue(val.Key, val.Value);
                }

                cmd.Connection = Conexao();
                cmd.Connection.Open();

                var teste = cmd.ExecuteReader();
                return teste; 

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CommandInserir(string nomeProcedure, Dictionary<string, string> Parametros)
        {
            int IdCliente = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = nomeProcedure;

                    foreach (var val in Parametros)
                    {
                        cmd.Parameters.AddWithValue(val.Key, val.Value);
                    }

                    cmd.Connection = Conexao();
                    cmd.Connection.Open();

                    IdCliente = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd.Connection.Close();
                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IdCliente;
        }

        public int CommandExecucaoSimples(string nomeProcedure, Dictionary<string, string> Parametros)
        {
            int IdCliente = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = nomeProcedure;

                    foreach (var val in Parametros)
                    {
                        cmd.Parameters.AddWithValue(val.Key, val.Value);
                    }

                    cmd.Connection = Conexao();
                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IdCliente;
        }
    }
}

