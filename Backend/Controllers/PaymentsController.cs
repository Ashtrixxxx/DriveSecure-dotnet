using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Threading.Tasks;

namespace YourAppNamespace.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public PaymentsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51NMZV9SHrtHmWHnMEIAcK77lgDQhc65NjAxLDtqMlts4D1FTpxeyIreQ2slpUoId9a08ZDvuQH8X9xghmoS1A3Sp00Q7RGhTKk"; // Replace with your Stripe Secret Key
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] PaymentRequest request)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(request.Amount * 100), // Convert dollars to cents
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                Description = "Payment for insurance premium",
            };
            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

    }
    public class PaymentRequest
    {
        public double Amount { get; set; }
    }

}
