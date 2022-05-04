using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class CartRepositoryExtensions
    {

        public static IQueryable<Cart> FindByUserId(this IQueryable<Cart> carts, string userId)
        {
            return carts
                    .Where(c => c.UserId.Equals(userId));
        }

        public static IQueryable<Cart> WithCartItems(this IQueryable<Cart> carts)
        {
            return carts.Include(c => c.CartItems)
                        .ThenInclude(ci => ci.Product);
        }

        public static IQueryable<double> GetTotal(this IQueryable<Cart> carts)
        {
            return carts.Select(c => c.CartItems.Sum(ci => ci.Amount * ci.Product.Price));
        }

        /*public static async Task<bool> ExistsBySession(this IQueryable<Cart> carts, string sessionId)
        {
            var dbCart = await carts
                                .GetCartBySessionIdOrNull(sessionId);
            if (dbCart == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/

        public static async Task<Models.Cart?> GetCartByUserIdOrNull(this IQueryable<Cart> carts, string userId)
        {
            return await carts
                            .FindByUserId(userId)
                            .GetCartOrNull();
        }

        /*public static async Task<int> GetIdBySessionId(this IQueryable<Cart> carts, string sessionId)
        {
            return await carts
                            .FindBySessionId(sessionId)
                            .GetId();
        }*/

        public static async Task<int> GetId(this IQueryable<Cart> carts)
        {
            return await carts
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<Models.Cart?> GetCartOrNull(this IQueryable<Cart> carts)
            => await carts.Select(dbRecord => new Models.Cart(dbRecord.Id, dbRecord.UserId)).SingleOrDefaultAsync();
    }
}
