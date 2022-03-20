using Webshop.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ExistsByUsername(string username);
        Task<bool> AddUser(NewUser newUser);
    }
}
