namespace Webshop.DAL
{
    public class Address
    {
        public Address()
        {
            PaymentInfos = new HashSet<PaymentInfo>();
            ShippingInfos = new HashSet<ShippingInfo>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<PaymentInfo> PaymentInfos { get; set; }
        public ICollection<ShippingInfo> ShippingInfos { get; set; }
    }
}
