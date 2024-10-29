using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoekWinkel.Models
{
    public class VerlanglijstModel
    {
        [Key]
        public int VerlanglijstId { get; set; }

        [Required]
        public string GebruikersId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public bool OpVerlanglijst { get; set; } = false;

        [ForeignKey(nameof(ProductId))]
        public BoekModel Boek { get; set; }
    }
}
