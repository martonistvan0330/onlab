using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class CustomerRepositoryExtensions
    {
        public static IQueryable<Customer> FilterByUser(this IQueryable<Customer> customers, int userId)
        {
            return customers.Where(c => c.UserId == userId);
        }

        public static IQueryable<Customer> FindByName(this IQueryable<Customer> customers, string customerName)
        {
            return customers.Where(c => c.Name.Equals(customerName));
        }

        public static async Task<bool> ExistsByName(this IQueryable<Customer> customers, string customerName, int userId)
        {
            var dbCustomer = await customers
                                    .FilterByUser(userId)
                                    .FindByName(customerName)
                                    .GetCustomerOrNull();
            if (dbCustomer == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<Customer?> GetCustomerByNameOrNull(this IQueryable<Customer> customers, string customerName, int userId)
        {
            return await customers
                            .FilterByUser(userId)
                            .FindByName(customerName)
                            .GetCustomerOrNull();
        }

        public static async Task<Customer?> GetCustomerOrNull(this IQueryable<Customer> customers)
        {
            return await customers.SingleOrDefaultAsync();
        }
    }
}
