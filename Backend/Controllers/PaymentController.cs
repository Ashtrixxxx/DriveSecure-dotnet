using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentServices _paymentServices;
        private readonly IPolicyServices _policyServices;
        public PaymentController(IPaymentServices paymentServices, IPolicyServices policyServices)
        {
            _paymentServices = paymentServices;
            _policyServices = policyServices;
        }

        [Authorize(Policy = "AdminAndUser")]
        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetAllPayment()
        {
            return await _paymentServices.GetAllPaymentDetails();
        }

        [Authorize(Policy = "AdminAndUser")]
        [HttpGet("{paymentId}")]
        public async Task<PaymentDetails> GetPayments(int paymentId)
        {
            return await _paymentServices.GetPaymentDetailsById(paymentId);
        }


        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<PaymentDetails> CreatePaymentDetails(PaymentDetails paymentDetails)
        {
            await _policyServices.PolicyPaid(paymentDetails.PolicyID);
           return  await _paymentServices.AddPaymentDetails(paymentDetails);

        }


        [Authorize(Roles = "user")]
        [HttpPut]
        public async Task<PaymentDetails> UpdatePayment(PaymentDetails paymentDetails)
        {
            return await _paymentServices.UpdatePaymentDetails(paymentDetails);
        }


        [Authorize(Roles = "admin")]

        [HttpDelete("{Id}")]
        public async Task DeletePaymentDetails(int Id)
        {
            await _paymentServices.DeletePaymentDetails(Id);
        }

    }
}
