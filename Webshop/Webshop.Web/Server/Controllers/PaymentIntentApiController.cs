using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Webshop.BL;

namespace Webshop.Web.Server.Controllers
{
	[Route("api/create-payment-intent")]
	[ApiController]
	public class PaymentIntentApiController : Controller
	{
		private readonly CustomerManager customerManager;
		private readonly CartManager cartManager;

		public PaymentIntentApiController(CustomerManager customerManager, CartManager cartManager)
		{
			this.customerManager = customerManager;
			this.cartManager = cartManager;
		}

		[HttpPost]
		public async Task<ActionResult> Create(PaymentIntentCreateRequest request)
		{
			var dbCustomer = await customerManager.GetById(request.CustomerId, request.UserId);

			var shippingAddress = dbCustomer.ShippingInfo.ShippingAddressInfo.Address;
			var billingAddress = dbCustomer.PaymentInfo.BillingAddressInfo.Address;

			var customerOptions = new CustomerCreateOptions()
			{
				Address = new AddressOptions()
				{
					Country = "HU",
					State = billingAddress.Region,
					PostalCode = billingAddress.ZipCode,
					City = billingAddress.City,
					Line1 = billingAddress.Street,
				},
				Email = "mistvan0330@gmail.com",
				Name = dbCustomer.Name,
				Phone = dbCustomer.PaymentInfo.BillingAddressInfo.PhoneNumber,
				Shipping = new ShippingOptions()
				{
					Address = new AddressOptions()
                    {
						Country = "HU",
						State = shippingAddress.Region,
						PostalCode = shippingAddress.ZipCode,
						City = shippingAddress.City,
						Line1 = shippingAddress.Street,
					},
					Name = dbCustomer.ShippingInfo.ShippingAddressInfo.FirstName + " " + dbCustomer.ShippingInfo.ShippingAddressInfo.LastName,
					Phone = dbCustomer.ShippingInfo.ShippingAddressInfo.PhoneNumber,
                }

			};
			var customerService = new CustomerService();
			var customer = customerService.Create(customerOptions);

			var paymentIntentService = new PaymentIntentService();
			var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
			{
				Amount = await CalculateOrderAmount(request.UserId),
				Currency = "huf",
				Customer = customer.Id,
			});

			return Json(new { clientSecret = paymentIntent.ClientSecret });
		}

		private async Task<int> CalculateOrderAmount(string userId)
		{
			var total = await cartManager.GetTotal(userId);
			return (int) Math.Round((total * 100), 0);
 		}

		public class PaymentIntentCreateRequest
		{
			[JsonProperty("customerId")]
			public int CustomerId { get; set; }

			[JsonProperty("userId")]
			public string UserId { get; set; }
		}
	}
}
