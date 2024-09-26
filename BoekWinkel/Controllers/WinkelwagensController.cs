using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoekWinkel.Data;
using BoekWinkel.Models;

namespace BoekWinkel.Controllers
{
    public class WinkelwagensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinkelwagensController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Winkelwagens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Winkelwagen.ToListAsync());
        }

        // GET: Winkelwagens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winkelwagen = await _context.Winkelwagen
                .FirstOrDefaultAsync(m => m.WinkelwagenId == id);
            if (winkelwagen == null)
            {
                return NotFound();
            }

            return View(winkelwagen);
        }

        // GET: Winkelwagens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Winkelwagens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WinkelwagenId,gebruikersId,BoekId,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(winkelwagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(winkelwagen);
        }

        // GET: Winkelwagens/Edit/5
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
            return View(winkelwagen);
        }

        // POST: Winkelwagens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WinkelwagenId,gebruikersId,BoekId,InWinkelwagen,Betaald")] Winkelwagen winkelwagen)
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
            return View(winkelwagen);
        }

        // GET: Winkelwagens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var winkelwagen = await _context.Winkelwagen
                .FirstOrDefaultAsync(m => m.WinkelwagenId == id);
            if (winkelwagen == null)
            {
                return NotFound();
            }

            return View(winkelwagen);
        }

        // POST: Winkelwagens/Delete/5
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
