using Microsoft.AspNetCore.Mvc;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class AdminController : Controller
    {
        private myDbContext _db;
        public AdminController(myDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("name") != null)
            {
                ViewBag.name = HttpContext.Session.GetString("name");
            }

            return View();
        }
        
    }
}
