using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YourAppNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
        {
            StripeConfiguration.ApiKey = "sk_test_51NMZV9SHrtHmWHnMEIAcK77lgDQhc65NjAxLDtqMlts4D1FTpxeyIreQ2slpUoId9a08ZDvuQH8X9xghmoS1A3Sp00Q7RGhTKk"; // Replace with your secret key

            // Step 1: Set dummy customer data
            var customerName = "John Doe"; // Dummy customer name
            var customerEmail = "johndoe@example.com"; // Dummy customer email
            var customerAddress = new Address
            {
                Line1 = "123 Test Street", // Dummy address line 1
                City = "Test City", // Dummy city
                State = "Test State", // Dummy state
                PostalCode = "123456", // Dummy postal code
                Country = "IN" // Country code for India
            };

            // Step 2: Create a customer with name, email, and address
            var customerOptions = new CustomerCreateOptions
            {
                Name = customerName,
                Email = customerEmail,
                Address = new AddressOptions
                {
                    Line1 = customerAddress.Line1,
                    City = customerAddress.City,
                    State = customerAddress.State,
                    PostalCode = customerAddress.PostalCode,
                    Country = customerAddress.Country // Ensure country is set
                }
            };

            var customerService = new CustomerService();
            var customer = await customerService.CreateAsync(customerOptions);

            // Step 3: Create the checkout session with the customer ID
            var lineItemList = new List<SessionLineItemOptions>();

            foreach (var item in request.LineItems)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = item.PriceData.Currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.PriceData.ProductData.Name,
                        },
                        UnitAmount = item.PriceData.UnitAmount,
                    },
                    Quantity = item.Quantity
                };

                lineItemList.Add(sessionLineItem);
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItemList,
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/paymentsuccess",
                CancelUrl = "http://localhost:3000/cancel",
                Customer = customer.Id, // Attach customer to the session
                BillingAddressCollection = "required", // Require billing address
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string> { "IN" } // Allow India
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Ok(new { sessionId = session.Id });
        }
    }

    public class CreateCheckoutSessionRequest
    {
        public List<LineItem> LineItems { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public Address CustomerAddress { get; set; }
    }

    public class LineItem
    {
        [JsonPropertyName("price_data")]
        public PriceData PriceData { get; set; }
        public int Quantity { get; set; }
    }

    public class PriceData
    {
        public string Currency { get; set; }
        [JsonPropertyName("product_data")]
        public ProductData ProductData { get; set; }
        public int UnitAmount { get; set; }
    }

    public class ProductData
    {
        public string Name { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } // Add country to Address class
    }
}
