namespace Backend.Models
{
    public class PaymentHistory
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int PolicyId { get; set;}
        public decimal PremiumAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; }


        public PaymentHistory()
        {

        }


    }
}
