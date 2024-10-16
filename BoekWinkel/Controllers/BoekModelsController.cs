using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoekWinkel.Data;
using BoekWinkel.Models;
using BoekWinkel.Data.Migrations;
using Microsoft.AspNetCore.Authorization;

namespace BoekWinkel.Controllers
{
    public class BoekModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoekModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]

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
        public async Task<IActionResult> Create([Bind("BoekId,BoekTitle,BoekAuthor,BoekDescription,BoekPrice,BoekCategory,BoekImageURL,BoekImage")] BoekWinkel.Models.BoekModel boekModel, IFormFile BoekImage)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {

                if ((BoekImage == null && string.IsNullOrEmpty(boekModel.BoekImageURL)) ||
                (BoekImage != null && !string.IsNullOrEmpty(boekModel.BoekImageURL)))
                {
                    ModelState.AddModelError("", "Please provide either an image file or an image URL, but not both.");
                    return View(boekModel);
                }
          
                
                // Verwerking van afbeelding als Base64-string
                if (BoekImage != null && BoekImage.Length > 0 && boekModel.BoekImageURL == null)
                {
                    if (BoekImage.ContentType.StartsWith("image/") && BoekImage.Length <= 5 * 1024 * 1024) // max grote van afbeelding is 5mb
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await BoekImage.CopyToAsync(memoryStream);
                            byte[] fileBytes = memoryStream.ToArray();
                            boekModel.BoekImage = Convert.ToBase64String(fileBytes);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Image must be a valid image file and less than 5MB.");
                        return View(boekModel);
                    }
                }
                

                    // Voeg het boekmodel toe aan de database
                    _context.Add(boekModel);
                    await _context.SaveChangesAsync();

                    // Nu het boek is opgeslagen voeg het voorraaditem toe
                    var voorraad = new VoorRaadBoeken
                    {
                        boekId = boekModel.BoekId,
                        voorRaad = 0,
                        verkocht = 0,
                        geretourd = 0
                    };

                    _context.Add(voorraad);
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
        public async Task<IActionResult> Edit(int id, [Bind("BoekId,BoekTitle,BoekAuthor,BoekDescription,BoekPrice,BoekCategory,BoekImageURL,BoekImage")] BoekWinkel.Models.BoekModel boekModel)
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
