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
    public class ManageaccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageaccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: MangeAccount
        public async Task<IActionResult> Index(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != loggedInUser)
            {
                return Unauthorized();
            }

            var userDetails = await _context.UserMoneyModel
                .Where(u => u.LinkedUser == userId)                
                .ToListAsync();            

            return View(userDetails);
        }

        // GET: Manageaccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMoneyModel = await _context.UserMoneyModel
                .FirstOrDefaultAsync(m => m.UserMoneyId == id);
            if (userMoneyModel == null)
            {
                return NotFound();
            }

            return View(userMoneyModel);
        }

        // GET: Manageaccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manageaccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserMoneyId,Money,LinkedUser,Land,Regio_Provincie,Stad,Postcode,Straatnaam,Voornaam,TussenVoegsel,Achternaam")] UserMoneyModel userMoneyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMoneyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMoneyModel);
        }

        // GET: Manageaccount/Edit/5
        public async Task<IActionResult> Edit(int? id, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(userId == null) 
            {
                return NotFound();
            }

            if(userId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            var userMoneyModel = await _context.UserMoneyModel.FindAsync(id);
            if (userMoneyModel == null)
            {
                return NotFound();
            }
            return View(userMoneyModel);
        }

        // POST: Manageaccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string userId, [Bind("UserMoneyId,Money,LinkedUser,Land,Regio_Provincie,Stad,Postcode,Straatnaam,Voornaam,TussenVoegsel,Achternaam")] UserMoneyModel userMoneyModel)
        {
            if (id != userMoneyModel.UserMoneyId)
            {
                return NotFound();
            }

            if(userId == null)
            {
                return RedirectToAction("null");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userMoneyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserMoneyModelExists(userMoneyModel.UserMoneyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new {userId = userId});
            }
            return RedirectToAction("FOUT");
        }

        // GET: Manageaccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMoneyModel = await _context.UserMoneyModel
                .FirstOrDefaultAsync(m => m.UserMoneyId == id);
            if (userMoneyModel == null)
            {
                return NotFound();
            }

            return View(userMoneyModel);
        }

        // POST: Manageaccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userMoneyModel = await _context.UserMoneyModel.FindAsync(id);
            if (userMoneyModel != null)
            {
                _context.UserMoneyModel.Remove(userMoneyModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserMoneyModelExists(int id)
        {
            return _context.UserMoneyModel.Any(e => e.UserMoneyId == id);
        }
    }
}
