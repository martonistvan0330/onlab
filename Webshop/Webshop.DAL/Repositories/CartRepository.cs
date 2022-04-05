using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private WebshopDbContext dbContext;

        public CartRepository(WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Cart?> GetCartBySessionIdOrNull(string sessionId)
        {
            return await dbContext.Cart
                            .GetCartBySessionIdOrNull(sessionId);
        }

        public async Task<Cart> CreateNewCart(int userId, string sessionId)
        {
            var cart = new Cart()
            {
                UserId = userId,
                SessionId = sessionId,
            };

            dbContext.Cart.Add(cart);
            await dbContext.SaveChangesAsync();
            return cart;
        }
    }
}
