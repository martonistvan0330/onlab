namespace Webshop.DAL.EF
{
    public class Address
    {
        public Address()
        {
            AddressInfos = new HashSet<AddressInfo>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        public ICollection<AddressInfo> AddressInfos { get; set; }
    }
}
