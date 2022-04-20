using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class CartItemRepositoryExtensions
    {
        public static IQueryable<CartItem> FilterByCart(this IQueryable<CartItem> cartItems, int cartId)
        {
            return cartItems.Where(ci => ci.CartId == cartId);
        }

        public static IQueryable<CartItem> WithProduct(this IQueryable<CartItem> cartItems)
        {
            return cartItems
                    .Include(ci => ci.Product);
        }

        public static IQueryable<CartItem> WithSize(this IQueryable<CartItem> cartItems)
        {
            return cartItems
                    .Include(ci => ci.Size);
        }

        public static async Task<IReadOnlyCollection<Models.CartItem>> GetCartItems(this IQueryable<CartItem> cartItems)
        {
            return await cartItems.Select(dbRec => dbRec.GetCartItem())
                                  .ToArrayAsync();
        }

        public static Models.CartItem GetCartItem(this CartItem dbRecord)
        {
            return new Models.CartItem(
                dbRecord.Id,
                dbRecord.ProductId,
                dbRecord.Product.Name,
                dbRecord.Size.Id,
                dbRecord.Size.Name,
                dbRecord.Amount,
                dbRecord.Amount * dbRecord.Product.Price
                );
        }

        public static async Task<CartItem?> GetCartItemByCartProductSize(this IQueryable<CartItem> cartItems, int cartId, int productId, int SizeId)
        {
            return await cartItems
                            .Where(ci => ci.CartId == cartId)
                            .Where(ci => ci.ProductId == productId)
                            .Where(ci => ci.SizeId == SizeId)
                            .SingleOrDefaultAsync();
        }

        public static async Task<CartItem?> GetCartItemByIdCart(this IQueryable<CartItem> cartItems, int cartItemId, int cartId)
        {
            return await cartItems
                            .Where(ci => ci.Id == cartItemId)
                            .Where(ci => ci.CartId == cartId)
                            .SingleOrDefaultAsync();
        }
    }
}
