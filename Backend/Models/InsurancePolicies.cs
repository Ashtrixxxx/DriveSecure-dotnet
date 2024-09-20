using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class InsurancePolicies
    {
        [Key]
        public int PolicyID { get; set; }
        [Required]
        public string CoverageType { get; set; } // Third party, self , Theft cover
        [Required]
        public DateOnly CoverageStartDate { get; set; }
        [Required]
        public DateOnly CoverageEndDate { get; set; }
        public bool IsRenewed { get; set; }
        [Required]
        public decimal CoverageAmount { get; set; }
        public int Status { get; set; }

        public int UserID { get; set; }

        //Foreign Key
        [ForeignKey("UserID")]
        public UserDetails? UserDetails { get; set; }

      

    }
}
