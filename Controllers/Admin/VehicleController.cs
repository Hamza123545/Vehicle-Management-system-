using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class VehicleController : Controller
    {
        private readonly myDbContext _db;

        public VehicleController(myDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            var vehicles = _db.Vehicles.ToList();
            return View(vehicles); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            _db.Vehicles.Add(vehicle);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            var vehicle = _db.Vehicles.FirstOrDefault(v => v.VehicleID == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
        public IActionResult Edit(int id)
        {
            var vehicle = _db.Vehicles.Find(id);
            return View(vehicle);
        }

        [HttpPost]
        public IActionResult Edit(int id, Vehicle vehicle)
        {
            _db.Vehicles.Update(vehicle);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var vehicle = _db.Vehicles.Find(id);
            _db.Vehicles.Remove(vehicle);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
