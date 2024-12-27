using System;
using System.Collections.Generic;

namespace VehicleInsurance.Models.ReportModels
{
    public class ReportsViewModel
    {
        public List<MonthlySalesReport> MonthlySales { get; set; } // Monthly policy sales
        public List<VehicleAnalysisReport> VehicleAnalysis { get; set; } // Vehicle-wise policy data
        public List<ClaimReport> ClaimReports { get; set; } // Claim statistics
        public List<PolicyDueReport> PoliciesDue { get; set; } // Policies nearing renewal
        public List<LapsedPolicyReport> LapsedPolicies { get; set; } // Expired policies
    }

    public class MonthlySalesReport
    {
        public string MonthYear { get; set; } // e.g., "Dec-2024"
        public int TotalPolicies { get; set; }
        public decimal TotalPremium { get; set; }
    }

    public class VehicleAnalysisReport
    {
        public string VehicleName { get; set; }
        public int TotalPolicies { get; set; }
        public decimal TotalPremium { get; set; }
    }

    public class ClaimReport
    {
        public string PlaceOfAccident { get; set; }
        public int TotalClaims { get; set; }
        public decimal TotalClaimedAmount { get; set; }
    }

    public class PolicyDueReport
    {
        public string PolicyNumber { get; set; }
        public string CustomerName { get; set; }
        public string VehicleName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

    public class LapsedPolicyReport
    {
        public string PolicyNumber { get; set; }
        public string CustomerName { get; set; }
        public string VehicleName { get; set; }
        public DateTime LapsedDate { get; set; }
    }
}
