using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleInsurance.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VehicleInsurance.Controllers
{
    public class BillingController : Controller
    {
        private readonly myDbContext _db;

        public BillingController(myDbContext context)
        {
            _db = context;
        }

        // Display a list of all billings
        public IActionResult Index()
        {
            var billings = _db.Billings
                .Include(b => b.Policy)
                .ThenInclude(p => p.Customer)
                .Include(b => b.Policy)
                .ThenInclude(p => p.Vehicle)
                .ToList();
            return View(billings);
        }

        // Create Billing (GET)
        public IActionResult Create()
        {
            ViewBag.Policies = _db.Policies.ToList();
            return View();
        }

        // Create Billing (POST)
        [HttpPost]
        public IActionResult Create(Billing billing)
        {
            if (ModelState.IsValid)
            {
                _db.Billings.Add(billing);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Policies = _db.Policies.ToList();
            return View(billing);
        }

        // Edit Billing (GET)
        public IActionResult Edit(int id)
        {
            var billing = _db.Billings.Find(id);
            if (billing == null)
                return NotFound();

            ViewBag.Policies = _db.Policies.ToList();
            return View(billing);
        }

        // Edit Billing (POST)
        [HttpPost]
        public IActionResult Edit(int id, Billing billing)
        {
            if (ModelState.IsValid)
            {
                _db.Billings.Update(billing);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Policies = _db.Policies.ToList();
            return View(billing);
        }

        // Delete Billing (GET)
        public IActionResult Delete(int id)
        {
            var billing = _db.Billings.Find(id);
            if (billing == null)
                return NotFound();

            return View(billing);
        }

        // Delete Billing (POST)
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var billing = _db.Billings.Find(id);
            _db.Billings.Remove(billing);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
