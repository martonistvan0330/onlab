using System.Transactions;
using Webshop.DAL.Repositories.Interfaces;
using Webshop.Models;

namespace Webshop.BL
{
    public class UserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            return await userRepository.ExistsByUsername(username);
        }

        public async Task<bool> TryAddUser(NewUser newUser)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                if (await userRepository.AddUser(newUser))
                {
                    transaction.Complete();
                    return true;
                }
                else
                {
                    transaction.Complete();
                    return false;
                }
            }
        }
    }
}
