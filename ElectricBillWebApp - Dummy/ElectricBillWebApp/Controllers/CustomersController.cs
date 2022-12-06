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
    public class CustomersController : Controller
    {
        private readonly EBSDBContext _context;

        public CustomersController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.TblCustomers.Include(t => t.CreatedByNavigation);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .Include(t => t.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Address,Barcode,MeterNo,Township,IsActive")] TblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                long userID = Convert.ToInt64(HttpContext.Session.GetString("id"));
                tblCustomer.CreatedDate = DateTime.Now;
                tblCustomer.CreatedBy = userID;
                tblCustomer.IsActive = true;

                _context.Add(tblCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblCustomer.CreatedBy);
            return View(tblCustomer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblCustomer.CreatedBy);
            return View(tblCustomer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Email,Password,Address,Barcode,MeterNo,Township,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,IsActive,DeletedBy,DeletedDate")] TblCustomer tblCustomer)
        {
            if (id != tblCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCustomerExists(tblCustomer.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblCustomer.CreatedBy);
            return View(tblCustomer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .Include(t => t.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblCustomers == null)
            {
                return Problem("Entity set 'EBSDBContext.TblCustomers'  is null.");
            }
            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer != null)
            {
                _context.TblCustomers.Remove(tblCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCustomerExists(long id)
        {
          return _context.TblCustomers.Any(e => e.Id == id);
        }
    }
}
