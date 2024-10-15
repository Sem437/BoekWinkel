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

            //kijken of het ingelogde gebruikersId gelijk is met id in de DB en hij in je winkelmand staat 
            var winkelWagenItems = await _context.Winkelwagen
                .Where(w => w.gebruikersId == userId && w.InWinkelwagen == true 
                && w.AantalItems > 0 && w.Betaald == false)
                .Include(w => w.Boek) // voegt het boekModel toe
                .ToListAsync();

            
            decimal totalePrijs = winkelWagenItems.Sum(item => item.Boek.BoekPrice);

            var WinkelwagenViewModel = new WinkelwagenViewModel
            {
                WinkelwagenItems = winkelWagenItems,
                TotalePrijs = totalePrijs
            };

            return View(WinkelwagenViewModel);
        }

        // GET: Winkelwagen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winkelwagen = await _context.Winkelwagen
                .Include(w => w.Boek)
                .FirstOrDefaultAsync(m => m.WinkelwagenId == id);
            if (winkelwagen == null)
            {
                return NotFound();
            }

            return View(winkelwagen);
        }

        // GET: Winkelwagen/Create
        public IActionResult Create()
        {
            ViewData["BoekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor");
            return View();
        }

        // POST: Winkelwagen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WinkelwagenId,gebruikersId,BoekId,aantalItems,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(winkelwagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", winkelwagen.BoekId);
            return View(winkelwagen);
        }

        //GET Edit
        public async Task<IActionResult> Edit(int? id, string UserId)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Haal het winkelwagenitem op met het meegegeven ID
            var winkelwagen = await _context.Winkelwagen.FindAsync(id);
            if (winkelwagen == null)
            {
                return NotFound();
            }

            // Geef het winkelwagenitem door aan de view om het formulier in te vullen
            return View(winkelwagen);
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

            // Werk de velden bij die aangepast zijn
            existingWinkelwagen.AantalItems = winkelwagen.AantalItems;

            // hij doet het alleen als het model niet valid is , maar de update werkt wel
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingWinkelwagen);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Terug naar de winkelwagen indexpagina
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


        //oude post
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string UserId, [Bind("WinkelwagenId,gebruikersId,BoekId,aantalItems,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
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

            // Werk de velden bij die aangepast zijn
            existingWinkelwagen.AantalItems = winkelwagen.AantalItems;

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingWinkelwagen);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }

            return View("Index", await _context.Winkelwagen.ToListAsync());
        }

        */

        // GET: Winkelwagen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winkelwagen = await _context.Winkelwagen
                .Include(w => w.Boek)
                .FirstOrDefaultAsync(m => m.WinkelwagenId == id);
            if (winkelwagen == null)
            {
                return NotFound();
            }

            return View(winkelwagen);
        }

        // POST: Winkelwagen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var winkelwagen = await _context.Winkelwagen.FindAsync(id);
            if (winkelwagen != null)
            {
                _context.Winkelwagen.Remove(winkelwagen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WinkelwagenExists(int id)
        {
            return _context.Winkelwagen.Any(e => e.WinkelwagenId == id);
        }
    }
}


public class WinkelwagenService
{
    private readonly ApplicationDbContext _context;

    public WinkelwagenService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Deze functie voert de update uit zonder een IActionResult
    public void UpdateAantalItems(int winkelwagenId, int nieuwAantalItems)
    {
        var winkelwagen = _context.Winkelwagen.Find(winkelwagenId);

        if (winkelwagen != null)
        {
            winkelwagen.AantalItems = nieuwAantalItems;
            _context.SaveChanges(); // Voer de update uit in de database
        }
    }
}
