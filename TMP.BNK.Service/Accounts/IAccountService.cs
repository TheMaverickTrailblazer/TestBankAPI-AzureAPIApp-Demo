using System.Collections.Generic;
using TMP.BNK.Model;

namespace TMP.BNK.Service
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts(int clientId);
        Account GetAccountDetails(int clientId, string accountNumber);
        OperationResponse CreateAccount(int clientId, Account account);
        OperationResponse DeteteAccount(int clientId, string accountNumber);
        OperationResponse Deposit(AccountRequest depositRequest);
        OperationResponse Withdraw(AccountRequest withdrawRequest);
    }
}
