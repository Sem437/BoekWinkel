using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BoekWinkel.Models
{
    public class BoekModel
    {
        [Key]
        public int BoekId { get; set; }

        [Required]
        public required string BoekTitle { get; set; }

        [Required]
        public required string BoekAuthor { get; set; }

        [Required]
        public required string BoekDescription { get; set;}

        [Required]
        public decimal BoekPrice { get; set; }

        [Required]
        public required string BoekCategory { get; set;}

        public string? BoekImageURL { get; set; }

        public string? BoekImage { get; set; }

    }
}
