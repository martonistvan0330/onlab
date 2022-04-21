using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> ExistsByName(string customerName, string userId);
        Task<bool> ExistsById(int customerId, string userId);
        //Task<IReadOnlyCollection<Customer>> ListCustomers(int userId);
        Task<(bool, int)> AddCustomer(Customer customer, int shippingInfoId, int paymentInfoId, string userId);
        //Task<bool> UpdateCustomer(Customer customer, int userId, int shippingInfoId, int paymentInfoId, string oldName);
    }
}
