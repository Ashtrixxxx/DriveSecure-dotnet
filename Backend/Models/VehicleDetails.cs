using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class VehicleDetails
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string VehicleType { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public string EngineNumber { get; set; }
        [Required]
        public string EngineType { get; set; }
        [Required]
        public int YearOfManufacture {  get; set; }
        [Required]
        public int NumberOfSeats { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public string ListPrice { get; set; }
        [Required]
        public string LicensePlateNumber { get; set; }
        
        [Required]
        public string VehicleCondition { get; set; }
        [Required]
        [MaxLength(255)]
        public string ServiceHistory { get; set; }
        [Required]
        public DateOnly LastServiceDate { get; set; }
        [Required]
        public int VehicleIdentificationNumber { get; set; }
        [Required]
        public string TransmissionType { get; set; }
        [Required]
        public int NumberOfPreviousOwners { get; set; }
        [Required]
        public DateOnly RegistrationDate { get; set;}



        //Foreign key
        public int UserID { get; set; }
        public UserDetails? UserDetails { get; set; }

        //Foreign key
        public int PolicyID { get; set; }
        public InsurancePolicies? Insurance { get; set; }


    }
}
