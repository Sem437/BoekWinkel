﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoekWinkel.Models
{
    public class Winkelwagen
    {
        [Key]
        public int WinkelwagenId { get; set; }

        public string gebruikersId { get; set; }

        public int BoekId { get; set; }

        public int AantalItems { get; set; } = 1;

        public bool InWinkelwagen { get; set; } = false;

        public bool Betaald { get; set; } = false;

        public BoekModel Boek { get; set; }

        public UserMoneyModel UserMoney { get; set; }
    }
}
