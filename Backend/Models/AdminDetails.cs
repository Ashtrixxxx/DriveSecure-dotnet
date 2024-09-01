using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class AdminDetails
    {
        [Key]

        public int AdminId {get;set;}
        [Required]
        [MaxLength(100)]
        public string AdminName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength (20)]
        public string password { get; set;}


    }
}
