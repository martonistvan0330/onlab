namespace Webshop.DAL
{
    public class ShippingInfo
    {
        public ShippingInfo()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public int ShippingMethodId { get; set; }
        public int ShippingAddressId { get; set; }

        public ShippingMethod ShippingMethod { get; set; }
        public Address ShippingAddress { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
