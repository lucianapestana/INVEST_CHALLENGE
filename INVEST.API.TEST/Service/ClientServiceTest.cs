using INVEST.API.Service;
using INVEST.API.TEST.Repository;
using INVEST.BUSINESSLOGIC.Models;
using Moq;
using Xunit;

namespace INVEST.API.TEST.Service
{
    public class ClientServiceTest
    {
        private ApiMockRepository _mock;
        private ClientService _service;

        public ClientServiceTest()
        {
            _mock = new ApiMockRepository();
            _service = new ClientService(_mock.ClientRepository.Object);
        }

        [Theory(DisplayName = "Validate the client's login - Success.")]
        [InlineData("adm", "adm123")]
        [Trait("ClientService", "GetLoginClient")]
        public async Task GetLogin_Client_Success(string username, string password)
        {
            // Arrange
            var _input = new LoginClient()
            {
                Username = username,
                Password = password
            };

            _mock.ClientRepository.Setup(x => x.GetLoginClient(It.IsAny<LoginClient>()))
            .ReturnsAsync(1);
            
            // Act
            var result = await _service.GetLoginClient(_input);

            // Assert
            Assert.True(result != null);
            Assert.True(result?.LoginClient?.ClientId > 0);
        }

        [Theory(DisplayName = "Validate the client's login - Invalid Client.")]
        [InlineData("", "adm123")]
        [InlineData("adm", "")]
        [Trait("ClientService", "GetLoginClient")]
        public async Task GetLogin_Client_Invalid_Client(string username, string password)
        {
            // Arrange
            var _input = new LoginClient()
            {
                Username = username,
                Password = password
            };

            _mock.ClientRepository.Setup(x => x.GetLoginClient(It.IsAny<LoginClient>()))
            .ReturnsAsync(0);

            // Act
            var result = await _service.GetLoginClient(_input);

            // Assert
            Assert.True(result.LoginClient == null);
        }

        [Theory(DisplayName = "Validate the client's login - Error.")]
        [InlineData("adm", "adm123")]
        [Trait("ClientService", "GetLoginClient")]
        public async Task GetLogin_Client_Error(string username, string password)
        {
            // Arrange
            var _input = new LoginClient()
            {
                Username = username,
                Password = password
            };

            _mock.ClientRepository.Setup(x => x.GetLoginClient(It.IsAny<LoginClient>()))
            .ThrowsAsync(new Exception("Ocorreu um erro ao efetuar o login"));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetLoginClient(_input));

            Assert.Equal("Ocorreu um erro ao efetuar o login", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("Ocorreu um erro ao efetuar o login", exception.InnerException.Message);
        }

        [Theory(DisplayName = "Get the clients - Success.")]
        [InlineData(1, "administrador")]
        [Trait("ClientService", "GetClients")]
        public async Task GetClients_Success(int? idClient, string? nameClient)
        {
            // Arrange
            _mock.ClientRepository.Setup(x => x.GetClients(It.IsAny<int?>(), It.IsAny<string?>()))
            .ReturnsAsync(new List<Client>() { new Client() { ClientId = 1 } });

            // Act
            var result = await _service.GetClients(idClient, nameClient);

            // Assert
            Assert.True(result != null);
            Assert.True(result?.Clients?.Count > 0);
        }

        [Theory(DisplayName = "Get the clients - Error.")]
        [InlineData(1, "administrador")]
        [Trait("ClientService", "GetClients")]
        public async Task GetClients_Error(int? idClient, string? nameClient)
        {
            // Arrange
            _mock.ClientRepository.Setup(x => x.GetClients(It.IsAny<int?>(), It.IsAny<string?>()))
            .ThrowsAsync(new Exception("Ocorreu um erro ao obter os registros"));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetClients());

            Assert.Equal("Ocorreu um erro ao obter os registros", exception.Message);
            Assert.NotNull(exception.InnerException);
            Assert.Equal("Ocorreu um erro ao obter os registros", exception.InnerException.Message);
        }
    }
}
