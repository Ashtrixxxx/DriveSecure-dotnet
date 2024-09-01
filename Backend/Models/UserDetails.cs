using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserDetails
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserPass { get; set; }
        [Required]
        public string ProfileUrl { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateOnly DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string StreetAddr { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Occupation { get; set; }

        //Navigation Properties
        public ICollection<InsurancePolicies> InsurancePolicies { get; set; } = new List<InsurancePolicies>();
        public ICollection<PaymentHistory> paymentHistories { get; set; } = new List<PaymentHistory>();
        public ICollection<SupportDocuments> SupportDocuments { get; set; } = new List<SupportDocuments>();
        public ICollection<VehicleDetails> VehicleDetails { get; set; } = new List<VehicleDetails>();

    }
}
