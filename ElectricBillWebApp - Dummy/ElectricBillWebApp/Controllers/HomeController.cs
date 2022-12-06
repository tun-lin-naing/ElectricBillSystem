using ElectricBillWebApp.CustomFilter;
using ElectricBillWebApp.Data;
using ElectricBillWebApp.Models;
using ElectricBillWebApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace ElectricBillWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EBSDBContext _context;

        public HomeController(ILogger<HomeController> logger, EBSDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        

        [RolePermissionFilterAttribute]
        public IActionResult Index()
        {
            ViewBag.Customer = _context.TblCustomers.Count();
            ViewBag.User = _context.TblUsers.Count();
            ViewBag.Bill = _context.TblBillHistories.Count();
            ViewBag.Pay = _context.TblPayHistories.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}