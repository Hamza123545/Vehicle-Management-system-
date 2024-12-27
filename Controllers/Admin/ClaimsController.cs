using Microsoft.AspNetCore.Mvc;
using VehicleInsurance.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VehicleInsurance.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly myDbContext _context;

        public ClaimsController(myDbContext context)
        {
            _context = context;
        }

        // GET: Claims
        public IActionResult Index()
        {
            var claims = _context.Claims
                                 .Include(c => c.Policy) 
                                 .ToList();
            return View(claims);
        }

        
        public IActionResult Create()
        {
            ViewBag.Policies = _context.Policies.ToList(); 
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            ViewBag.Policies = _context.Policies.ToList();
            return View(claim); 
        }

        
        public IActionResult Edit(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim == null)
            {
                return NotFound();
            }
            ViewBag.Policies = _context.Policies.ToList(); 
            return View(claim);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Claim claim)
        {
            if (id != claim.ClaimID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(claim);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Policies = _context.Policies.ToList();
            return View(claim); 
        }

        public IActionResult Delete(int id)
        {
            var claim = _context.Claims
                                .Include(c => c.Policy)
                                .FirstOrDefault(c => c.ClaimID == id);

            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                _context.Claims.Remove(claim);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
