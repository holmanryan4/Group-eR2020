using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authentication.Data;
using Authentication.Models;

namespace Authentication.Controllers
{
    public class WalletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wallets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Wallet.Include(w => w.memory).Include(w => w.payment).Include(w => w.transactions);
            return View(await applicationDbContext.ToListAsync());
        }
       
        // GET: Wallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet
                .Include(w => w.memory)
                .Include(w => w.payment)
                .Include(w => w.transactions)
                .FirstOrDefaultAsync(m => m.WalletId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult Create()
        {
            ViewData["MemoryId"] = new SelectList(_context.Memory, "MemoryId", "MemoryId");
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId");
            ViewData["TransactionsId"] = new SelectList(_context.Transactions, "TransactionId", "TransactionId");
            return View();
        }

        // POST: Wallets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WalletId,Balance,PaymentId,MemoryId,TransactionsId")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemoryId"] = new SelectList(_context.Memory, "MemoryId", "MemoryId", wallet.MemoryId);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", wallet.PaymentId);
            ViewData["TransactionsId"] = new SelectList(_context.Transactions, "TransactionId", "TransactionId", wallet.TransactionsId);
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            ViewData["MemoryId"] = new SelectList(_context.Memory, "MemoryId", "MemoryId", wallet.MemoryId);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", wallet.PaymentId);
            ViewData["TransactionsId"] = new SelectList(_context.Transactions, "TransactionId", "TransactionId", wallet.TransactionsId);
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WalletId,Balance,PaymentId,MemoryId,TransactionsId")] Wallet wallet)
        {
            if (id != wallet.WalletId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.WalletId))
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
            ViewData["MemoryId"] = new SelectList(_context.Memory, "MemoryId", "MemoryId", wallet.MemoryId);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", wallet.PaymentId);
            ViewData["TransactionsId"] = new SelectList(_context.Transactions, "TransactionId", "TransactionId", wallet.TransactionsId);
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallet
                .Include(w => w.memory)
                .Include(w => w.payment)
                .Include(w => w.transactions)
                .FirstOrDefaultAsync(m => m.WalletId == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wallet = await _context.Wallet.FindAsync(id);
            _context.Wallet.Remove(wallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(int id)
        {
            return _context.Wallet.Any(e => e.WalletId == id);
        }
    }
}
