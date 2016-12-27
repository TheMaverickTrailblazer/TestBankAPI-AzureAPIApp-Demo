using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TMP.BNK.Model;
using TMP.BNK.Core;
using TMP.BNK.DataService;
using Moq;

namespace TMP.BNK.Service.Tests
{
    [TestClass()]
    public class AccountServiceTests
    {
        IAccountService _accountService;
        Mock<IAccountDataService> _accountDataServiceMock;
        List<Account> _accounts = new List<Account>(){
                new Account { Number="CA123456789", Title="John Smith - Checking Account", Balance=1500, ClientId=12345, Type="Checkiing"},
                new Account { Number = "SA987654223", Title = "Jonh Smith - Saving Account", Balance = 1200 , ClientId=12345, Type="Saving"},
                new Account { Number = "SA987654321", Title = "David Smith", Balance = 2300 , ClientId=12345, Type="Saving"},
                new Account { Number = "CA224455667", Title = "Karthik Jambulingam - Deposit Account", Balance = 120 , ClientId=55555, Type="Deposit"}
            };
        int _clientId = 12345;

        public AccountServiceTests()
        {
            //Arrange
            _accountDataServiceMock = new Mock<IAccountDataService>();
            _accountDataServiceMock.Setup(service => service.Deposit(It.IsAny<AccountRequest>())).Returns(true);
            _accountDataServiceMock.Setup(service => service.Withdraw(It.IsAny<AccountRequest>())).Returns(true);
            _accountService = new AccountService(_accountDataServiceMock.Object);
        }

        [TestMethod()]
        public void DepositValidInputTest()
        {
            //Arrange
            var expectedResponseCode = 0;
            var depositRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = "CA123456789",
                Amount = 500
            };
            //Act
            var response = _accountService.Deposit(depositRequest);
            //Assert
            Assert.AreEqual(expectedResponseCode, response.ErrorCode, "Deposit limit check, positive scenario failed!");

        }

        [TestMethod()]
        public void DepositLimitExceededTest()
        {
            //Arrange
            var expectedResponseCode = ErrorCodes.DEPOSIT_LIMIT_EXCEEDED;
            var depositRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = "CA123456789",
                Amount = 10001
            };
            //Act
            var response = _accountService.Deposit(depositRequest);
            //Assert
            Assert.AreEqual(expectedResponseCode, response.ErrorCode, "Deposit limit exceeded check failed!");

        }

        [TestMethod()]
        public void WithdrawValidInputTest()
        {
            //Arrange
            var expectedResponseCode = 0;
            var withdrawRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = "SA987654223",
                Amount = 800
            };
            var accountService = getWithdrawalService(withdrawRequest.ClientId, withdrawRequest.AccountNumber);

            //Act
            var response = accountService.Withdraw(withdrawRequest);
            //Assert
            Assert.AreEqual(expectedResponseCode, response.ErrorCode, "Withraw limit check, positive scenario failed!");

        }
        [TestMethod()]
        public void WithdrawLimitExceededTest()
        {
            //Arrange
            var expectedResponseCode = ErrorCodes.WITHDRAW_LIMIT_EXCEEDED;
            var withdrawRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = "SA987654223",
                Amount = 1100
            };
            var accountService = getWithdrawalService(withdrawRequest.ClientId, withdrawRequest.AccountNumber);

            //Act
            var response = accountService.Withdraw(withdrawRequest);
            //Assert
            Assert.AreEqual(expectedResponseCode, response.ErrorCode, "Withraw limit exceeded check failed!");

        }

        [TestMethod()]
        public void WithdrawMinBalanceExceededTest()
        {
            //Arrange
            var expectedResponseCode = ErrorCodes.MINIMUM_BALANCE_EXCEEDED;
            var withdrawRequest = new AccountRequest()
            {
                ClientId = 55555,
                AccountNumber = "CA224455667",
                Amount = 50
            };
            var accountService = getWithdrawalService(withdrawRequest.ClientId, withdrawRequest.AccountNumber);

            //Act
            var response = accountService.Withdraw(withdrawRequest); ;
            //Assert
            Assert.AreEqual(expectedResponseCode, response.ErrorCode, "Withraw: minimum balance check failed!");

        }

        public IAccountService getWithdrawalService(int clientId, string accountNumber)
        {
            var givenAccount = _accounts.Where(account => account.ClientId == clientId && account.Number == accountNumber).FirstOrDefault();
            var accountDataServiceMock = _accountDataServiceMock;
            accountDataServiceMock.Setup(service => service.GetAccountDetails(It.IsAny<int>(), It.IsAny<string>())).Returns(givenAccount);
            var accountService = new AccountService(accountDataServiceMock.Object);
            return accountService;
        }

        //[TestMethod()]
        //public void AccountServiceTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void AccountServiceTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void GetAccountsTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void GetAccountDetailsTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void CreateAccountTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void DeteteAccountTest()
        //{
        //    throw new NotImplementedException();
        //}
    }
}