using BoekWinkel.Data;
using BoekWinkel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BoekWinkel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BoekModel.ToListAsync());
        }

        public async Task<IActionResult> Details(int Id)
        {
            if(Id == 0) 
            {
                return NotFound();
            }

            var boekDetails = await _context.BoekModel
                .FirstOrDefaultAsync(b => b.BoekId == Id);

            if (boekDetails == null)
            {
                return NotFound(); 
            }

            var voorraadBoek = await _context.VoorRaadBoeken
               .FirstOrDefaultAsync(v => v.boekId == Id);

            ViewModel viewModel = new ViewModel
            {
                BoekModel = boekDetails,
                voorRaadBoeken = voorraadBoek
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
