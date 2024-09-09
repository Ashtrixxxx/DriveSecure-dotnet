using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;

        public PaymentController(IPaymentServices paymentServices)
        {
            _paymentServices = paymentServices;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetAllPayment()
        {
            return await _paymentServices.GetAllPaymentDetails();
        }

        [HttpGet("{paymentId}")]
        public async Task<PaymentDetails> GetPayments(int paymentId)
        {
            return await _paymentServices.GetPaymentDetailsById(paymentId);
        }

        [HttpPost]
        public async Task<PaymentDetails> CreatePaymentDetails(PaymentDetails paymentDetails)
        {
           return  await _paymentServices.AddPaymentDetails(paymentDetails);
        }

        [HttpPut]
        public async Task<PaymentDetails> UpdatePayment(PaymentDetails paymentDetails)
        {
            return await _paymentServices.UpdatePaymentDetails(paymentDetails);
        }

        [HttpDelete]
        public async Task DeletePaymentDetails(int Id)
        {
            await _paymentServices.DeletePaymentDetails(Id);
        }

    }
}
