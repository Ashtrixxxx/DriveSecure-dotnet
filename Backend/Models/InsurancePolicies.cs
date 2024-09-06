using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class InsurancePolicies
    {
        [Key]
        public int PolicyID { get; set; }
        [Required]
        public string CoverageType { get; set; }
        [Required]
        public DateOnly CoverageStartDate { get; set; }
        [Required]
        public DateOnly CoverageEndDate { get; set; }
        [Required]
        public decimal CoverageAmount { get; set; }
        public int Status { get; set; }


        //Foreign Key
        public UserDetails? UserDetails { get; set; }

      

    }
}
