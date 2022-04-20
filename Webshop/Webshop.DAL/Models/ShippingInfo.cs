namespace Webshop.DAL.Models
{
    public class ShippingInfo
    {
        public readonly int ShippingMethodId;
        public readonly string ShippingMethodName;
        public readonly AddressInfo ShippingAddressInfo;

        public ShippingInfo(int shippingMethodId, string shippingMethodName, AddressInfo shippingAddressInfo)
        {
            ShippingMethodId = shippingMethodId;
            ShippingMethodName = shippingMethodName;
            ShippingAddressInfo = shippingAddressInfo;
        }
    }
}
