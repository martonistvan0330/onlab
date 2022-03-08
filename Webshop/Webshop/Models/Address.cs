namespace Webshop.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }

        public Address(int id, string? name, string country, string region, string city, string zipCode, string street)
        {
            Id = id;
            Name = name;
            Country = country;
            Region = region;
            City = city;
            ZipCode = zipCode;
            Street = street;
        }
    }
}
