namespace TMP.BNK.Model
{
    public class OperationResponse
    {
        public bool IsSuccess
        {
            get
            {
                return (ErrorCode <= 0);
            }
        }
        
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
