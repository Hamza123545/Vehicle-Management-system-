using Microsoft.AspNetCore.Mvc;
using VehicleInsurance.Models;

namespace VehicleInsurance.Controllers.Admin
{
    public class ExpenseController : Controller
    {
        private myDbContext _db;
        public ExpenseController(myDbContext context)
        {
            _db = context;
        }

        // GET: Expense
        public IActionResult Index()
        {
            var expenses = _db.Expenses.ToList();
            return View(expenses);
        }

        // GET: Expense/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        [HttpPost]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expense/Edit/5
        public IActionResult Edit(int id)
        {
            var expense = _db.Expenses.Find(id);
            if (expense == null)
                return NotFound();

            return View(expense);
        }

        // POST: Expense/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Expense expense)
        {
            if (id != expense.ExpenseID)
                return NotFound();

            if (ModelState.IsValid)
            {
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expense/Details/5
        public IActionResult Details(int id)
        {
            var expense = _db.Expenses.Find(id);
            if (expense == null)
                return NotFound();

            return View(expense);
        }


        public IActionResult Delete(int id)
        {
            var expense = _db.Expenses.Find(id);
            _db.Expenses.Remove(expense);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
