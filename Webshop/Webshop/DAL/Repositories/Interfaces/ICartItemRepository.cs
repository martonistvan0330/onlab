using Webshop.DAL.Models;

namespace Webshop.DAL.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        Task<(bool, int)> AddCartItem(CartItem cartItem, int cartId, int productId, int sizeId);
        Task<bool> UpdateCartItem(int cartItemId, int cartId, int amount);
        Task<int> GetAmountById(int cartItemId);
        Task<EF.CartItem?> GetByIdOrNull(int cartItemId);
        Task<IReadOnlyCollection<CartItemWithId>> ListCartItems(int cartId);
    }
}
