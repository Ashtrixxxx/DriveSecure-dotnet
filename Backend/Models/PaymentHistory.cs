using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PaymentHistory
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public decimal PremiumAmount { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; }
        [Required]
        public DateOnly PaymentDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        //Foreign Key
        public int UserID { get; set; }
        public UserDetails? UserDetails { get; set; }

        // Foreign key for policy 
        public int PolicyID { get; set; }

        public InsurancePolicies? Policy { get; set; }
        


    }
}
