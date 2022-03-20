namespace Webshop.DAL.Models
{
    public class ShippingInfo
    {
        public readonly ShippingMethod ShippingMethod;
        public readonly AddressInfo ShippingAddressInfo;

        public ShippingInfo(ShippingMethod shippingMethod, AddressInfo shippingAddressInfo)
        {
            ShippingMethod = shippingMethod;
            ShippingAddressInfo = shippingAddressInfo;
        }
    }
}
