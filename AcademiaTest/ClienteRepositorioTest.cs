using Academia.Interfaces;
using Academia.Models;
using Academia.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AcademiaTest
{
    [TestClass]
    public class ClienteRepositorioTest
    {
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly Mock<IRepositoryConnection> _repositoryConnection;

        private readonly Mock<IEnderecoClienteRepositorio> _enderecoClienteRepositorio;


        public ClienteRepositorioTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _repositoryConnection = new Mock<IRepositoryConnection>();
            _enderecoClienteRepositorio = new Mock<IEnderecoClienteRepositorio>();
        }

        [TestMethod]
        public void BuscaTodosClientesTest_OK()
        {
            //Arrange
            var jsonDataTable = @"[
                    {
                      'IdCliente': '1',
                      'CpfCliente': '07772719906',
                      'NomeCliente': 'Arthur',
                      'StatusCliente': 'true',
                    },
                    {
                      'IdCliente': '2',
                      'CpfCliente': '53772719906',
                      'NomeCliente': 'Renan',
                      'StatusCliente': 'true',
                    }
                ]";

            //var dataTable = JsonConvert.DeserializeObject<DataTable>(jsonDataTable);

            _repositoryConnection.Setup(x => x.CommandBusca("BuscaTodosClientes", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarTodosClientes();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuscarClientePorCpfTest_OK()
        {
            //Arrange
            var jsonDataTable = @"[
                    {
                      'IdCliente': '1',
                      'CpfCliente': '07772719906',
                      'NomeCliente': 'Arthur',
                      'StatusCliente': 'true',
                    }
                ]";
            //var dataTable = JsonConvert.DeserializeObject<DataTable>(jsonDataTable);
            var cpfCliente = "07772719906";

            _repositoryConnection.Setup(x => x.CommandBusca("BuscaClientePorCpf", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarClientePorCpf(cpfCliente);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuscarClientePorCpfTest_Empty()
        {
            //Arrange
            var jsonDataTable = @"[{}]";
            var cpfCliente = "07772719906";

            _repositoryConnection.Setup(x => x.CommandBusca("BuscaClientePorCpf", It.IsAny<Dictionary<string, string>>())).Returns(jsonDataTable);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarClientePorCpf(cpfCliente);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuscarClientePorCpfTest_ArgumentNullException()
        {
            //Arrange
            var cpfCliente = "07772719906";

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarClientePorCpf(cpfCliente);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AtualizaClienteTest_Ok()
        {
            //Arrange
            var jsonCliente = @"{
               'idCliente': 11,
               'cpfCliente': '456456456',
                'nomeCliente': 'testando alteracao',
                'statusCliente': true,
                'enderecoCliente': {
                    'logradouroCliente': 'rua santos alterada',
                    'bairroCliente': 'santo agostin'
                 }
                 } ";

            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(jsonCliente);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            repo.AtualizarCliente(cliente);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AtualizaClienteTest_NullReferenceException()
        {
            //Arrange
            var jsonCliente = @"{ } ";

            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(jsonCliente);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            repo.AtualizarCliente(cliente);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AtualizaClienteTest_Ex()
        {
            //Arrange
            var jsonCliente = @"{
               'idCliente': 11,
               'cpfCliente': '456456456',
                'nomeCliente': 'testando alteracao',
                'statusCliente': true,
                'enderecoCliente': {
                    'logradouroCliente': 'rua santos alterada',
                    'bairroCliente': 'santo agostin'
                 }
                 } ";

            Cliente cliente = JsonConvert.DeserializeObject<Cliente>(jsonCliente);
            _repositoryConnection.Setup(x => x.CommandExecucaoSimples("AtualizaCliente", It.IsAny<Dictionary<string, string>>())).Throws(new Exception());

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            repo.AtualizarCliente(cliente);

            //Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void InsereCliente()
        {
            //Arrange
            var jsonCliente = @"{
               'cpfCliente': '456456456',
                'nomeCliente': 'testando alteracao',
                'statusCliente': true,
                'enderecoCliente': {
                    'logradouroCliente': 'rua santos alterada',
                    'bairroCliente': 'santo agostin'
                 }
                 } ";
            var cliente = JsonConvert.DeserializeObject<Cliente>(jsonCliente);

            _repositoryConnection.Setup(x => x.CommandInserir("InsereCliente", It.IsAny<Dictionary<string, string>>())).Returns(1);
            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            repo.InserirCliente(cliente);

            //Assert
            Assert.IsTrue(true);
        }

    }
}
