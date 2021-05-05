using Academia.Interfaces;
using Academia.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        public void BuscaTodosClientesTeste()
        {
            //Arrange
            bool readToggle = true;
            Mock<SqlDataReader> dataReader = new Mock<SqlDataReader>();
            dataReader.Setup(x => x.Read()).Returns(() => readToggle).Callback(() => readToggle = false);
            dataReader.Setup(x => x["IdCliente"]).Returns("1");
            dataReader.Setup(x => x["CpfCliente"]).Returns("07772719906");
            dataReader.Setup(x => x["NomeCliente"]).Returns("Arthur");
            dataReader.Setup(x => x["StatusCliente"]).Returns("true");
            var dic = new Dictionary<string, string>();
            _repositoryConnection.Setup(x => x.CommandBusca("BuscaTodosClientes", dic)).Returns(dataReader.Object);

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarTodosClientes();

            //Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void BuscaTodosClientesTeste_Null()
        {
            //Arrange

            //Action
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            var result = repo.BuscarTodosClientes();

            //Assert
            Assert.IsNull(result);

        }
    }
}
