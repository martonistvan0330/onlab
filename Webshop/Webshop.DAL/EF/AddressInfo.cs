namespace Webshop.DAL.EF
{
    public class AddressInfo
    {
        public AddressInfo()
        {
            PaymentInfos = new HashSet<PaymentInfo>();
            ShippingInfos = new HashSet<ShippingInfo>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<PaymentInfo> PaymentInfos { get; set; }
        public ICollection<ShippingInfo> ShippingInfos { get; set; }
    }
}
