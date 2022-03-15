namespace Webshop.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Login() { }
        public Login(string username, string password) : this()
        {
            Username = username;
            Password = password;
        }
    }
}
