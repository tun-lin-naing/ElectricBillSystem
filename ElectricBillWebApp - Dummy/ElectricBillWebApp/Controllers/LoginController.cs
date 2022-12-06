using ElectricBillWebApp.Interface;
using ElectricBillWebApp.Models.User;
using ElectricBillWebApp.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ElectricBillWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly Logger _log;
        private readonly ILoginRepository _loginRepository;
        public LoginController(Logger log, ILoginRepository loginRepository)
        {
            _log = log;
            _loginRepository = loginRepository;
        }
        public IActionResult Index()
        {
            string? name = HttpContext.Session.GetString("name");
            string? email = HttpContext.Session.GetString("email");
            long? id = Convert.ToInt64(HttpContext.Session.GetString("id"));

            if (id > 0)
            {
                var route_values = new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Home",
                });

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                ReqUserLogin reqUserLogin = new()
                {
                    Email = email,
                    Password = password
                };

                ResUserLogin? userResult = _loginRepository.Login(reqUserLogin)!;
                if (userResult != null && userResult.Name != null)
                {
                    HttpContext.Session.SetString("name", userResult.Name);
                    HttpContext.Session.SetString("email", userResult.Email);
                    HttpContext.Session.SetString("id", userResult.Id.ToString());

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                _log.Error("Login Controller - Login => ", ex);
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
