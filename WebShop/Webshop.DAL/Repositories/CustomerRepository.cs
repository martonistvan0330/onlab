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

        public async Task<bool> ExistsById(int customerId, int userId)
        {
            return await dbContext.Customer.ExistsById(customerId, userId);
        }

        public async Task<IReadOnlyCollection<Models.Customer>> ListCustomers(int userId)
        {

            return await dbContext.Customer
                            .WithShippingInfo()
                            .WithPaymentInfo()
                            .FilterByUser(userId)
                            .GetCustomers();
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

        public async Task<bool> UpdateCustomer(Models.Customer customer, int userId, int shippingInfoId, int paymentInfoId, string oldName)
        {
            if (await dbContext.Customer.ExistsByName(oldName, userId))
            {
                var dbCustomer = await dbContext.Customer.GetCustomerByNameOrNull(oldName, userId);

                if (dbCustomer != null)
                {
                    dbCustomer.Name = customer.Name;
                    dbCustomer.ShippingInfoId = shippingInfoId;
                    dbCustomer.PaymentInfoId = paymentInfoId;

                    dbContext.Customer.Update(dbCustomer);

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
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
