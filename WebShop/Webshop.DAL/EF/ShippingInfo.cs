namespace Webshop.DAL.EF
{
    public class ShippingInfo
    {
        public ShippingInfo()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public int ShippingAddressInfoId { get; set; }

        public ShippingMethod ShippingMethod { get; set; }
        public AddressInfo ShippingAddressInfo { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
