using ElectricBillWebApp.Common;
using ElectricBillWebApp.Data;
using ElectricBillWebApp.Models;
using ElectricBillWebApp.Models.Bill;
using ElectricBillWebApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ElectricBillWebApp.CommonConstant;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectricBillWebApp.Models.Pay;

namespace ElectricBillWebApp.Controllers
{
    public class BillController : Controller
    {
        private readonly ILogger<BillController> _logger;

        private readonly EBSDBContext _context;

        public BillController(ILogger<BillController> logger, EBSDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Bill(string meterNo = null, string township = null)
        {
            ViewBag.MeterNo = meterNo;
            ViewBag.TownShip = township;

            var eBSDBContext = _context.TblCustomers
                .WhereIf(!string.IsNullOrEmpty(meterNo), w => w.MeterNo == meterNo)
                .WhereIf(!string.IsNullOrEmpty(township), w => w.Township == meterNo);
            return View(await eBSDBContext.ToListAsync());
        }

        public async Task<IActionResult> ShowPayBill(string meterNo = null, string township = null)
        {
            ViewBag.MeterNo = meterNo;
            ViewBag.TownShip = township;

            var eBSDBContext = _context.TblCustomers
                .WhereIf(!string.IsNullOrEmpty(meterNo), w => w.MeterNo == meterNo)
                .WhereIf(!string.IsNullOrEmpty(township), w => w.Township == meterNo)
                .Include(i => i.TblBillHistories);
            return View(await eBSDBContext.ToListAsync());
        }

        public IActionResult CreateBill(long id)
        {
            TblCustomer? customer = _context.TblCustomers
                .Where(w => w.Id == id)
                .FirstOrDefault();

            ResCreateBill createBill = null;
            if (customer != null)
            {
                long lastUnit = 0;
                TblBillHistory? bill = _context.TblBillHistories
                    .Where(w => w.Id == customer.Id)
                    .OrderByDescending(o => o.DueDate)
                    .FirstOrDefault();

                if (bill != null)
                {
                    lastUnit = bill.CurrentUnit;
                }
                createBill = new ResCreateBill()
                {
                    CustomerId = customer.Id,
                    Name = customer.Name,
                    MeterNo = customer.MeterNo,
                    Township = customer.Township,
                    LastUnit = lastUnit,
                    ReadDate = DateTime.Now,
                    DueDate = DateTime.Now,
                    BillPeriod = DateTime.Now.ToString("MMM-yyyy")
                };
            }
            return View(createBill);
        }

        public IActionResult PayBill(long id)
        {
            TblCustomer? customer = _context.TblCustomers
                .Where(w => w.Id == id)
                .FirstOrDefault();

            ResPayBill createBill = new ResPayBill();
            createBill.PayMethod = new List<PayMethod>();
            if (customer != null)
            {
                List<TblBillHistory> bill1 = _context.TblBillHistories
                .Where(w => w.CustomerId == customer.Id)
                .OrderByDescending(o => o.Id)
                .ToList();

                List<TblBillHistory> bill2 = _context.TblBillHistories
                .Where(w => w.CustomerId == customer.Id)
                .OrderBy(o => o.Id)
                .ToList();

                TblBillHistory? bill = _context.TblBillHistories
                .Where(w => w.CustomerId == customer.Id)
                .OrderBy(o => o.Id)
                .Last();

                if (bill != null)
                {
                    createBill.CustomerId = customer.Id;
                    createBill.BillId = bill.Id;
                    createBill.Name = customer.Name;
                    createBill.MeterNo = customer.MeterNo;
                    createBill.Township = customer.Township;
                    createBill.LastUnit = bill.LastUnit ?? 0;
                    createBill.CurrentUnit = bill.CurrentUnit;
                    createBill.ReadDate = DateTime.Now;
                    createBill.DueDate = DateTime.Now;
                    createBill.BillPeriod = bill.BillPeriod;
                    createBill.Amount = bill.Amount;
                    createBill.PrepaidAmount = bill.PrepaidAmount ?? 0;
                    createBill.RemainAmount = bill.RemainAmount ?? 0;
                    createBill.FineAmount = bill.FineAmount ?? 0;
                    createBill.PayMethod = Constants.PayMethod;
                    createBill.TotalAmount = (createBill.PrepaidAmount + createBill.Amount) - (createBill.RemainAmount + createBill.FineAmount);
                }
                else
                {
                    return View("ShowPayBill");
                }

            }
            return View(createBill);
        }
    }
}