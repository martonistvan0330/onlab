using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdOrNull(string userId);
        Task<double> GetTotalByUser(string userId);
        Task<Cart> CreateNewCart(string userId);
    }
}
