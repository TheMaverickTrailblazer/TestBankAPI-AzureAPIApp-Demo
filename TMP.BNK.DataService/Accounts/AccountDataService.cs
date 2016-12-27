using System.Collections.Generic;
using System.Linq;

using TMP.BNK.Model;

namespace TMP.BNK.DataService
{
    public class AccountDataService : IAccountDataService
    {
        static List<Account> _accounts = new List<Account>(){
                new Account { Number="CA123456789", Title="John Smith - Checking Account", Balance=1500, ClientId=12345, Type="Checkiing"},
                new Account { Number = "SA987654223", Title = "Jonh Smith - Saving Account", Balance = 1200 , ClientId=12345, Type="Saving"},
                new Account { Number = "SA987654321", Title = "David Smith", Balance = 2300 , ClientId=12345, Type="Saving"},
                new Account { Number = "CA224455667", Title = "Karthik Jambulingam - Deposit Account", Balance = 120 , ClientId=55555, Type="Deposit"}
            };
        static long _accountNumberSeed = 10000000000;
        public IEnumerable<Account> GetAccounts(int clientId)
        {
            return _accounts.Where(a => a.ClientId == clientId);
        }

        public Account GetAccountDetails(int clientId, string accountNumber)
        {
            if (clientId <= 0 || accountNumber == null)
            {
                return null;
            }
            return _accounts.Where(a => a.ClientId == clientId && a.Number == accountNumber).FirstOrDefault();
        }

        public bool CreateAccount(int clientId, Account account)
        {
            bool result = true;
            if (_accounts.Any(a => a.ClientId == clientId && a.Number == account.Number)) //TODO: need proper duplicate check
            {
                result = false;
            }
            account.Number = GenerateAccountNumber();
            _accounts.Add(account);
            return result;
        }

        private string GenerateAccountNumber()
        {
            _accountNumberSeed += 1;
            return _accountNumberSeed.ToString();
        }

        public bool DeteteAccount(int clientId, string accountNumber)
        {
            var account = _accounts.Where(a => a.Number == accountNumber).FirstOrDefault();
            return _accounts.Remove(account);
        }

        public bool Deposit(AccountRequest request)
        {
            //TODO: This logic supposed to be done in stored proc
            var account = _accounts.Where(a => a.Number == request.AccountNumber).FirstOrDefault();
            if (account == null)
            {
                return false;
            }
            account.Balance += request.Amount;
            return true;
        }
        public bool Withdraw(AccountRequest request)
        {
            var account = _accounts.Where(a => a.Number == request.AccountNumber).FirstOrDefault();
            if (account == null)
            {
                return false;
            }
            account.Balance -= request.Amount;
            return true;
        }
    }
}
