using System.ComponentModel.DataAnnotations;

namespace VehicleInsurance.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseID { get; set; } 

        [Required]
        public DateTime ExpenseDate { get; set; } 

        [Required]
        public string ExpenseType { get; set; } 

        [Required]
        public decimal Amount { get; set; } 
    }
}
