using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInsurance.Models
{
    public class Billing
    {
        [Key]
        public int BillID { get; set; }

        // Foreign Key to Policy
        [Required]
        public int PolicyID { get; set; }

        // Navigation Property to Policy
        [ForeignKey("PolicyID")]
        public Policy Policy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}
