namespace Webshop.Models
{
    public class NewUser
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public NewUser(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
