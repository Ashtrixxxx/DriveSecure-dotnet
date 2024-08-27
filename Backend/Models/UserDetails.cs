namespace Backend.Models
{
    public class UserDetails
    {

        public int UserID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateOnly DOB { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string StreetAddr { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        public string City { get; set; }

        public string Occupation { get; set; }

        public UserDetails()
        {

        }

    }
}
