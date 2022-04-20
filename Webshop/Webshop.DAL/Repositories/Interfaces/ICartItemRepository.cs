using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<(bool, int)> AddCartItem(NewCartItem cartItem, int cartId);
        Task<bool> UpdateCartItem(int cartItemId, int cartId, int amount);
        Task<bool> RemoveCartItem(int cartItemId, int cartId);
        Task<int> GetAmountById(int cartItemId);
        Task<EF.CartItem?> GetByIdOrNull(int cartItemId);
        Task<IReadOnlyCollection<CartItemWithId>> ListCartItems(int cartId);
    }
}
