using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class PolicyController : Controller
    {
        private readonly myDbContext _db;

        public PolicyController(myDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var policies = _db.Policies
                .Include("Customer")
                .Include("Vehicle")
                .ToList();
            return View(policies);
        }

        public IActionResult Details(int id)
        {
            var policy = _db.Policies
                .Include(e => e.Customer)
                .Include(e => e.Vehicle)
                .FirstOrDefault(e => e.PolicyID == id);

            if (policy == null)
                return NotFound();

            return View(policy);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Vehicles = _db.Vehicles.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult Create(Policy policy)
        {
          
                _db.Policies.Add(policy);
                _db.SaveChanges();
                return RedirectToAction("Index");
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Vehicles = _db.Vehicles.ToList();
            return View(policy);

        }

        public IActionResult Edit(int id)
        {
            var policy = _db.Policies.Find(id);
            if (policy == null)
            {
                return NotFound();
            }
            ViewBag.Customers = _db.Customers.ToList();
            ViewBag.Vehicles = _db.Vehicles.ToList();
            return View(policy);
        }

        [HttpPost]
        public IActionResult Edit(int id, Policy policy)
        {

            _db.Policies.Update(policy);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var policy = _db.Policies.Find(id);
            _db.Policies.Remove(policy);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

