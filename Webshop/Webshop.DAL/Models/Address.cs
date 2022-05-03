namespace Webshop.DAL.Models
{
    public class Address
    {
        public readonly string Country;
        public readonly string Region;
        public readonly string ZipCode;
        public readonly string City;
        public readonly string Street;

        public Address(string country, string region, string zipCode, string city, string street)
        {
            Country = country;
            Region = region;
            ZipCode = zipCode;
            City = city;
            Street = street;
        }
    }
}
