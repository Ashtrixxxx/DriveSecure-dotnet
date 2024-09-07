using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class PaymentDetails
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
        public UserDetails? UserDetails { get; set; }

        // Foreign key for policy 

        public InsurancePolicies? Policy { get; set; }
        


    }
}
