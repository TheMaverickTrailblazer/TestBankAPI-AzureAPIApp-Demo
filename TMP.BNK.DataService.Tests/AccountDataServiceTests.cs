using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TMP.BNK.Model;

namespace TMP.BNK.DataService.Tests
{
    [TestClass()]
    public class AccountDataServiceTests
    {
        //Arrange
        IAccountDataService _accountDataService = new AccountDataService();
        List<Account> _expectedAccounts = new List<Account>(){
                new Account { Number="CA123456789", Title="John Smith - Checking Account", Balance=1500, ClientId=12345, Type="Checkiing"},
                new Account { Number = "SA987654223", Title = "Jonh Smith - Saving Account", Balance = 1200 , ClientId=12345, Type="Saving"},
                new Account { Number = "SA987654321", Title = "David Smith", Balance = 2300 , ClientId=12345, Type="Saving"},
                new Account { Number = "CA224455667", Title = "Karthik Jambulingam - Deposit Account", Balance = 2300 , ClientId=55555, Type="Deposit"}
            };
        int _clientId = 12345;

        [TestMethod()]
        public void GetAccountsValidCountTest()
        {
            //Arrange
            var expectedAccountCount = 3;
            //Act
            var actualAccountsCount = _accountDataService.GetAccounts(_clientId).ToList();

            //Assert
            Assert.AreEqual(expectedAccountCount, actualAccountsCount.Count, "Number of accounts returned doesn't match!");
        }

        [TestMethod()]
        public void GetAccountsZeroCountTest()
        {
            //Arrange
            var expectedAccountCount = 0;
            var clientId = 0;
            //Act
            var actualAccountsCount = _accountDataService.GetAccounts(clientId).ToList();

            //Assert
            Assert.AreEqual(expectedAccountCount, actualAccountsCount.Count, "Number of accounts returned should be zero!");
        }


        [TestMethod()]
        public void GetAccountDetailsInvalidInputTest()
        {
            //Arrange
            var clientId = 0;
            var accountNumber = "";
            //Act
            var actualAccount = _accountDataService.GetAccountDetails(clientId, accountNumber);

            //Assert
            Assert.IsNull(actualAccount, "Input checks logic is broken!");
        }        

        [TestMethod()]
        public void DepositTest()
        {
            //Arrange
            var expectedBalance = 2200;
            var depositRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = "SA987654223",
                Amount = 1000
            };

            //Act
            _accountDataService.Deposit(depositRequest);
            var depositedAccount = _accountDataService.GetAccountDetails(_clientId, depositRequest.AccountNumber);
            //Assert
            Assert.AreEqual(expectedBalance, depositedAccount.Balance, "Deposit logic is broken!");

        }
    }
}