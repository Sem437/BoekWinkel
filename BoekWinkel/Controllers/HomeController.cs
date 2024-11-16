using BoekWinkel.Data;
using BoekWinkel.Data.Migrations;
using BoekWinkel.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace BoekWinkel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
       
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {           
            return View(await _context.BoekModel.ToListAsync());
        }


        //Details 
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
            
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.gebruikersId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewBag.ProductId = Id;
            }

            var voorraadBoek = await _context.VoorRaadBoeken
               .FirstOrDefaultAsync(v => v.boekId == Id);

            var winkelwagenItem = await _context.Winkelwagen
                .FirstOrDefaultAsync(w => w.gebruikersId == User.FindFirstValue(ClaimTypes.NameIdentifier) && w.BoekId == Id);

            ViewModel viewModel = new ViewModel
            {
                BoekModel = boekDetails,
                voorRaadBoeken = voorraadBoek,
                Winkelwagen = winkelwagenItem
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
        // POST: Winkelwagen/Create
        [HttpPost]
        public async Task<IActionResult> add(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Identity","Account", "Login"); 
            }

            // Controleer voorraad
            var voorRaad = await _context.VoorRaadBoeken
                .Where(v => v.boekId == Id)
                .Select(v => v.voorRaad)
                .FirstOrDefaultAsync();

            if (voorRaad <= 0)
            {
                TempData["Error"] = "Dit boek is niet meer op voorraad.";
                return RedirectToAction("Details", new { Id = Id });
            }

            var winkelwagenItem = await _context.Winkelwagen
                .FirstOrDefaultAsync(w => w.gebruikersId == userId && w.BoekId == Id && w.Betaald == false && w.InWinkelwagen == true);

            if (winkelwagenItem != null)
            {
                // Update het aantal als het boek al in de winkelwagen zit
                winkelwagenItem.AantalItems += 1; // Verhoog het aantal met 1
                _context.Update(winkelwagenItem);
            }
            else
            {
                //GET userMoneyModel
                var userMoney = await _context.UserMoneyModel
                                .FirstOrDefaultAsync(u => u.LinkedUser == userId);


                // Voeg een nieuw record toe als het boek niet in de winkelwagen zit
                var nieuwWinkelwagenItem = new Winkelwagen
                {
                    gebruikersId = userId,
                    BoekId = Id,
                    InWinkelwagen = true,
                    Betaald = false,
                    AantalItems = 1,
                    UserMoney = userMoney // Stel dit altijd in
                };

                _context.Add(nieuwWinkelwagenItem);
            }

            var inDB = _context.Winkelwagen
                .FirstOrDefault(w => w.gebruikersId == userId && w.BoekId == Id);


            if(inDB != null)
            {
                // edit winkelwagen en zet InWinkelwagen true
                if(inDB.InWinkelwagen == false)
                {
                    inDB.InWinkelwagen = true;
                    _context.Update(inDB);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "winkelwagen", new { userId = userId });
                }                
                else 
                {
                    return RedirectToAction("Index", "Winkelwagen", new { userId = userId });                  
                }
            }

  
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = Id;

            return RedirectToAction("Index", "Winkelwagen", new { userId = userId });
        }

        //toevoegen aan verlanglijst
        // POST: Wishlist/Create
        [HttpPost("home/AddToList")]
        public async Task<IActionResult> AddToList(int Id) 
        {                      
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Identity", "Account", "Login");
            }            

            // Haal de waarden uit de Request.Form
            var gebruikersId = Request.Form["GebruikersId"];
            var productId = Request.Form["ProductId"];
            var opVerlanglijst = bool.Parse(Request.Form["OpVerlanglijst"]);

            var verlanglijstModel = new VerlanglijstModel
            {
                GebruikersId = gebruikersId,
                ProductId = int.Parse(productId),
                OpVerlanglijst = opVerlanglijst
            };           

            if (ModelState.IsValid)
            {
                _context.Add(verlanglijstModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Whislist", new {userId = gebruikersId});
            }

            return RedirectToAction("details", new {Id = Id});
        }
    }
}
