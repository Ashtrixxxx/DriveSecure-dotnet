namespace Backend.Models
{
    public class InsurancePolicies
    {

        public int PolicyID { get; set; }

        public int UserID { get; set; }

        public string CoverageType { get; set; }

        public DateOnly CoverageStartDate { get; set; }

        public DateOnly CoverageEndDate { get; set; }

        public decimal CoverageLimit { get; set; }

        public int Status { get; set; }

        public InsurancePolicies()
        {

        }

    }
}
