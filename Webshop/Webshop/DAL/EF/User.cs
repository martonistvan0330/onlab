namespace Webshop.DAL.EF
{
    public class User
    {
        public User()
        {
            Customers = new HashSet<Customer>();
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
