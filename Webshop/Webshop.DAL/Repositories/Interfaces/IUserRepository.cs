using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid?> Login(string username, string password);
        Task<bool> ExistsByUsername(string username);
        Task<bool> AddUser(NewUser newUser);
    }
}
