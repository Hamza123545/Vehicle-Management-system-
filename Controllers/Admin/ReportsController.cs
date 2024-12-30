using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using VehicleInsurance.Models;
using VehicleInsurance.Models.ReportModels;

namespace VehicleInsurance.Controllers
{
    public class ReportsController : Controller
    {
        private readonly myDbContext _db;

        public ReportsController(myDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new ReportsViewModel
            {
                MonthlySales = GetMonthlySalesReport(),
                VehicleAnalysis = GetVehicleAnalysisReport(),
                ClaimReports = GetClaimReports(),
                PoliciesDue = GetPoliciesDueReport(),
                LapsedPolicies = GetLapsedPoliciesReport()
            };

            return View(viewModel);
        }

        private List<MonthlySalesReport> GetMonthlySalesReport()
        {
            return _db.Policies
                .GroupBy(p => new { p.PolicyDate.Year, p.PolicyDate.Month })
                .Select(g => new MonthlySalesReport
                {
                    MonthYear = $"{g.Key.Month}/{g.Key.Year}",
                    TotalPolicies = g.Count(),
                    TotalPremium = g.Sum(p => p.VehicleRate)
                })
                .ToList();
        }

        private List<VehicleAnalysisReport> GetVehicleAnalysisReport()
        {
            return _db.Policies
                .Include(p => p.Vehicle)
                .GroupBy(p => p.Vehicle.VehicleName)
                .Select(g => new VehicleAnalysisReport
                {
                    VehicleName = g.Key,
                    TotalPolicies = g.Count(),
                    TotalPremium = g.Sum(p => p.VehicleRate)
                })
                .ToList();
        }

        private List<ClaimReport> GetClaimReports()
        {
            return _db.Claims
                .GroupBy(c => c.PlaceOfAccident)
                .Select(g => new ClaimReport
                {
                    PlaceOfAccident = g.Key,
                    TotalClaims = g.Count(),
                    TotalClaimedAmount = g.Sum(c => c.ClaimableAmount)
                })
                .ToList();
        }

        private List<PolicyDueReport> GetPoliciesDueReport()
        {
            DateTime today = DateTime.Today;
            return _db.Policies
                .Include(p => p.Customer)
                .Include(p => p.Vehicle)
                .Where(p => p.PolicyDate.AddMonths(p.PolicyDuration) > today &&
                            p.PolicyDate.AddMonths(p.PolicyDuration) <= today.AddMonths(1))
                .Select(p => new PolicyDueReport
                {
                    PolicyNumber = p.PolicyNumber,
                    CustomerName = p.Customer.CustomerName,
                    VehicleName = p.Vehicle.VehicleName,
                    ExpiryDate = p.PolicyDate.AddMonths(p.PolicyDuration)
                })
                .ToList();
        }

        private List<LapsedPolicyReport> GetLapsedPoliciesReport()
        {
            DateTime today = DateTime.Today;
            return _db.Policies
                .Include(p => p.Customer)
                .Include(p => p.Vehicle)
                .Where(p => p.PolicyDate.AddMonths(p.PolicyDuration) < today)
                .Select(p => new LapsedPolicyReport
                {
                    PolicyNumber = p.PolicyNumber,
                    CustomerName = p.Customer.CustomerName,
                    VehicleName = p.Vehicle.VehicleName,
                    LapsedDate = p.PolicyDate.AddMonths(p.PolicyDuration)
                })
                .ToList();
        }
    }
}
