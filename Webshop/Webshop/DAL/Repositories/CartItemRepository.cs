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

        public async Task<(bool, int)> AddCartItem(Models.CartItem cartItem, int cartId, int productId, int sizeId)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByCartProductSize(cartId, productId, sizeId);
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
                    ProductId = productId,
                    SizeId = sizeId,
                    Amount = cartItem.Amount,
                    Price = cartItem.Product.Price * cartItem.Amount,
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

        public async Task<bool> UpdateCartItem(int cartItemId, int cartId, int amount)
        {
            var dbCartItem = await dbContext.CartItem
                                       .GetCartItemByIdCart(cartItemId, cartId);
            if (dbCartItem != null)
            {
                dbCartItem.Amount = amount;
                dbContext.CartItem.Update(dbCartItem);
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
            else
            {
                return false;
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

        public async Task<IReadOnlyCollection<Models.CartItemWithId>> ListCartItems(int cartId)
        {
            return await dbContext.CartItem
                            .WithProduct()
                            .WithSize()
                            .FilterByCart(cartId)
                            .GetCartItemsWithId();
        }
    }
}
