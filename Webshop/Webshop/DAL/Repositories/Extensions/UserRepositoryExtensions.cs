using Microsoft.EntityFrameworkCore;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class UserRepositoryExtensions
    {


        public async static Task<User?> GetUserByUsernameOrNull(this IQueryable<User> users, string username)
            => await users.SingleOrDefaultAsync(u => u.Username.Equals(username));
    }
}
