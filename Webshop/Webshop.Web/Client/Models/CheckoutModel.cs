using System.ComponentModel.DataAnnotations;
using Webshop.Web.Shared.Models;

namespace Webshop.Web.Client.Models
{
	public class CheckoutModel
	{
		public AddressInfo ShippingAddressInfo { get; set; }
		public AddressInfo BillingAddressInfo { get; set; }

		public CheckoutModel()
		{
			ShippingAddressInfo = new();
			BillingAddressInfo = new();
		}
	}
}
