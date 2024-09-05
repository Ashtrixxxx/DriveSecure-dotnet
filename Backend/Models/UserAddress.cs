using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserAddress
    {

        [Required]
        public string StreetAddr { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }

        //Foriegn key 

        [Required]
        public int UserID { get; set; }

        public UserDetails? UserDetails { get; set; }

    }
}
