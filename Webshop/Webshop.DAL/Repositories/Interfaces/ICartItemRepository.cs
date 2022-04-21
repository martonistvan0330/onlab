using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<(bool, int)> AddCartItem(NewCartItem cartItem, int cartId);
        Task<(bool, int)> UpdateCartItem(UpdateCartItem cartItem, int cartId);
        Task<bool> RemoveCartItem(int cartItemId, int cartId);
        Task<int> GetAmountById(int cartItemId);
        Task<EF.CartItem?> GetByIdOrNull(int cartItemId);
        Task<IReadOnlyCollection<CartItem>> ListCartItems(int cartId);
    }
}
