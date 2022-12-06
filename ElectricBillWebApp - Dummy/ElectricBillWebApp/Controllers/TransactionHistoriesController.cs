using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectricBillWebApp.Data;
using ElectricBillWebApp.Models.Entities;

namespace ElectricBillWebApp.Controllers
{
    public class TransactionHistoriesController : Controller
    {
        private readonly EBSDBContext _context;

        public TransactionHistoriesController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: TransactionHistories
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.TblTransactionHistories.Include(t => t.Bill).Include(t => t.Customer).Include(t => t.Pay);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: TransactionHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblTransactionHistories == null)
            {
                return NotFound();
            }

            var tblTransactionHistory = await _context.TblTransactionHistories
                .Include(t => t.Bill)
                .Include(t => t.Customer)
                .Include(t => t.Pay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblTransactionHistory == null)
            {
                return NotFound();
            }

            return View(tblTransactionHistory);
        }

        // GET: TransactionHistories/Create
        public IActionResult Create()
        {
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id");
            ViewData["PayId"] = new SelectList(_context.TblPayHistories, "Id", "Id");
            return View();
        }

        // POST: TransactionHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,BillId,PayId,MeterNo,PrepaidAmount,RemainAmount,FineAmount,LateMonthCount,BillPeriod,Status")] TblTransactionHistory tblTransactionHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblTransactionHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id", tblTransactionHistory.BillId);
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblTransactionHistory.CustomerId);
            ViewData["PayId"] = new SelectList(_context.TblPayHistories, "Id", "Id", tblTransactionHistory.PayId);
            return View(tblTransactionHistory);
        }

        // GET: TransactionHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblTransactionHistories == null)
            {
                return NotFound();
            }

            var tblTransactionHistory = await _context.TblTransactionHistories.FindAsync(id);
            if (tblTransactionHistory == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id", tblTransactionHistory.BillId);
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblTransactionHistory.CustomerId);
            ViewData["PayId"] = new SelectList(_context.TblPayHistories, "Id", "Id", tblTransactionHistory.PayId);
            return View(tblTransactionHistory);
        }

        // POST: TransactionHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerId,BillId,PayId,MeterNo,PrepaidAmount,RemainAmount,FineAmount,LateMonthCount,BillPeriod,Status")] TblTransactionHistory tblTransactionHistory)
        {
            if (id != tblTransactionHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTransactionHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTransactionHistoryExists(tblTransactionHistory.Id))
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
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id", tblTransactionHistory.BillId);
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblTransactionHistory.CustomerId);
            ViewData["PayId"] = new SelectList(_context.TblPayHistories, "Id", "Id", tblTransactionHistory.PayId);
            return View(tblTransactionHistory);
        }

        // GET: TransactionHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblTransactionHistories == null)
            {
                return NotFound();
            }

            var tblTransactionHistory = await _context.TblTransactionHistories
                .Include(t => t.Bill)
                .Include(t => t.Customer)
                .Include(t => t.Pay)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblTransactionHistory == null)
            {
                return NotFound();
            }

            return View(tblTransactionHistory);
        }

        // POST: TransactionHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblTransactionHistories == null)
            {
                return Problem("Entity set 'EBSDBContext.TblTransactionHistories'  is null.");
            }
            var tblTransactionHistory = await _context.TblTransactionHistories.FindAsync(id);
            if (tblTransactionHistory != null)
            {
                _context.TblTransactionHistories.Remove(tblTransactionHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTransactionHistoryExists(long id)
        {
          return _context.TblTransactionHistories.Any(e => e.Id == id);
        }
    }
}
