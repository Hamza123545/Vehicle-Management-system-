using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class EstimateController : Controller
    {
        private readonly myDbContext _db;

        public EstimateController(myDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            var estimates = _db.Estimates.Include(e => e.Customer).ToList();
            return View(estimates);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _db.Customers.ToList(); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Estimate estimate)
        {
           
                _db.Estimates.Add(estimate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            ViewBag.Customers = _db.Customers.ToList();
            return View(estimate);
        }
        public IActionResult Details(int id)
        {
            var estimate = _db.Estimates
                .Include(e => e.Customer)
                .FirstOrDefault(e => e.EstimateId == id);

            if (estimate == null)
                return NotFound();

            return View(estimate);
        }

        public IActionResult Edit(int id)
        {
            var estimate = _db.Estimates.Find(id);
            if (estimate == null)
            {
                return NotFound();
            }
            ViewBag.Customers = _db.Customers.ToList();
            return View(estimate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Estimate estimate)
        {

                _db.Estimates.Update(estimate);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
     
        public IActionResult Delete(int id)
        {
            var estimate = _db.Estimates.Find(id);
            _db.Estimates.Remove(estimate);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
