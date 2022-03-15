namespace Webshop.DAL
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public bool MainCustomer { get; set; }

        public User User { get; set; }
    }
}
