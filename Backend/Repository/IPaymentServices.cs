using Backend.Models;

namespace Backend.Repository
{
    public interface IPaymentServices
    {
        Task<IEnumerable<PaymentDetails>> GetAllPaymentDetails();
        Task<PaymentDetails> GetPaymentDetailsById(int id);
        Task<PaymentDetails> AddPaymentDetails(PaymentDetails paymentDetails);
        Task<PaymentDetails> UpdatePaymentDetails(PaymentDetails paymentDetails);
        Task DeletePaymentDetails(int id);
    }
}
