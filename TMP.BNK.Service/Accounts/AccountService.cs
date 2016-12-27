using System.Collections.Generic;

using TMP.BNK.Core;
using TMP.BNK.Model;
using TMP.BNK.DataService;

namespace TMP.BNK.Service
{
    public class AccountService : IAccountService
    {
        IAccountDataService _accountDataService;

        public AccountService()
        {
            _accountDataService = new AccountDataService();
        }
        public AccountService(IAccountDataService accountDataService)
        {
            _accountDataService = accountDataService;
        }

        public IEnumerable<Account> GetAccounts(int clientId)
        {
            return _accountDataService.GetAccounts(clientId);
        }

        public Account GetAccountDetails(int clientId, string accountNumber)
        {
            return _accountDataService.GetAccountDetails(clientId, accountNumber);
        }

        public OperationResponse CreateAccount(int clientId, Account account)
        {
            var createResponse = _accountDataService.CreateAccount(clientId, account);
            return MapOperationResponse(createResponse);
        }

        public OperationResponse DeteteAccount(int clientId, string accountNumber)
        {
            var deleteResponse = _accountDataService.DeteteAccount(clientId, accountNumber);
            return MapOperationResponse(deleteResponse);
        }

        public OperationResponse Deposit(AccountRequest depositRequest)
        {
            var validatationResult = ValidateDepositRequest(depositRequest);
            if (!validatationResult.IsSuccess)
            {
                return validatationResult;
            }

            var depositResponse = _accountDataService.Deposit(depositRequest);
            return MapOperationResponse(depositResponse);

        }
        public OperationResponse Withdraw(AccountRequest withdrawRequest)
        {
            var validatationResult = ValidateWithdrawRequest(withdrawRequest);
            if (!validatationResult.IsSuccess)
            {
                return validatationResult;
            }

            var withdrawResponse = _accountDataService.Withdraw(withdrawRequest);
            return MapOperationResponse(withdrawResponse);
        }

        private OperationResponse MapOperationResponse(bool result)
        {
            var response = new OperationResponse();
            if (!result)
            {
                response.ErrorCode = ErrorCodes.OPERATION_FAILED;
            }
            return response;
        }
        private OperationResponse ValidateDepositRequest(AccountRequest request)
        {
            var response = new OperationResponse();

            if (request.Amount > Constants.DEPOSIT_LIMIT)
            {
                response.ErrorCode = ErrorCodes.DEPOSIT_LIMIT_EXCEEDED;
            }
            return response;
        }

        private OperationResponse ValidateWithdrawRequest(AccountRequest request)
        {
            var response = new OperationResponse();
            var account = _accountDataService.GetAccountDetails(request.ClientId, request.AccountNumber);
            if (account == null)
            {
                response.ErrorCode = ErrorCodes.INVALID_ENTITY;
                return response;
            }
            var balanceLimit = (((decimal)Constants.WITHDRAW_LIMIT_PERCENTAGE / 100) * account.Balance);
            if (request.Amount > balanceLimit)
            {
                response.ErrorCode = ErrorCodes.WITHDRAW_LIMIT_EXCEEDED;
            }
            else if ((account.Balance - request.Amount) < Constants.MINIMUM_BALANCE)
            {
                response.ErrorCode = ErrorCodes.MINIMUM_BALANCE_EXCEEDED;
            }
            return response;
        }

    }
}
