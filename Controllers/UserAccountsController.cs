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
using Microsoft.AspNetCore.Identity;

namespace Authentication.Controllers
{
    public class UserAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserAccountsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: UserAccounts/Index
        public IActionResult Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.UserAccount.Include(c => c.Address).Include(c => c.FirstName).Include(c => c.LastName);
            var singleUser = applicationDbContext.Where(c => c.UserName == userId); 
            return View(singleUser.ToListAsync());
        }

        // GET: UserAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccount
                .Include(u => u.Address)
                .Include(u => u.Wallet)
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
            ViewData["IdentityUserId"] = new SelectList(_context.Set<UserAccount>(), "ID", "ID");
            return View();
        }

        // POST: UserAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _userManager.FindByIdAsync(userId).Result;
                userAccount.UserName = user.Email;
                userAccount.Wallet = new Wallet() { Balance = 0 };
                userAccount.Wallet.Payment = new Payment() { CCNumber = 0 };
                userAccount.Wallet.Transactions = new Transactions() { SentToWallet = false };

                _context.UserAccount.Add(userAccount);
       

                await _context.SaveChangesAsync();
                return RedirectToAction ("UserHomePAge", "UserAccounts");


            }
            ViewData["UserId"] = new SelectList(_context.Set<UserAccount>(), "AddressID", "AddressID", userAccount.AddressID);
            ViewData["UserId"] = new SelectList(_context.Set<Wallet>(), "WalletId", "WalletId", userAccount.WalletId);
            ViewData["UserId"] = new SelectList(_context.Set<Payment>(), "PaymentId", "PaymentId", userAccount.Wallet.PaymentId);
            ViewData["IdentityUserId"] = new SelectList(_context.Set<UserAccount>(), "ID", "ID");
            return View("UserHomePage", userAccount);
        }

        // GET: UserAccounts/Edit/5
        public IActionResult Edit()
        {
            return View();
               
        }

        
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
                .Include(u => u.Address)
                .Include(u => u.Wallet)
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userManager.FindByIdAsync(userId).Result;
            var userAccount = user.Email;
            if (userAccount == null)
            {
                return Create();
            }

            var User = _context.UserAccount
                .Include(u => u.Address)
                .Include(g => g.Group)
                .Include(w => w.Wallet)
                .Include(p => p.Wallet.Payment)
                .Where(x => x.UserName == userAccount).FirstOrDefaultAsync();
            if (User == null)
            {
                return Create();
            }
            
            return View(User);
        }
    }
}
