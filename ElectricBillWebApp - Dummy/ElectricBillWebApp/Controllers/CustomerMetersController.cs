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
    public class CustomerMetersController : Controller
    {
        private readonly EBSDBContext _context;

        public CustomerMetersController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: CustomerMeters
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.CustomerMeters.Include(c => c.Customer).Include(c => c.Meter).Include(c => c.User);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: CustomerMeters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.CustomerMeters == null)
            {
                return NotFound();
            }

            var customerMeter = await _context.CustomerMeters
                .Include(c => c.Customer)
                .Include(c => c.Meter)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerMeter == null)
            {
                return NotFound();
            }

            return View(customerMeter);
        }

        // GET: CustomerMeters/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Name");
            ViewData["MeterId"] = new SelectList(_context.TblMeterTypes, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Name");
            return View();
        }

        // POST: CustomerMeters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,MeterId,Status")] CustomerMeter customerMeter)
        {
            if (customerMeter.MeterId > 0 && customerMeter.CustomerId >0)
            {
                customerMeter.UserId = Convert.ToInt64(HttpContext.Session.GetString("id"));
                _context.Add(customerMeter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Name", "Id", customerMeter.CustomerId);
            ViewData["MeterId"] = new SelectList(_context.TblMeterTypes, "Id", "Name", customerMeter.MeterId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Name", customerMeter.UserId);
            return View(customerMeter);
        }

        // GET: CustomerMeters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.CustomerMeters == null)
            {
                return NotFound();
            }

            var customerMeter = await _context.CustomerMeters.FindAsync(id);
            if (customerMeter == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Id", customerMeter.CustomerId);
            ViewData["MeterId"] = new SelectList(_context.TblMeterTypes, "Id", "Id", customerMeter.MeterId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id", customerMeter.UserId);
            return View(customerMeter);
        }

        // POST: CustomerMeters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CustomerId,UserId,MeterId,Status")] CustomerMeter customerMeter)
        {
            if (id != customerMeter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerMeter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerMeterExists(customerMeter.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.TblCustomers, "Id", "Name", customerMeter.CustomerId);
            ViewData["MeterId"] = new SelectList(_context.TblMeterTypes, "Name", "Id", customerMeter.MeterId);
            ViewData["UserId"] = new SelectList(_context.TblUsers, "Id", "Id", customerMeter.UserId);
            return View(customerMeter);
        }

        // GET: CustomerMeters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.CustomerMeters == null)
            {
                return NotFound();
            }

            var customerMeter = await _context.CustomerMeters
                .Include(c => c.Customer)
                .Include(c => c.Meter)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerMeter == null)
            {
                return NotFound();
            }

            return View(customerMeter);
        }

        // POST: CustomerMeters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.CustomerMeters == null)
            {
                return Problem("Entity set 'EBSDBContext.CustomerMeters'  is null.");
            }
            var customerMeter = await _context.CustomerMeters.FindAsync(id);
            if (customerMeter != null)
            {
                _context.CustomerMeters.Remove(customerMeter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMeterExists(long id)
        {
            return _context.CustomerMeters.Any(e => e.Id == id);
        }
    }
}
