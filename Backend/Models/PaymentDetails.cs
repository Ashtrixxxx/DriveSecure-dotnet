using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class PaymentDetails
    {
        [Key]
        public int PaymentId { get; set; }
        [Required]
        public decimal PremiumAmount { get; set; }
        
        [Required]
        public DateOnly PaymentDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        public int UserID { get; set; }
        //Foreign Key
        [ForeignKey("UserID")]

        public UserDetails? UserDetails { get; set; }

        // Foreign key for policy 
        public int PolicyID { get; set; }

        [ForeignKey("PolicyID")]

        public InsurancePolicies? Policy { get; set; }
        


    }
}
