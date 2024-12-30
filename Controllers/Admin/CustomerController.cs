using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class CustomerController : Controller
    {
        private myDbContext _db;
        public CustomerController(myDbContext context)
        {
            _db = context;
        }
        public IActionResult Show()
        {
            var customers = _db.Customers.ToList();
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]

        public async Task<IActionResult> Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            return RedirectToAction("show");

        }
        public IActionResult Details(int id)
        {
            var customer = _db.Customers
                .Include(c => c.Policies)
                .FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        public IActionResult Edit(int id)
        {
            var cus = _db.Customers.Find(id);
            return View(cus);
        }
        //[HttpPost]
        //public IActionResult Edit(int id, Customer customer)
        //{
        //    _db.Customers.Update(customer);
        //    _db.SaveChanges();
        //    return RedirectToAction("Show");
        //}
        [HttpPost]
        public IActionResult Edit(Customer model, IFormFile? AddressProof)
        {
            try
            {
                if (AddressProof != null)
                {
                    // Save the uploaded file and update the AddressProof property
                    string filePath = Path.Combine("Uploads", AddressProof.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        AddressProof.CopyTo(stream);
                    }
                    model.AddressProof = filePath;
                }
                else
                {
                    // Use the existing AddressProof value
                    model.AddressProof = Request.Form["ExistingAddressProof"];
                }

                _db.Customers.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Show");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the record: " + ex.Message);
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            var cus = _db.Customers.Find(id);
            _db.Customers.Remove(cus);
            _db.SaveChanges();
            return RedirectToAction("show");
        }

    }
}
