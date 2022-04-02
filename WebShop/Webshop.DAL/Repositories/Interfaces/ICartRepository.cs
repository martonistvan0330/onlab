using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<EF.Cart?> GetCartBySessionIdOrNull(string sessionId);
        Task<EF.Cart> CreateNewCart(int userId, string sessionId);
    }
}
