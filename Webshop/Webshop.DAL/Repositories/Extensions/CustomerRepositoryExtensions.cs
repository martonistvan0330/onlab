using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class CustomerRepositoryExtensions
    {
        public static IQueryable<Customer> FilterByUser(this IQueryable<Customer> customers, string userId)
        {
            return customers.Where(c => c.UserId.Equals(userId));
        }

        public static IQueryable<Customer> FindByName(this IQueryable<Customer> customers, string customerName)
        {
            return customers.Where(c => c.Name.Equals(customerName));
        }

        public static IQueryable<Customer> FindById(this IQueryable<Customer> customers, int customerId)
        {
            return customers.Where(c => c.Id == customerId);
        }

        public static async Task<bool> ExistsByName(this IQueryable<Customer> customers, string customerName, string userId)
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

        public static async Task<bool> ExistsById(this IQueryable<Customer> customers, int customerId, string userId)
        {
            var dbCustomer = await customers
                                    .FilterByUser(userId)
                                    .FindById(customerId)
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

        /*public static async Task<int> GetIdByName(this IQueryable<Customer> customers, string customerName, int userId)
        {
            return await customers
                            .FilterByUser(userId)
                            .FindByName(customerName)
                            .GetId();
        }*/

        /*public static async Task<IReadOnlyCollection<Models.Customer>> GetCustomers(this IQueryable<Customer> customers)
        {
            return await customers.Select(dbRec => dbRec.GetCustomer())
                                  .ToArrayAsync();
        }*/

        public static async Task<Customer?> GetCustomerByNameOrNull(this IQueryable<Customer> customers, string customerName, string userId)
        {
            return await customers
                            .FilterByUser(userId)
                            .FindByName(customerName)
                            .GetCustomerOrNull();
        }

        public static IQueryable<Customer> WithShippingInfo(this IQueryable<Customer> customers)
        {
            return customers
                    .Include(c => c.ShippingInfo)
                    .Include(c => c.ShippingInfo.ShippingMethod)
                    .Include(c => c.ShippingInfo.ShippingAddressInfo)
                    .Include(c => c.ShippingInfo.ShippingAddressInfo.Address);
        }

        public static IQueryable<Customer> WithPaymentInfo(this IQueryable<Customer> customers)
        {
            return customers
                    .Include(c => c.PaymentInfo)
                    .Include(c => c.PaymentInfo.PaymentMethod)
                    .Include(c => c.PaymentInfo.BillingAddressInfo)
                    .Include(c => c.PaymentInfo.BillingAddressInfo.Address);
        }

        /*public static Models.Customer GetCustomer(this Customer dbRecord)
            => new Models.Customer(
                dbRecord.Name,
                new Models.ShippingInfo(
                    new Models.ShippingMethod(
                        dbRecord.ShippingInfo.ShippingMethod.Method
                        ),
                    new Models.AddressInfo(
                        dbRecord.ShippingInfo.ShippingAddressInfo.FirstName,
                        dbRecord.ShippingInfo.ShippingAddressInfo.LastName,
                        new Models.Address(
                            dbRecord.ShippingInfo.ShippingAddressInfo.Address.Country,
                            dbRecord.ShippingInfo.ShippingAddressInfo.Address.Region,
                            dbRecord.ShippingInfo.ShippingAddressInfo.Address.City,
                            dbRecord.ShippingInfo.ShippingAddressInfo.Address.ZipCode,
                            dbRecord.ShippingInfo.ShippingAddressInfo.Address.Street
                            ),
                        dbRecord.ShippingInfo.ShippingAddressInfo.PhoneNumber
                        )
                    ),
                new Models.PaymentInfo(
                    new Models.PaymentMethod(
                        dbRecord.PaymentInfo.PaymentMethod.Method,
                        dbRecord.PaymentInfo.PaymentMethod.Deadline
                        ),
                    new Models.AddressInfo(
                        dbRecord.PaymentInfo.BillingAddressInfo.FirstName,
                        dbRecord.PaymentInfo.BillingAddressInfo.LastName,
                        new Models.Address(
                            dbRecord.PaymentInfo.BillingAddressInfo.Address.Country,
                            dbRecord.PaymentInfo.BillingAddressInfo.Address.Region,
                            dbRecord.PaymentInfo.BillingAddressInfo.Address.City,
                            dbRecord.PaymentInfo.BillingAddressInfo.Address.ZipCode,
                            dbRecord.PaymentInfo.BillingAddressInfo.Address.Street
                            ),
                        dbRecord.PaymentInfo.BillingAddressInfo.PhoneNumber
                        )
                    )
                );*/

        public static async Task<int> GetId(this IQueryable<Customer> customers)
            => await customers.Select(c => c.Id).SingleAsync();

        public static async Task<Customer?> GetCustomerOrNull(this IQueryable<Customer> customers)
            => await customers.SingleOrDefaultAsync();
    }
}
