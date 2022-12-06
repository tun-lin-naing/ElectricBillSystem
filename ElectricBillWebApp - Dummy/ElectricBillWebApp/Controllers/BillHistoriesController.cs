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

namespace ElectricBillWebApp.Controllers
{
    public class BillHistoriesController : Controller
    {
        private readonly EBSDBContext _context;

        public BillHistoriesController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: BillHistories
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.TblBillHistories.Include(t => t.Customer).Include(t => t.User);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: BillHistories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblBillHistories == null)
            {
                return NotFound();
            }

            var tblBillHistory = await _context.TblBillHistories
                .Include(t => t.Customer)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblBillHistory == null)
            {
                return NotFound();
            }

            return View(tblBillHistory);
        }

        // GET: BillHistories/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id");
            return View();
        }

        // POST: BillHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeterNo,Name,Township,CustomerId,ReadDate,DueDate,LastUnit,CurrentUnit,BillPeriod")] ResCreateBill createBill)
        {
            TblTransactionHistory? tblTransaction = _context.TblTransactionHistories
                    .Where(w => w.CustomerId == createBill.CustomerId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault();

            decimal? prepaidAmount = 0;
            decimal? remainAmount = 0;
            decimal? fineAmount = 0;
            int lateMonthCount = 0;

            if (tblTransaction != null)
            {
                prepaidAmount = tblTransaction.PrepaidAmount;
                remainAmount = tblTransaction.RemainAmount;
                fineAmount = tblTransaction.FineAmount;
            }


            CustomerMeter? customerMeter = _context.CustomerMeters
                    .Where(w => w.CustomerId == createBill.CustomerId)
                    .OrderByDescending(o => o.Id)
                    .FirstOrDefault();

            long type = 1;

            if (customerMeter != null)
            {
                type = customerMeter.MeterId;
            }

            decimal cost = 0;
            long usedUnit = createBill.CurrentUnit - createBill.LastUnit;
            if (type == 1)
            {
                //type 1
                if (usedUnit >= 0 && usedUnit <= 30)
                {
                    cost += usedUnit * 35;
                }
                else if (usedUnit >= 31 && usedUnit <= 50)
                {
                    cost += 30 * 35;
                    cost += (usedUnit - 30) * 50;
                }
                else if (usedUnit >= 51 && usedUnit <= 75)
                {
                    cost += 30 * 35;
                    cost += 20 * 50;
                    cost += (usedUnit - 50) * 70;
                }
                else if (usedUnit >= 76 && usedUnit <= 100)
                {
                    cost += 30 * 35;
                    cost += 20 * 50;
                    cost += 25 * 70;
                    cost += (usedUnit - 75) * 90;
                }
                else if (usedUnit >= 101 && usedUnit <= 150)
                {
                    cost += 30 * 35;
                    cost += 20 * 50;
                    cost += 25 * 70;
                    cost += 25 * 90;
                    cost += (usedUnit - 100) * 110;
                }
                else if (usedUnit >= 151 && usedUnit <= 200)
                {
                    cost += 30 * 35;
                    cost += 20 * 50;
                    cost += 25 * 70;
                    cost += 25 * 90;
                    cost += 50 * 110;
                    cost += (usedUnit - 150) * 120;
                }
                else
                {
                    cost += 30 * 35;
                    cost += 20 * 50;
                    cost += 25 * 70;
                    cost += 25 * 90;
                    cost += 50 * 110;
                    cost += 50 * 120;
                    cost += (usedUnit - 200) * 125;
                }
            }
            else
            {
                //Type 2
                if (usedUnit >= 0 && usedUnit <= 500)
                {
                    cost += usedUnit * 125;
                }
                else if (usedUnit >= 501 && usedUnit <= 5000)
                {
                    cost += 500 * 125;
                    cost += (usedUnit - 500) * 135;
                }
                else if (usedUnit >= 5001 && usedUnit <= 10000)
                {
                    cost += 500 * 125;
                    cost += 4500 * 135;
                    cost += (usedUnit - 5000) * 145;
                }
                else if (usedUnit >= 10001 && usedUnit <= 20000)
                {
                    cost += 500 * 125;
                    cost += 4500 * 135;
                    cost += 5000 * 145;
                    cost += (usedUnit - 10000) * 155;
                }
                else if (usedUnit >= 20001 && usedUnit <= 50000)
                {
                    cost += 500 * 125;
                    cost += 4500 * 135;
                    cost += 5000 * 145;
                    cost += 10000 * 155;
                    cost += (usedUnit - 20000) * 165;
                }
                else if (usedUnit >= 50001 && usedUnit <= 100000)
                {
                    cost += 500 * 125;
                    cost += 4500 * 135;
                    cost += 5000 * 145;
                    cost += 10000 * 155;
                    cost += 30000 * 165;
                    cost += (usedUnit - 50000) * 175;
                }
                else
                {
                    cost += 500 * 125;
                    cost += 4500 * 135;
                    cost += 5000 * 145;
                    cost += 10000 * 155;
                    cost += 30000 * 165;
                    cost += 50000 * 175;
                    cost += (usedUnit - 100000) * 165;
                }
            }

            TblBillHistory tblBillHistory = null;
            if (ModelState.IsValid)
            {
                tblBillHistory = new TblBillHistory()
                {
                    CustomerId = createBill.CustomerId,
                    UserId = Convert.ToInt64(HttpContext.Session.GetString("id")),
                    ReadDate = createBill.ReadDate,
                    DueDate = createBill.DueDate,
                    LastUnit = createBill.LastUnit,
                    CurrentUnit = createBill.CurrentUnit,
                    UsedUnit = usedUnit,
                    BillPeriod = createBill.BillPeriod,
                    Currency = "MMK",
                    Amount = cost,
                    PrepaidAmount = prepaidAmount,
                    RemainAmount = remainAmount,
                    FineAmount = fineAmount
                };

                _context.Add(tblBillHistory);
                await _context.SaveChangesAsync();
                return Redirect("../Bill/Bill/");
            }

            return Redirect("../Bill/CreateBill/" + createBill.CustomerId);
        }

        // GET: BillHistories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblBillHistories == null)
            {
                return NotFound();
            }

            var tblBillHistory = await _context.TblBillHistories.FindAsync(id);
            if (tblBillHistory == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", tblBillHistory.CustomerId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id", tblBillHistory.UserId);
            return View(tblBillHistory);
        }

        // POST: BillHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerId,UserId,ReadDate,DueDate,LastUnit,CurrentUnit,UsedUnit,Currency,Amount,BillPeriod,PrepaidAmount,RemainAmount,FineAmount")] TblBillHistory tblBillHistory)
        {
            if (id != tblBillHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblBillHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblBillHistoryExists(tblBillHistory.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Name", tblBillHistory.CustomerId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Name", tblBillHistory.UserId);
            return View(tblBillHistory);
        }

        // GET: BillHistories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblBillHistories == null)
            {
                return NotFound();
            }

            var tblBillHistory = await _context.TblBillHistories
                .Include(t => t.Customer)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblBillHistory == null)
            {
                return NotFound();
            }

            return View(tblBillHistory);
        }

        // POST: BillHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblBillHistories == null)
            {
                return Problem("Entity set 'EBSDBContext.TblBillHistories'  is null.");
            }
            var tblBillHistory = await _context.TblBillHistories.FindAsync(id);
            if (tblBillHistory != null)
            {
                _context.TblBillHistories.Remove(tblBillHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblBillHistoryExists(long id)
        {
            return _context.TblBillHistories.Any(e => e.Id == id);
        }
    }
}
