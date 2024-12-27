using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.Models
{
    public class Estimate
    {
        [Key]
        public int EstimateId { get; set; } // Primary Key

        [Required]
        public int CustomerID { get; set; } // Foreign Key (linked to Customer)

        [Required]
        public int EstimateNumber { get; set; } // Unique Estimate Number


        [Required]
        [StringLength(15)]
        public string? CustomerPhoneNumber { get; set; } // Phone Number (numeric)

        [Required]
        [StringLength(100)]
        public string VehicleName { get; set; } // Vehicle Name (character)

        [Required]
        [StringLength(50)]
        public string VehicleModel { get; set; } // Vehicle Model (character)

        [Required]
        [Range(1, int.MaxValue)]
        public decimal VehicleRate { get; set; } // Vehicle Rate (numeric)

        [Required]
        [StringLength(50)]
        public string VehicleWarranty { get; set; } // Vehicle Warranty (character)

        [Required]
        [StringLength(50)]
        public string VehiclePolicyType { get; set; } // Vehicle Policy Type (character)


        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; } // Navigation Property
    }
}
