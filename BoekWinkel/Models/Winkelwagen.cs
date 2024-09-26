using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoekWinkel.Models
{
    public class Winkelwagen
    {
        [Key]
        public int WinkelwagenId { get; set; }

        [ForeignKey("Id")]
        public string gebruikersId { get; set; }

        [ForeignKey("BoekId")]
        public int BoekId { get; set; }

        public bool InWinkelwagen { get; set; } = false;

        public bool Betaald { get; set; } = false;
    }
}
