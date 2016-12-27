namespace TMP.BNK.Core
{
    public static class ErrorCodes
    {
        //Could be enum and message from json
        public const int OPERATION_FAILED = 100;
        public const int DEPOSIT_LIMIT_EXCEEDED = 101;
        public const int WITHDRAW_LIMIT_EXCEEDED = 102;
        public const int MINIMUM_BALANCE_EXCEEDED = 103;
        public const int INVALID_ENTITY = 104;

        public const string OPERATION_FAILED_MESSAGE = "Operation failed.";
        public const string DEPOSIT_LIMIT_EXCEEDED_MESSAGE = "Deposit limit exceeded.";
        public const string WITHDRAW_LIMIT_EXCEEDED_MESSAGE = "Withdrawal limit exceeded.";
        public const string MINIMUM_BALANCE_EXCEEDED_MESSAGE = "Withdrawal exceeds min balalnce limit.";
        public const string INVALID_ENTITY_MESSAGE = "Invalid entity found.";
    }
}
