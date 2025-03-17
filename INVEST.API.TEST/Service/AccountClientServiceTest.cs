using INVEST.API.Service;
using INVEST.API.TEST.Repository;
using INVEST.BUSINESSLOGIC.Models;
using Moq;
using Xunit;

namespace INVEST.API.TEST.Service
{
    public class AccountClientServiceTest
    {
        private ApiMockRepository _mock;
        private AccountClientService _service;

        public AccountClientServiceTest()
        {
            _mock = new ApiMockRepository();
            _service = new AccountClientService(_mock.AccountClientRepository.Object);
        }

        [Theory(DisplayName = "Validate the client's account balance in the order - Success.")]
        [InlineData(1, 100)]
        [Trait("AccountClientService", "ValidateBalanceAccountClientOrder")]
        public async Task ValidateBalanceOrder_AccountClient_Success(int accountClientId, decimal totalProduct)
        {
            // Arrange
            _mock.AccountClientRepository.Setup(x => x.GetAccountsClients(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<int?>()))
            .ReturnsAsync(new List<AccountClient>() { new AccountClient() { AccountClientId = 1, Balance = 2000 } });

            // Act
            var result = await _service.ValidateBalanceAccountClientOrder(accountClientId, totalProduct);

            // Assert
            Assert.True(result?.AccountsClients?.Count > 0);
            Assert.True(result?.AccountsClients?.FirstOrDefault()?.Balance >= 0);
            Assert.False(result.Errors.Count() > 0);
        }

        [Theory(DisplayName = "Validate the customer's account balance in the order - Error Balance.")]
        [InlineData(1, 300)]
        [Trait("AccountClientService", "ValidateBalanceAccountClientOrder")]
        public async Task ValidateBalanceOrder_AccountClient_BalanceError(int accountClientId, decimal totalProduct)
        {
            // Arrange
            _mock.AccountClientRepository.Setup(x => x.GetAccountsClients(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<int?>()))
            .ReturnsAsync(new List<AccountClient>() { new AccountClient() { AccountClientId = 1, Balance = 200 } });

            // Act
            var result = await _service.ValidateBalanceAccountClientOrder(accountClientId, totalProduct);

            // Assert
            Assert.True(result.Errors.Any());
            Assert.Equal("Saldo insuficiente! O total da compra é maior que o saldo disponível.", result.Errors.First().Message);
        }

        [Theory(DisplayName = "Validate the customer's account balance in the order - Error Invalid Account.")]
        [InlineData(0, 100)]
        [Trait("AccountClientService", "ValidateBalanceAccountClientOrder")]
        public async Task ValidateBalanceOrder_AccountClient_AccountIdError(int accountClientId, decimal totalProduct)
        {
            // Arrange
            _mock.AccountClientRepository.Setup(x => x.GetAccountsClients(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<int?>()))
            .ReturnsAsync(new List<AccountClient>());

            // Act
            var result = await _service.ValidateBalanceAccountClientOrder(accountClientId, totalProduct);

            // Assert
            Assert.True(result?.AccountsClients?.Count == 0);
        }
    }
}
