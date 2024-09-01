using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserDetails
    {

        [Required]
        public int UserID { get; set; }
      
        public string UserPass { get; set; }
        [Required]
        public string ProfileUrl { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
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

        public UserDetails()
        {

        }

    }
}
