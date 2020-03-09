using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authentication.Data;
using Authentication.Models;
using System.Security.Claims;

namespace Authentication.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: UserAccounts/Index
        public async Task<IActionResult> Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.UserAccount.Include(c => c.address).Include(c => c.FirstName).Include(c => c.LastName);
            var singleUser = applicationDbContext.Where(c => c.UserName == userId); //Nick Helped, everythings still on fire tho.
            return View(await singleUser.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.address)
                .Include(u => u.wallet)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        public IActionResult Create()
        {
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressId", "AddressId");
            ViewData["WalletId"] = new SelectList(_context.Set<Wallet>(), "WalletId", "WalletId");
            return View();
        }

        // POST: UserAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,PhoneNumber,AddressID,WalletId")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Set<Address>(), "AddressId", "AddressId", userAccount.AddressID);
            ViewData["WalletId"] = new SelectList(_context.Set<Wallet>(), "WalletId", "WalletId", userAccount.WalletId);
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public IActionResult Edit()
        {
            return View();
               
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,AddressID,WalletId")] UserAccount userAccount)
        {
            if (id != userAccount.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountExists(userAccount.UserId))
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
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressId", "AddressId", userAccount.AddressID);
            ViewData["WalletId"] = new SelectList(_context.Set<Wallet>(), "WalletId", "WalletId", userAccount.WalletId);
            return View(userAccount);
        }

        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.address)
                .Include(u => u.wallet)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            _context.UserAccount.Remove(userAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.UserId == id);
        }
        public IActionResult UserHomePage()
        {
            return View();
        }
    }
}
