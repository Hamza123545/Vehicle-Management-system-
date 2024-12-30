using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInsurance.Models
{
    public class Billing
    {
        [Key]
        public int BillID { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Required]
        public int PolicyID { get; set; }

        [ForeignKey("PolicyID")]
        public Policy Policy { get; set; }

        [Required]
        [MaxLength(150)]
        public string AddressProof { get; set; }

        [Required]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public int BillNumber { get; set; }

        [Required]
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleModel { get; set; }

        [Required]
        public decimal VehicleRate { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleBodyNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string VehicleEngineNumber { get; set; }

        [Required]
        public DateTime BillingDate { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
