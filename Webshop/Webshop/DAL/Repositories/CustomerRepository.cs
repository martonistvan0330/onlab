using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebshopDbContext dbContext;

        public CustomerRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<bool> ExistsByName(string customerName, int userId)
        {
            return await dbContext.Customer.ExistsByName(customerName, userId);
        }

        public async Task<bool> AddCustomer(Models.Customer customer, int userId, int shippingInfoId, int paymentInfoId)
        {
            var dbCustomer = new Customer()
            {
                UserId = userId,
                Name = customer.Name,
                ShippingInfoId = shippingInfoId,
                PaymentInfoId = paymentInfoId,
                MainCustomer = false,
            };
            if (await dbContext.Customer.ExistsByName(customer.Name, userId))
            {
                return false;
            }
            else
            {
                dbContext.Customer.Add(dbCustomer);
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
}
