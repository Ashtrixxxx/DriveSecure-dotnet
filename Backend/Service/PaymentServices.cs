using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;

namespace Backend.Service
{
    public class PaymentServices:IPaymentServices
    {
        private readonly DriveDbContext _context;
        public PaymentServices(DriveDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentDetails>> GetAllPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        public async Task<PaymentDetails> GetPaymentDetailsById(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }

        public async Task<PaymentDetails> AddPaymentDetails(PaymentDetails paymentDetails)
        {
            _context.PaymentDetails.Add(paymentDetails);
            await _context.SaveChangesAsync();
            return paymentDetails;
        }

        public async Task<PaymentDetails> UpdatePaymentDetails(PaymentDetails paymentDetails)
        {
            _context.Entry(paymentDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return paymentDetails;
        }

        public async Task DeletePaymentDetails(int id)
        {
            var paymentDetails = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetails != null)
            {
                _context.PaymentDetails.Remove(paymentDetails);
                await _context.SaveChangesAsync();
            }
        }
    }
}
