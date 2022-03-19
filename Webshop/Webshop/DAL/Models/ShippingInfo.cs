namespace Webshop.Models
{
    public class ShippingInfo
    {
        public ShippingMethod Method { get; set; }
        public Address ShippingAddress { get; set; }

        public ShippingInfo(ShippingMethod method, Address shippingAddress)
        {
            Method = method;
            ShippingAddress = shippingAddress;
        }
    }
}
