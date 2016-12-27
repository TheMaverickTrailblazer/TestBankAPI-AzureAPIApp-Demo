using System.Collections.Generic;

using TMP.BNK.Model;

namespace TMP.BNK.DataService
{
    public interface IAccountDataService
    {
        IEnumerable<Account> GetAccounts(int clientId);
        Account GetAccountDetails(int clientId, string accountNumber);
        bool CreateAccount(int clientId, Account account);
        bool DeteteAccount(int clientId, string accountNumber);
        bool Deposit(AccountRequest depositRequest);
        bool Withdraw(AccountRequest withdrawRequest);
    }
}
