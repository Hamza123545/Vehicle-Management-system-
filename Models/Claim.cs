using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; } // Primary Key

        [Required]
        public int PolicyID { get; set; } // Foreign Key (linked to Policy)

        [ForeignKey("PolicyID")]
        public Policy Policy { get; set; } // Navigation Property to Policy

        [Required]
        public DateTime DateOfAccident { get; set; } // The date of the accident

        [Required]
        public string PlaceOfAccident { get; set; } // The place where the accident occurred

        [Required]
        public decimal InsuredAmount { get; set; } // The amount insured in the policy

        [Required]
        public decimal ClaimableAmount { get; set; } // The amount that is claimable
    }
}
