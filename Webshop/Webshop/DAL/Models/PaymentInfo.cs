namespace Webshop.DAL.Models
{
    public class PaymentInfo
    {
        public readonly PaymentMethod PaymentMethod;
        public readonly AddressInfo BillingAddressInfo;

        public PaymentInfo(PaymentMethod paymentMethod, AddressInfo billingAddressInfo)
        {
            PaymentMethod = paymentMethod;
            BillingAddressInfo = billingAddressInfo;
        }
    }
}
