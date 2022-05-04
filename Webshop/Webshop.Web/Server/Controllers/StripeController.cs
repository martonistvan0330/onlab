using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;

namespace Webshop.Web.Server.Controllers
{
    [Route("api/stripe")]
    [ApiController]
    public class StripeController : Controller
    {
        [HttpPost("customer")]
        public ActionResult<string> Create(CustomerCreateRequest request)
        {
            var customerId = request.CustomerId;

            var options = new CustomerCreateOptions
            {
                Description = "My First Test Customer (created for API docs)",
            };
            var service = new CustomerService();
            var customer = service.Create(options);
            return Json(new { stripe_customer_id = customer.Id });
        }

        [HttpPost]
        public ActionResult<string> CreatePaymentMethod(int customerId)
        {
            var options = new CustomerCreateOptions
            {
                Description = "My First Test Customer (created for API docs)",
            };
            var service = new CustomerService();
            var customer = service.Create(options);
            return customer.Id;
        }

        public class CustomerCreateRequest
        {
            [JsonProperty("customerId")]
            public int CustomerId { get; set; }

            [JsonProperty("userId")]
            public string UserId { get; set; }
        }
    }
}
