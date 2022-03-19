namespace Webshop.Models
{
    public class PaymentInfo
    {
        public PaymentMethod Method { get; set; }
        public Address BillingAddress { get; set; }

        public PaymentInfo(PaymentMethod method, Address billingAddress)
        {
            Method = method;
            BillingAddress = billingAddress;
        }
    }
}
