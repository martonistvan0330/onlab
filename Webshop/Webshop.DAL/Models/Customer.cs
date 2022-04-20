namespace Webshop.DAL.Models
{
    public class Customer
    {
        public readonly string Name;
        public readonly ShippingInfo ShippingInfo;
        public readonly PaymentInfo PaymentInfo;

        public Customer(string name, ShippingInfo shippingInfo, PaymentInfo paymentInfo)
        {
            Name = name;
            ShippingInfo = shippingInfo;
            PaymentInfo = paymentInfo;
        }
    }
}
