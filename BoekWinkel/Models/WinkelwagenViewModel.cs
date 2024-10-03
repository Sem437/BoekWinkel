using BoekWinkel.Models;

namespace BoekWinkel.ViewModels
{
    public class WinkelwagenViewModel
    {
        public List<Winkelwagen> WinkelwagenItems { get; set; }
        public decimal TotalePrijs { get; set; }
        public BoekModel Boek { get; set; }
    }
}
