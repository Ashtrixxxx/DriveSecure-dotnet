namespace Backend.Models
{
    public class PaymentCompletionDto
    {

        public VehicleDetails Vehicle { get; set; }
        public InsurancePolicies PolicyDetails { get; set; }
        public SupportDocuments SupportDocuments { get; set; }
    }
}
