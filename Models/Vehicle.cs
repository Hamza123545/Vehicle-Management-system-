using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; } 

        [Required]
        [MaxLength(50)]
        public string VehicleName { get; set; } 

        [Required]
        public string VehicleOwnerName { get; set; } 

        [Required]
        public string VehicleModel { get; set; }

        [Required]
        public string VehicleVersion { get; set; } 

        [Required]
        public decimal VehicleRate { get; set; }

        [Required]
        public string VehicleWarranty { get; set; }

        [Required]
        public string VehicleBodyNumber { get; set; }

        [Required]
        public string VehicleEngineNumber { get; set; }

        [Required]
        public string VehicleNumber { get; set; }
        public ICollection<Policy> Policies { get; set; }
        public ICollection<Billing> Billings { get; set; }

    }
}
