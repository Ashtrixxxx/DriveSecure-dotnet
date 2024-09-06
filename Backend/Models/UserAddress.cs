using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class UserAddress
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public string StreetAddr { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }

        //Foriegn key 


        public UserDetails? UserDetails { get; set; }

    }
}
