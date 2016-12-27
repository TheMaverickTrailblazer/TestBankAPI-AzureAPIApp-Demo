namespace TMP.BNK.Model
{
    public class AccountRequest
    {
        public int ClientId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
