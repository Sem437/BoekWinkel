using System.ComponentModel.DataAnnotations;

namespace BoekWinkel.Models
{
    public class BoekModel
    {
        public int BoekId { get; set; }

        [Required]
        public string BoekTitle { get; set; }

        [Required]
        public string BoekAuthor { get; set; }

        [Required]
        public string BoekDescription { get; set;}

        [Required]
        public int BoekPrice { get; set; }

        [Required]
        public string BoekCategory { get; set;}
    
    }
}
