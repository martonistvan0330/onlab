﻿using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;
using Webshop.Models;

namespace Webshop.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebshopDbContext dbContext;

        public UserRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<bool> ExistsByUsername(string username)
        {
            var user = await dbContext.User.GetUserByUsernameOrNull(username);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> AddUser(NewUser newUser)
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