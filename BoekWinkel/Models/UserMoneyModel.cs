using System.ComponentModel.DataAnnotations;

namespace BoekWinkel.Models
{
    public class UserMoneyModel
    {
        [Key]
        public int UserMoneyId { get; set; }

        [Required]
        public decimal Money { get; set; } 

        [Required]
        public string LinkedUser { get; set; }
    }
}
