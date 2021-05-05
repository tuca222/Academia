using Academia.Interfaces;
using Academia.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
        public void BuscaTodosClientesTest()
        {
            var repo = new ClienteRepositorio(_enderecoClienteRepositorio.Object, _configurationMock.Object, _repositoryConnection.Object);
            Assert.IsNotNull(repo.BuscarTodosClientes());

        }
    }
}
