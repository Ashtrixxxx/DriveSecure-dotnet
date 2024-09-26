using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class UserProfile
    {
        
        [Key]
        public int ProfileID { get; set; }
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
        public string Occupation { get; set; }

        [Required]
        public string StreetAddr { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public bool IsProfiled { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public UserDetails? UserDetails { get; set; }

    }
}
