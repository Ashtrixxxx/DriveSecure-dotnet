using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class SupportDocuments
    {
        [Key]
        public int DocumentId { get; set; }
<<<<<<< HEAD
=======
        
>>>>>>> Ashefa
        [Required]
        public string AddressProof { get; set; }
        [Required]
        public int RCProof { get; set; }
<<<<<<< HEAD
        
        //Foreign Key
        public int UserId { get; set; }
        public UserDetails? UserDetails { get; set; }
=======
        //Foreign Key
        public int UserId { get; set; }
        public UserDetails UserDetails { get; set; }


>>>>>>> Ashefa
    }
}
