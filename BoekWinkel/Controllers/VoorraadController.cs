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
    public class VoorraadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoorraadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Voorraad
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VoorRaadBoeken.Include(v => v.BoekModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Voorraad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorRaadBoeken = await _context.VoorRaadBoeken
                .Include(v => v.BoekModel)
                .FirstOrDefaultAsync(m => m.voorraadId == id);
            if (voorRaadBoeken == null)
            {
                return NotFound();
            }

            return View(voorRaadBoeken);
        }

        // GET: Voorraad/Create
        public IActionResult Create()
        {
            ViewData["boekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor");
            return View();
        }

        // POST: Voorraad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("voorraadId,boekId,voorRaad,verkocht,geretourd")] VoorRaadBoeken voorRaadBoeken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voorRaadBoeken);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["boekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", voorRaadBoeken.boekId);
            return View(voorRaadBoeken);
        }

        // GET: Voorraad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorRaadBoeken = await _context.VoorRaadBoeken.FindAsync(id);
            if (voorRaadBoeken == null)
            {
                return NotFound();
            }
            ViewData["boekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", voorRaadBoeken.boekId);
            return View(voorRaadBoeken);
        }

        // POST: Voorraad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("voorraadId,boekId,voorRaad,verkocht,geretourd")] VoorRaadBoeken voorRaadBoeken)
        {
            if (id != voorRaadBoeken.voorraadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voorRaadBoeken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoorRaadBoekenExists(voorRaadBoeken.voorraadId))
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
            ViewData["boekId"] = new SelectList(_context.BoekModel, "BoekId", "BoekAuthor", voorRaadBoeken.boekId);
            return View(voorRaadBoeken);
        }

        // GET: Voorraad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voorRaadBoeken = await _context.VoorRaadBoeken
                .Include(v => v.BoekModel)
                .FirstOrDefaultAsync(m => m.voorraadId == id);
            if (voorRaadBoeken == null)
            {
                return NotFound();
            }

            return View(voorRaadBoeken);
        }

        // POST: Voorraad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voorRaadBoeken = await _context.VoorRaadBoeken.FindAsync(id);
            if (voorRaadBoeken != null)
            {
                _context.VoorRaadBoeken.Remove(voorRaadBoeken);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoorRaadBoekenExists(int id)
        {
            return _context.VoorRaadBoeken.Any(e => e.voorraadId == id);
        }
    }
}
