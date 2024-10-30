using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoekWinkel.Data;
using BoekWinkel.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BoekWinkel.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WishlistController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        // GET: Wishlist
        public async Task<IActionResult> Index(string? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            string loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser == null || loggedInUser != userId)
            {
                return Unauthorized();
            }

            var userWishList = await _context.VerlanglijstModel                
                .Where(u => u.GebruikersId == userId)
                .Where(u => u.OpVerlanglijst == true)   
                .Include(u => u.Boek)
                .ToListAsync();           

            return View(userWishList);
        }


        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(string? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            string loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(loggedInUser == null || loggedInUser != userId)
            {
                return Unauthorized();
            }

            var userWishList = await _context.VerlanglijstModel
                .Where(u => u.GebruikersId == userId)
                .Where(u => u.OpVerlanglijst == true)
                .ToListAsync();

            return View(userWishList);
        }

        // GET: Wishlist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VerlanglijstId,GebruikersId,ProductId,OpVerlanglijst")] VerlanglijstModel verlanglijstModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verlanglijstModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(verlanglijstModel);
        }

        // GET: Wishlist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verlanglijstModel = await _context.VerlanglijstModel.FindAsync(id);
            if (verlanglijstModel == null)
            {
                return NotFound();
            }
            return View(verlanglijstModel);
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VerlanglijstId,GebruikersId,ProductId,OpVerlanglijst")] VerlanglijstModel verlanglijstModel)
        {
            if (id != verlanglijstModel.VerlanglijstId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verlanglijstModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerlanglijstModelExists(verlanglijstModel.VerlanglijstId))
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
            return View(verlanglijstModel);
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verlanglijstModel = await _context.VerlanglijstModel
                .FirstOrDefaultAsync(m => m.VerlanglijstId == id);
            if (verlanglijstModel == null)
            {
                return NotFound();
            }

            return View(verlanglijstModel);
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verlanglijstModel = await _context.VerlanglijstModel.FindAsync(id);
            if (verlanglijstModel != null)
            {
                _context.VerlanglijstModel.Remove(verlanglijstModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Wishlist/Remove/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var verlanglijstModel = await _context.VerlanglijstModel.FindAsync(id);
            if (verlanglijstModel != null)
            {
                _context.VerlanglijstModel.Remove(verlanglijstModel);
            }

            var logedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { userId =logedInUser });
        }

        private bool VerlanglijstModelExists(int id)
        {
            return _context.VerlanglijstModel.Any(e => e.VerlanglijstId == id);
        }
    }
}
