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

        public static async Task<IReadOnlyCollection<Models.CartItemWithId>> GetCartItemsWithId(this IQueryable<CartItem> cartItems)
        {
            return await cartItems.Select(dbRec => dbRec.GetCartItemWithId())
                                  .ToArrayAsync();
        }

        public static Models.CartItemWithId GetCartItemWithId(this CartItem dbRecord)
        {
            return new Models.CartItemWithId(
                dbRecord.Id,
                new Models.Product(
                    dbRecord.Product.Name,
                    dbRecord.Product.Price
                    ),
                dbRecord.Size.Name,
                dbRecord.Amount,
                dbRecord.Price
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
