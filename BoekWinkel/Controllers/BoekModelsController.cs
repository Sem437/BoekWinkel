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
    public class BoekModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoekModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BoekModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.BoekModel.ToListAsync());
        }

        // GET: BoekModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boekModel = await _context.BoekModel
                .FirstOrDefaultAsync(m => m.BoekId == id);
            if (boekModel == null)
            {
                return NotFound();
            }

            return View(boekModel);
        }

        // GET: BoekModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BoekModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoekId,BoekTitle,BoekAuthor,BoekDescription,BoekPrice,BoekCategory,BoekImageURL,BoekImage")] BoekModel boekModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boekModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(boekModel);
        }

        // GET: BoekModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boekModel = await _context.BoekModel.FindAsync(id);
            if (boekModel == null)
            {
                return NotFound();
            }
            return View(boekModel);
        }

        // POST: BoekModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BoekId,BoekTitle,BoekAuthor,BoekDescription,BoekPrice,BoekCategory,BoekImageURL,BoekImage")] BoekModel boekModel)
        {
            if (id != boekModel.BoekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boekModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoekModelExists(boekModel.BoekId))
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
            return View(boekModel);
        }

        // GET: BoekModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boekModel = await _context.BoekModel
                .FirstOrDefaultAsync(m => m.BoekId == id);
            if (boekModel == null)
            {
                return NotFound();
            }

            return View(boekModel);
        }

        // POST: BoekModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boekModel = await _context.BoekModel.FindAsync(id);
            if (boekModel != null)
            {
                _context.BoekModel.Remove(boekModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoekModelExists(int id)
        {
            return _context.BoekModel.Any(e => e.BoekId == id);
        }
    }
}
