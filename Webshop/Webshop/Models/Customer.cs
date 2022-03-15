namespace Webshop.Models
{
    public class Customer
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        public PaymentInfo PaymentInfo { get; set; }

        public Customer() { }

        public Customer(int userId, string name, ShippingInfo shippingInfo, PaymentInfo paymentInfo) : this() 
        {
            UserId = userId;
            Name = name;
            ShippingInfo = shippingInfo;
            PaymentInfo = paymentInfo;
        }
    }
}
