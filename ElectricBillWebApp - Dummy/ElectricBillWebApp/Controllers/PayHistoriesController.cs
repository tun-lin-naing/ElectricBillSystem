using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectricBillWebApp.Data;
using ElectricBillWebApp.Models.Entities;
using ElectricBillWebApp.Models.Bill;
using ElectricBillWebApp.CommonConstant;

namespace ElectricBillWebApp.Controllers
{
    public class PayHistoriesController : Controller
    {
        private readonly EBSDBContext _context;

        public PayHistoriesController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: PayHistories
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.TblPayHistories.Include(t => t.Bill).Include(t => t.Customer).Include(t => t.User);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: PayHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblPayHistories == null)
            {
                return NotFound();
            }

            var tblPayHistory = await _context.TblPayHistories
                .Include(t => t.Bill)
                .Include(t => t.Customer)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPayHistory == null)
            {
                return NotFound();
            }

            return View(tblPayHistory);
        }

        // GET: PayHistories/Create
        public IActionResult Create()
        {
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id");
            return View();
        }

        // POST: PayHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeterNo,CustomerId,BillId,Amount,PayMethodString")] ResPayBill payBill)
        {
            TblPayHistory tblPayHistory = new TblPayHistory()
            {
                CustomerId = payBill.CustomerId,
                BillId = payBill.BillId,
                UserId = Convert.ToInt64(HttpContext.Session.GetString("id")),
                PayAmount = payBill.Amount,
                PayDate = DateTime.Now,
                Method = Constants.PayMethod.Where(w => w.PayMethodName == payBill.PayMethodString).Single().Id,
                Code = Guid.NewGuid().ToString(),
                CreatedBy = Convert.ToInt64(HttpContext.Session.GetString("id")),
                CreatedDate = DateTime.Now,

            };

            if (payBill.Amount > 0)
            {
                var transaction = _context.Database.BeginTransaction();

                _context.Add(tblPayHistory);
                await _context.SaveChangesAsync();

                TblCustomer tblCustomer = _context.TblCustomers.Where(w => w.Id == payBill.CustomerId).First();

                TblBillHistory tblBillHistory = _context.TblBillHistories.Where(w => w.Id == payBill.BillId)
                    .Where(w => w.CustomerId == payBill.CustomerId)
                    .OrderByDescending(o => o.Id)
                    .First();

                TblTransactionHistory? tblTransaction = _context.TblTransactionHistories
                    .Where(w => w.CustomerId == payBill.CustomerId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault();

                decimal? prepaidAmount = tblBillHistory.PrepaidAmount;
                decimal? remainAmount = tblBillHistory.RemainAmount;
                decimal? fineAmount = tblBillHistory.FineAmount;
                int lateMonthCount = 0;
                byte status = 0;

                decimal actualAmount = payBill.Amount;
                decimal costAmount = tblBillHistory.Amount;

                if (prepaidAmount > 0)
                {
                    actualAmount += (prepaidAmount ?? 0);
                }

                if (remainAmount > 0)
                {
                    costAmount += remainAmount ?? 0;
                }

                if (fineAmount > 0)
                {
                    costAmount += fineAmount ?? 0;
                }

                if (actualAmount > costAmount)
                {
                    prepaidAmount = actualAmount - costAmount;
                }
                else if (actualAmount == costAmount)
                {
                    prepaidAmount = 0;
                    fineAmount = 0;
                    remainAmount = 0;
                    status = 1;
                }
                else
                {
                    prepaidAmount = 0;

                    if(tblTransaction != null)
                    {
                        lateMonthCount = (int)tblTransaction.LateMonthCount + 1;
                    }
                    else
                    {
                        lateMonthCount = 1;
                    }

                    fineAmount += lateMonthCount * 1000; // give fine 1000 MMK * Late count month
                    remainAmount = costAmount - actualAmount;
                }

                TblTransactionHistory tblTransactionHistory = new TblTransactionHistory()
                {
                    CustomerId = payBill.CustomerId,
                    BillId = payBill.BillId,
                    PayId = tblPayHistory.Id,
                    MeterNo = tblCustomer.MeterNo,
                    PrepaidAmount = prepaidAmount,
                    RemainAmount = remainAmount,
                    FineAmount = fineAmount,
                    LateMonthCount = (byte)lateMonthCount,
                    BillPeriod = tblBillHistory.BillPeriod,
                    Status = status
                };

                _context.Add(tblTransactionHistory);
                await _context.SaveChangesAsync();

                transaction.Commit();
                return Redirect("../Bill/ShowPayBill");
            }

            return Redirect("../Bill/PayBill/" + payBill.CustomerId);
        }

        // GET: PayHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblPayHistories == null)
            {
                return NotFound();
            }

            var tblPayHistory = await _context.TblPayHistories.FindAsync(id);
            if (tblPayHistory == null)
            {
                return NotFound();
            }
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id", tblPayHistory.BillId);
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblPayHistory.CustomerId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id", tblPayHistory.UserId);
            return View(tblPayHistory);
        }

        // POST: PayHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerId,UserId,BillId,PayAmount,PayDate,Method,Code,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] TblPayHistory tblPayHistory)
        {
            if (id != tblPayHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPayHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPayHistoryExists(tblPayHistory.Id))
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
            ViewData["BillId"] = new SelectList(_context.TblBillHistories, "Id", "Id", tblPayHistory.BillId);
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblPayHistory.CustomerId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id", tblPayHistory.UserId);
            return View(tblPayHistory);
        }

        // GET: PayHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblPayHistories == null)
            {
                return NotFound();
            }

            var tblPayHistory = await _context.TblPayHistories
                .Include(t => t.Bill)
                .Include(t => t.Customer)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblPayHistory == null)
            {
                return NotFound();
            }

            return View(tblPayHistory);
        }

        // POST: PayHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblPayHistories == null)
            {
                return Problem("Entity set 'EBSDBContext.TblPayHistories'  is null.");
            }
            var tblPayHistory = await _context.TblPayHistories.FindAsync(id);
            if (tblPayHistory != null)
            {
                _context.TblPayHistories.Remove(tblPayHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblPayHistoryExists(long id)
        {
            return _context.TblPayHistories.Any(e => e.Id == id);
        }
    }
}
