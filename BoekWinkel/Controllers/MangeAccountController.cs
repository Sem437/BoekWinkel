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
    public class MangeAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangeAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: MangeAccount
        public async Task<IActionResult> Index(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId != loggedInUser)
            {
                return Unauthorized();
            }

            var userDetails = await _context.UserMoneyModel
                .Where(u => u.LinkedUser == userId)
                .ToListAsync();

            return View(userDetails);
        }

        // GET: MangeAccount/Details/5
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

        // GET: MangeAccount/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MangeAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserMoneyId,Money,LinkedUser")] UserMoneyModel userMoneyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userMoneyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userMoneyModel);
        }

        // GET: MangeAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userMoneyModel = await _context.UserMoneyModel.FindAsync(id);
            if (userMoneyModel == null)
            {
                return NotFound();
            }
            return View(userMoneyModel);
        }

        // POST: MangeAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserMoneyId,Money,LinkedUser")] UserMoneyModel userMoneyModel)
        {
            if (id != userMoneyModel.UserMoneyId)
            {
                return NotFound();
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
                return RedirectToAction(nameof(Index));
            }
            return View(userMoneyModel);
        }

        // GET: MangeAccount/Delete/5
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

        // POST: MangeAccount/Delete/5
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
