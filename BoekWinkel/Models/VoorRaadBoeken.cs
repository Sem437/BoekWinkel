using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoekWinkel.Models
{
    public class VoorRaadBoeken
    {
        [Key]
        public int voorraadId { get; set; }

        [ForeignKey("BoekModel")]
        public int boekId { get; set; }

        public int voorRaad { get; set; } = 0;

        public int verkocht { get; set; } = 0;

        public int geretourd { get; set; } = 0;
        public BoekModel BoekModel { get; set; }  // Koppelt de voorraad aan een boek
    }
}
