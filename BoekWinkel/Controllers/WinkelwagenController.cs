using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoekWinkel.Data;
using BoekWinkel.ViewModels;
using BoekWinkel.Models;
using System.Security.Claims;

namespace BoekWinkel.Controllers
{
    public class WinkelwagenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinkelwagenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Winkelwagen
        public async Task<IActionResult> Index(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Redirect("/Identity/Account/Login");
            }

            string loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser == null || loggedInUser != userId) 
            {
                return Unauthorized();
            }

            //kijken of het ingelogde gebruikersId gelijk is met id in de DB en hij in je winkelmand staat 
            var winkelWagenItems = await _context.Winkelwagen
                .Where(w => w.gebruikersId == userId && w.InWinkelwagen == true 
                && w.AantalItems > 0 && w.Betaald == false)
                .Include(w => w.Boek) // voegt het boekModel toe
                .ToListAsync();

            int totaalAantalItems = winkelWagenItems.Sum(item => item.AantalItems);

            decimal totalePrijs = winkelWagenItems.Sum(item => item.AantalItems * item.Boek.BoekPrice);

            var WinkelwagenViewModel = new WinkelwagenViewModel
            {
                WinkelwagenItems = winkelWagenItems,
                TotalePrijs = totalePrijs
            };

            return View(WinkelwagenViewModel);
        }  

        // POST: Winkelwagen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string UserId, [Bind("WinkelwagenId,gebruikersId,BoekId,AantalItems,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser == null || UserId != loggedInUser)
            {
                return Unauthorized();
            }
            

            if (id != winkelwagen.WinkelwagenId)
            {
                return BadRequest();
            }

            // Haal het huidige winkelwagen item op uit de database
            var existingWinkelwagen = await _context.Winkelwagen.FindAsync(id);

            if (existingWinkelwagen == null)
            {
                return NotFound();
            }

            
            existingWinkelwagen.AantalItems = winkelwagen.AantalItems;

            if(winkelwagen.AantalItems < 0)
            {
                return BadRequest();
            }

            // Hier halen we de voorraad op door middel van een join
            var voorraadInfo = await (
                from voorraad in _context.VoorRaadBoeken
                where voorraad.boekId == winkelwagen.BoekId
                select voorraad.voorRaad
            ).FirstOrDefaultAsync();

            if (winkelwagen.AantalItems > voorraadInfo)
            {               
                //return Unauthorized();
                return RedirectToAction("Index", new {UserId = UserId});
            }

            // hij doet het alleen als het model niet valid is , maar de update werkt wel
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingWinkelwagen);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", new { UserId = UserId}); // Terug naar de winkelwagen indexpagina
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Winkelwagen.Any(e => e.WinkelwagenId == winkelwagen.WinkelwagenId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(winkelwagen); // Als de validatie faalt, blijf op de edit pagina
        }        

        // POST: Winkelwagen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(UserId))
            {
                return Redirect("/Identity/Account/Login");
            }          

            var winkelwagen = await _context.Winkelwagen.FindAsync(id);
            if (winkelwagen != null)
            {
                _context.Winkelwagen.Remove(winkelwagen);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

       //GET Winkelwagen/Order
       public async Task<IActionResult> Order(string userId)
       {
            var Order = await _context.Winkelwagen
                .Where(w => w.gebruikersId == userId && w.InWinkelwagen == true
                && w.AantalItems > 0 && w.Betaald == false)
                .Include(w => w.Boek) // voegt het boekModel toe                
                .ToListAsync();

            return View(Order);
       }
        
        private bool WinkelwagenExists(int id)
        {
            return _context.Winkelwagen.Any(e => e.WinkelwagenId == id);
        }
    }
}
