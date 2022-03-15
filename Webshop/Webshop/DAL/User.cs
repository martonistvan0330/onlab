namespace Webshop.DAL
{
    public class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
        }

        public User(string email, string username, string password) : this()
        {
            Email = email;
            Username = username;
            Password = password;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
