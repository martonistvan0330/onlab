using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebshopDbContext dbContext;

        public UserRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<Guid?> Login(string username, string password)
        {
            var dbUser = await dbContext.User
                                   .GetUserByUsernameOrNull(username);
            if (dbUser == null || !dbUser.Password.Equals(password))
            {
                return null;
            }
            else
            {
                var sessionId = Guid.NewGuid();
                var dbRecord = new Session()
                {
                    SessionId = sessionId.ToString(),
                    User = dbUser,
                    IsActive = true,
                };

                dbContext.Session.Add(dbRecord);
                await dbContext.SaveChangesAsync();
                return sessionId;
            }
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var dbUser = await dbContext.User.GetUserByUsernameOrNull(username);
            if (dbUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> AddUser(Models.NewUser newUser)
        {
            var dbUser = new User()
            {
                Email = newUser.Email,
                Username = newUser.Username,
                Password = newUser.Password,
            };

            var dbRecord = await dbContext.User
                                       .GetUserByUsernameOrNull(newUser.Username);
            if (dbRecord != null)
            {
                return false;
            }

            dbContext.User.Add(dbUser);

            try
            {
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
