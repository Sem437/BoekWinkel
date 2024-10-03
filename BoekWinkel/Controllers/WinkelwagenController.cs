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

        // GET: Winkelwagen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winkelwagen = await _context.Winkelwagen.FindAsync(id);
            if (winkelwagen == null)
            {
                return NotFound();
            }
            ViewData["BoekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", winkelwagen.BoekId);
            return View(winkelwagen);
        }

        // POST: Winkelwagen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WinkelwagenId,gebruikersId,BoekId,aantalItems,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
        {
            if (id != winkelwagen.WinkelwagenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(winkelwagen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WinkelwagenExists(winkelwagen.WinkelwagenId))
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
            ViewData["BoekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", winkelwagen.BoekId);
            return View(winkelwagen);
        }

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
