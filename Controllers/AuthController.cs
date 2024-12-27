using Microsoft.AspNetCore.Mvc;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers
{
    public class AuthController : Controller
    {
        private myDbContext _db;
        public AuthController(myDbContext context)
        {
            _db = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string uemail, string upass)
        {
            var row = _db.Customers.FirstOrDefault(u => u.Email == uemail);
            if (row != null && row.Password == upass)
            {
                HttpContext.Session.SetString("name", row.CustomerName);
                if (row.Role == "admin")
                {
                    return RedirectToAction("index", "admin");
                }
                else if (row.Role == "customer")
                {
                    return RedirectToAction("index");
                }
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                HttpContext.Session.Remove("name");
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
