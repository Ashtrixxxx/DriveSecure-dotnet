using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TempFormData
    {
        [Key]
        public int FormId { get; set; }
        [Required]
        public string FormData { get; set; }
        [Required]
        public string Status { get; set; }


        //Foreign Key
        public int UserID { get; set; }
        public UserDetails? UserDetails { get; set; }
 
        [Required]
        public DateOnly DateCreated { get; set; }


    }
}
