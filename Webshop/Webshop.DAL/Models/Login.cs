namespace Webshop.DAL.Models
{
    public class Login
    {
        public readonly string Username;
        public readonly string Password;
        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
