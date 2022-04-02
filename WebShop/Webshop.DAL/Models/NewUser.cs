namespace Webshop.DAL.Models
{
    public class NewUser
    {
        public readonly string Email;
        public readonly string Username;
        public readonly string Password;

        public NewUser(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
