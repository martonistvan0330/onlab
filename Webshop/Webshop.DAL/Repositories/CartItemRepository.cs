using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private WebshopDbContext dbContext;

        public CartItemRepository(WebshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(bool, int)> AddCartItem(Models.NewCartItem cartItem, int cartId)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByCartProductSize(cartId, cartItem.ProductId, cartItem.SizeId);
            if (dbCartItem != null)
            {
                dbCartItem.Amount += cartItem.Amount;
                dbContext.CartItem.Update(dbCartItem);
            }
            else
            {
                dbCartItem = new CartItem()
                {
                    CartId = cartId,
                    ProductId = cartItem.ProductId,
                    SizeId = cartItem.SizeId,
                    Amount = cartItem.Amount,
                };

                dbContext.CartItem.Add(dbCartItem);
            }

            try
            {
                await dbContext.SaveChangesAsync();
                return (true, dbCartItem.Id);
            }
            catch
            {
                return (false, -1);
            }
        }

        public async Task<(bool, int)> UpdateCartItem(Models.UpdateCartItem cartItem, int cartId)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByIdCart(cartItem.Id, cartId);
            if (dbCartItem != null)
            {
                dbCartItem.Amount = cartItem.Amount;
                dbCartItem.SizeId = cartItem.SizeId;
                dbContext.CartItem.Update(dbCartItem);
                try
                {
                    await dbContext.SaveChangesAsync();
                    return (true, dbCartItem.ProductId);
                }
                catch
                {
                    return (false, -1);
                }
            }
            else
            {
                return (false, -1);
            }
        }

        public async Task<bool> RemoveCartItem(int cartItemId, int cartId)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByIdCart(cartItemId, cartId);
            if (dbCartItem == null)
            {
                return true;
            }
            else
            {
                dbContext.CartItem.Remove(dbCartItem);

                try
                {
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<int> GetAmountById(int cartItemId)
        {
            return await dbContext.CartItem
                                       .Where(ci => ci.Id == cartItemId)
                                       .Select(ci => ci.Amount)
                                       .SingleOrDefaultAsync();
        }

        public async Task<CartItem?> GetByIdOrNull(int cartItemId)
        {
            return await dbContext.CartItem
                                       .WithProduct()
                                       .WithSize()
                                       .Where(ci => ci.Id == cartItemId)
                                       .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Models.CartItem>> ListCartItems(int cartId)
        {
            return await dbContext.CartItem
                            .FilterByCart(cartId)
                            .WithProduct()
                            .WithSize()
                            .GetCartItems();
        }
    }
}
