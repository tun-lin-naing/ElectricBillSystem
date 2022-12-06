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
    public class MeterTypesController : Controller
    {
        private readonly EBSDBContext _context;

        public MeterTypesController(EBSDBContext context)
        {
            _context = context;
        }

        // GET: MeterTypes
        public async Task<IActionResult> Index()
        {
            var eBSDBContext = _context.TblMeterTypes.Include(t => t.CreatedByNavigation);
            return View(await eBSDBContext.ToListAsync());
        }

        // GET: MeterTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TblMeterTypes == null)
            {
                return NotFound();
            }

            var tblMeterType = await _context.TblMeterTypes
                .Include(t => t.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblMeterType == null)
            {
                return NotFound();
            }

            return View(tblMeterType);
        }

        // GET: MeterTypes/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id");
            return View();
        }

        // POST: MeterTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,CreatedBy,CreatedDate")] TblMeterType tblMeterType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblMeterType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblMeterType.CreatedBy);
            return View(tblMeterType);
        }

        // GET: MeterTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TblMeterTypes == null)
            {
                return NotFound();
            }

            var tblMeterType = await _context.TblMeterTypes.FindAsync(id);
            if (tblMeterType == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblMeterType.CreatedBy);
            return View(tblMeterType);
        }

        // POST: MeterTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,IsActive,CreatedBy,CreatedDate")] TblMeterType tblMeterType)
        {
            if (id != tblMeterType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMeterType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMeterTypeExists(tblMeterType.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.TblUsers, "Id", "Id", tblMeterType.CreatedBy);
            return View(tblMeterType);
        }

        // GET: MeterTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TblMeterTypes == null)
            {
                return NotFound();
            }

            var tblMeterType = await _context.TblMeterTypes
                .Include(t => t.CreatedByNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblMeterType == null)
            {
                return NotFound();
            }

            return View(tblMeterType);
        }

        // POST: MeterTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TblMeterTypes == null)
            {
                return Problem("Entity set 'EBSDBContext.TblMeterTypes'  is null.");
            }
            var tblMeterType = await _context.TblMeterTypes.FindAsync(id);
            if (tblMeterType != null)
            {
                _context.TblMeterTypes.Remove(tblMeterType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMeterTypeExists(long id)
        {
          return _context.TblMeterTypes.Any(e => e.Id == id);
        }
    }
}
