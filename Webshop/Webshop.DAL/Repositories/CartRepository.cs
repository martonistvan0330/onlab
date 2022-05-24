using Microsoft.EntityFrameworkCore;
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

        public async Task<Models.Cart?> GetCartByUserIdOrNull(string userId)
        {
            return await dbContext.Cart
                            .GetCartByUserIdOrNull(userId);
        }

        public async Task<double> GetTotalByUser(string userId)
        {
            return await dbContext.Cart.WithCartItems()
                                       .FindByUserId(userId)
                                       .GetTotal()
                                       .SingleOrDefaultAsync();
        }

        public async Task<Models.Cart> CreateNewCart(string userId)
        {
            var cart = new Cart()
            {
                UserId = userId,
            };

            dbContext.Cart.Add(cart);
            await dbContext.SaveChangesAsync();
            return new Models.Cart(cart.Id, cart.UserId);
        }
    }
}
