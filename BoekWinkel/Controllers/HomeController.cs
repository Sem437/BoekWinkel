using BoekWinkel.Data;
using BoekWinkel.Data.Migrations;
using BoekWinkel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Security.Claims;

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



        //toevoegen aan winkelmand
        // POST: Winkelwagens/Create
        [HttpPost]
        public async Task<IActionResult> add(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); 
            }


            var winkelwagen = new Winkelwagen
            {
                gebruikersId = userId,
                BoekId = Id,
                InWinkelwagen = true, 
                Betaald = false       
            };

            if (ModelState.IsValid)
            {
                _context.Add(winkelwagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = Id;

            return View("Details", Id);
        }
    }
}
