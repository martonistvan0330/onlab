namespace Webshop.Models
{
    public class Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }

        public Address(string firstName, string lastName, string country, string region, string city, string zipCode, string street, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Region = region;
            City = city;
            ZipCode = zipCode;
            Street = street;
            PhoneNumber = phoneNumber;
        }
    }
}
