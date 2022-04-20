namespace Webshop.DAL.Models
{
	public class PaymentInfo
	{
		public readonly int PaymentMethodId;
		public readonly string PaymentMethodName;
		public readonly AddressInfo BillingAddressInfo;

		public PaymentInfo(int PaymentMethodId, string PaymentMethodName, AddressInfo billingAddressInfo)
		{
			this.PaymentMethodId = PaymentMethodId;
			this.PaymentMethodName = PaymentMethodName;
			BillingAddressInfo = billingAddressInfo;
		}
	}
}
