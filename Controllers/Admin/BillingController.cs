using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleInsurance.Controllers
{
    public class BillingController : Controller
    {
        private readonly myDbContext _context;

        public BillingController(myDbContext context)
        {
            _context = context;
        }

        // GET: Billing
        public async Task<IActionResult> Index()
        {
            var billings = _context.Billings
                                   .Include(b => b.Customer)
                                   .Include(b => b.Policy)
                                   .Include(b => b.Vehicle);
            return View(await billings.ToListAsync());
        }

        // GET: Billing/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            ViewData["PolicyID"] = new SelectList(_context.Policies, "PolicyID", "PolicyNumber");
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VehicleModel");
            return View();
        }

        // POST: Billing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillID,CustomerID,PolicyID,AddressProof,PhoneNumber,BillNumber,VehicleID,VehicleModel,VehicleRate,VehicleBodyNumber,VehicleEngineNumber,BillingDate,Amount")] Billing billing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", billing.CustomerID);
            ViewData["PolicyID"] = new SelectList(_context.Policies, "PolicyID", "PolicyNumber", billing.PolicyID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VehicleModel", billing.VehicleID);
            return View(billing);
        }

        // GET: Billing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                                        .Include(b => b.Customer)
                                        .Include(b => b.Policy)
                                        .Include(b => b.Vehicle)
                                        .FirstOrDefaultAsync(m => m.BillID == id);
            if (billing == null)
            {
                return NotFound();
            }

            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", billing.CustomerID);
            ViewData["PolicyID"] = new SelectList(_context.Policies, "PolicyID", "PolicyNumber", billing.PolicyID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VehicleModel", billing.VehicleID);
            return View(billing);
        }

        // POST: Billing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillID,CustomerID,PolicyID,AddressProof,PhoneNumber,BillNumber,VehicleID,VehicleModel,VehicleRate,VehicleBodyNumber,VehicleEngineNumber,BillingDate,Amount")] Billing billing)
        {
            if (id != billing.BillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingExists(billing.BillID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", billing.CustomerID);
            ViewData["PolicyID"] = new SelectList(_context.Policies, "PolicyID", "PolicyNumber", billing.PolicyID);
            ViewData["VehicleID"] = new SelectList(_context.Vehicles, "VehicleID", "VehicleModel", billing.VehicleID);
            return View(billing);
        }

        // GET: Billing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings
                                        .Include(b => b.Customer)
                                        .Include(b => b.Policy)
                                        .Include(b => b.Vehicle)
                                        .FirstOrDefaultAsync(m => m.BillID == id);
            if (billing == null)
            {
                return NotFound();
            }

            return View(billing);
        }

        // POST: Billing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billing = await _context.Billings.FindAsync(id);
            _context.Billings.Remove(billing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillingExists(int id)
        {
            return _context.Billings.Any(e => e.BillID == id);
        }
    }
}
