using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class SupportDocuments
    {
        [Key]
        public int DocumentId { get; set; }
        [Required]
        public string AddressProof { get; set; }
        [Required]
        public string RCProof { get; set; }

        
        public int UserID { get; set; }

        [ForeignKey("UserID")]

        //Foreign Key
        public UserDetails? UserDetails { get; set; }

        
       
    }
}
