using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{

    public enum Role
    {
        user,
        admin
    }

    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPass { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Role Role { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetTokenExpiry { get; set; }

        //Navigation Properties
        //public ICollection<InsurancePolicies> InsurancePolicies { get; set; } = new List<InsurancePolicies>();
        //public ICollection<PaymentHistory> paymentHistories { get; set; } = new List<PaymentHistory>();
        //public ICollection<SupportDocuments> SupportDocuments { get; set; } = new List<SupportDocuments>();
        //public ICollection<VehicleDetails> VehicleDetails { get; set; } = new List<VehicleDetails>();


    }
}
