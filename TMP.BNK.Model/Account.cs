using System.ComponentModel.DataAnnotations;

namespace TMP.BNK.Model
{
    public class Account
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string Title { get; set; }
        public string Type { get; set; } //TODO: should be a enum
        public decimal Balance { get; set; }

        public int ClientId { get; set; } 
    }
}
