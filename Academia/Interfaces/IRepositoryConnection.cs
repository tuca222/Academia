﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Interfaces
{
    public interface IRepositoryConnection
    {
        public SqlConnection Conexao();

        public SqlDataReader CommandBusca(string nomeProcedure, Dictionary<string, string> Parametros);

        public int CommandInserir(string nomeProcedure, Dictionary<string, string> Parametros);

        public int CommandExecucaoSimples(string nomeProcedure, Dictionary<string, string> Parametros);
    }
}