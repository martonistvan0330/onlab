using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> ExistsByName(string customerName, int userId);
        Task<IReadOnlyCollection<Customer>> ListCustomers(int userId);
        Task<bool> AddCustomer(Customer customer, int userId, int shippingInfoId, int paymentInfoId);
        Task<bool> UpdateCustomer(Customer customer, int userId, int shippingInfoId, int paymentInfoId, string oldName);
    }
}
