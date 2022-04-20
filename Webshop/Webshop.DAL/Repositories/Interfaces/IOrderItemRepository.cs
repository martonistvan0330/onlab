using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<(bool, int)> AddOrderItem(CartItem cartItem, int orderId, int statusId);
        Task<int> GetAmountById(int orderItemId);
    }
}
