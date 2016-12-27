using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

using TMP.BNK.Model;
using TMP.BNK.Service;

namespace TMP.UI.Service.Accounts
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        IAccountService _accountService = new AccountService();
        int _clientId = 12345; //TODO: To be retrieved from Identity

        // GET: api/Accounts
        [HttpGet]
        [Route("")]
        public IEnumerable<Account> Get()
        {
            return _accountService.GetAccounts(_clientId);
        }

        // GET: api/Accounts/5
        [HttpGet]
        [Route("{number:length(11)}")]
        public IHttpActionResult Get(string number)
        {
            var account = _accountService.GetAccountDetails(_clientId, number);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // POST: api/Accounts
        [HttpPost]
        [Route("")]
        public OperationResponse Post([FromBody]Account account)
        {
            return (_accountService.CreateAccount(_clientId, account));
        }

        // DELETE: api/Accounts/5
        [HttpDelete]
        [Route("{number}")]
        public OperationResponse Delete(string number)
        {
            return _accountService.DeteteAccount(_clientId, number);
        }

        // POST: api/Accounts/{number}/deposit
        [HttpPost]
        [Route("{number}/deposit/{amount}")]
        public OperationResponse Deposit(string number, int amount)
        {
            var depositRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = number,
                Amount = amount
            };
            return (_accountService.Deposit(depositRequest));
        }

        // POST: api/Accounts/{number}/deposit
        [HttpPost]
        [Route("{number}/withdraw/{amount}")]
        public OperationResponse Withdraw(string number, int amount)
        {
            var withdrawRequest = new AccountRequest()
            {
                ClientId = _clientId,
                AccountNumber = number,
                Amount = amount
            };
            return (_accountService.Withdraw(withdrawRequest));
        }

        protected override void Dispose(bool disposing)
        {
            _accountService = null;
            base.Dispose(disposing);
        }
    }
}
