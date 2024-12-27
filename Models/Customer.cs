using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleInsurance.Models
{
    public class Customer
    {
         
        [Key]
        public int CustomerID { get; set; } 

        [Required]
        [MinLength(3)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
 
        [Required]
        public string Address { get; set; } 

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string AddressProof { get; set; }

        [Required]
        public string Role { get; set; } = "customer";
        public ICollection<Policy> Policies { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}

