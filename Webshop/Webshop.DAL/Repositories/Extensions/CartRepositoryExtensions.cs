using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;

namespace Webshop.DAL.Repositories.Extensions
{
    public static class CartRepositoryExtensions
    {

        public static IQueryable<Cart> FindBySessionId(this IQueryable<Cart> carts, string sessionId)
        {
            return carts
                    .Where(c => c.SessionId.Equals(sessionId));
        }

        public static async Task<bool> ExistsBySession(this IQueryable<Cart> carts, string sessionId)
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
        }

        public static async Task<Cart?> GetCartBySessionIdOrNull(this IQueryable<Cart> carts, string sessionId)
        {
            return await carts
                            .FindBySessionId(sessionId)
                            .GetCartOrNull();
        }

        public static async Task<int> GetIdBySessionId(this IQueryable<Cart> carts, string sessionId)
        {
            return await carts
                            .FindBySessionId(sessionId)
                            .GetId();
        }

        public static async Task<int> GetId(this IQueryable<Cart> carts)
        {
            return await carts
                            .Select(a => a.Id)
                            .SingleAsync();
        }

        public static async Task<Cart?> GetCartOrNull(this IQueryable<Cart> carts)
            => await carts.SingleOrDefaultAsync();
    }
}
