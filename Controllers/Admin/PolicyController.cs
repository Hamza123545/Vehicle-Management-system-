using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;
using System.Linq;

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
            // Check for policies that need to be deactivated based on due date
            var policies = _db.Policies
                .Include("Customer")
                .Include("Vehicle")
                .ToList();

            // Automatically deactivate expired policies
            foreach (var policy in policies)
            {
                DeactivatePolicy(policy);  // This method will deactivate if the policy is expired
            }

            _db.SaveChanges();
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

        // Manual Activation of a Policy
        public IActionResult Activate(int id)
        {
            var policy = _db.Policies.Find(id);
            if (policy != null)
            {
                policy.IsActive = true;  // Set the policy status to active
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Manual Deactivation of a Policy
        public IActionResult Deactivate(int id)
        {
            var policy = _db.Policies.Find(id);
            if (policy != null)
            {
                policy.IsActive = false;  // Set the policy status to inactive
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Automatically deactivate expired policies
        private void DeactivatePolicy(Policy policy)
        {
            var policyDueDate = policy.PolicyDate.AddMonths(policy.PolicyDuration);
            if (DateTime.Now > policyDueDate && policy.IsActive)
            {
                policy.IsActive = false;  // Deactivate the policy if expired
            }
        }
    }
}
