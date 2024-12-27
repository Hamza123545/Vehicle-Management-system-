using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInsurance.Models
{
    public class Policy
    {

        [Key]
        public int PolicyID { get; set; }

        [Required]
        [MaxLength(20)]
        public string PolicyNumber { get; set; }

        [Required]
        public DateTime PolicyDate { get; set; }

        [Required]
        [Range(1, 120)]
        public int PolicyDuration { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        public int VehicleID { get; set; } 

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }

        [Required]
        public string VehicleNumber { get; set; }

        [Required]
        public decimal VehicleRate { get; set; }

        [MaxLength(30)]
        public string VehicleWarranty { get; set; }

        [MaxLength(30)]
        public string VehicleModel { get; set; }

        [MaxLength(30)]
        public string VehicleVersion { get; set; }

        [Required]
        public string VehicleBodyNumber { get; set; }

        [Required]
        public string VehicleEngineNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string AddressProof { get; set; }
    }
}























