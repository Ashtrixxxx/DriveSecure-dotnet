namespace Backend.Models
{
    public class VehicleDetails
    {

        public int VehicleId { get; set; }  

        public string EngineNumber { get; set; }

        public int YearOfManufacture {  get; set; }

        public int NumberOfSeats { get; set; }

        public string FuelType { get; set; }

        public string ListPrice { get; set; }

        public string LicensePlateNumber { get; set; }

        public int UserID { get; set; }

        public int PolicyID { get; set; }

        public VehicleDetails() { }

    }
}
