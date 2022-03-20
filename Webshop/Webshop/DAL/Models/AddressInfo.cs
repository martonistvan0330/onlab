namespace Webshop.DAL.Models
{
    public class AddressInfo
    {
        public readonly string FirstName;
        public readonly string LastName;
        public readonly Address Address;
        public readonly string PhoneNumber;

        public AddressInfo(string firstName, string lastName, Address address, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}
