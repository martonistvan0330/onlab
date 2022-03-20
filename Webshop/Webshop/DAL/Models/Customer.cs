namespace Webshop.DAL.Models
{
    public class Customer
    {
        public readonly string SessionId;
        public readonly string Name;
        public readonly ShippingInfo ShippingInfo;
        public readonly PaymentInfo PaymentInfo;

        public Customer(string sessionId, string name, ShippingInfo shippingInfo, PaymentInfo paymentInfo)
        {
            SessionId = sessionId;
            Name = name;
            ShippingInfo = shippingInfo;
            PaymentInfo = paymentInfo;
        }
    }
}
