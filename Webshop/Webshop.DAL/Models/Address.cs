namespace Webshop.DAL.Models
{
    public class Address
    {
        public readonly string Country;
        public readonly string Region;
        public readonly string City;
        public readonly string ZipCode;
        public readonly string Street;

        public Address(string country, string region, string city, string zipCode, string street)
        {
            Country = country;
            Region = region;
            City = city;
            ZipCode = zipCode;
            Street = street;
        }
    }
}
