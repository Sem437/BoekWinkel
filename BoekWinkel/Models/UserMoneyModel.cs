using Microsoft.EntityFrameworkCore;
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
        public required string LinkedUser { get; set; }

        public string? Land { get; set; }

        public string? Regio_Provincie { get; set; }

        public string? Stad { get; set; }

        public string? Postcode { get; set; }

        public string? Straatnaam { get; set; }

        public string? Voornaam { get; set; }

        public string? TussenVoegsel { get; set; }

        public string? Achternaam { get; set; }
    }
}
