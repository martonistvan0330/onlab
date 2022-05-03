using System.Transactions;
using Webshop.DAL.Models;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class CartManager
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly SessionManager sessionManager;
        private readonly ProductManager productManager;

        public CartManager(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            SessionManager sessionManager,
            ProductManager productManager
            )
        {
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
            this.sessionManager = sessionManager;
            this.productManager = productManager;
        }

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await sessionManager.ValidateSessionId(sessionId);
        }

        public async Task<DAL.EF.CartItem?> GetCartItemByIdOrNull(int cartItemId)
        {
            return await cartItemRepository.GetByIdOrNull(cartItemId);
        }

        public async Task<IReadOnlyCollection<CartItem>> ListCartItems(string userId)
        {
            var cart = await cartRepository.GetCartByUserIdOrNull(userId);
            if (cart == null)
            {
                return Array.Empty<CartItem>();
            }
            else
            {
                return await cartItemRepository.ListCartItems(cart.Id);
            }
        }

        public async Task<bool> TryAddCartItem(NewCartItem cartItem, string userId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var cart = await cartRepository.GetCartByUserIdOrNull(userId);
                if (cart == null)
                {
                    cart = await cartRepository.CreateNewCart(userId);
                }
                var (cartItemSuccess, cartItemId) = await cartItemRepository.AddCartItem(cartItem, cart.Id);
                if (cartItemSuccess)
                {
                    var stock = await productManager.GetStockByProductSize(cartItem.ProductId, cartItem.SizeId);
                    var amount = await cartItemRepository.GetAmountById(cartItemId);
                    if (stock >= amount)
                    {
                        transaction.Complete();
                        return true;
                    }
                    else
					{
                        throw new Exception("not enough items in stock");
					}
                }
            }
            return false;
        }

        public async Task<bool> TryUpdateCartItem(UpdateCartItem cartItem, string userId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var cart = await cartRepository.GetCartByUserIdOrNull(userId);
                if (cart != null)
                {
                    var (updateSuccess, productId) = await cartItemRepository.UpdateCartItem(cartItem, cart.Id);
                    if (updateSuccess)
                    {
                        var stock = await productManager.GetStockByProductSize(productId, cartItem.SizeId);
                        var amount = await cartItemRepository.GetAmountById(cartItem.Id);
                        if (stock >= amount)
                        {
                            transaction.Complete();
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public async Task<bool> TryRemoveCartItem(int cartItemId, string userId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var cart = await cartRepository.GetCartByUserIdOrNull(userId);
                if (cart != null)
                {
                    if (await cartItemRepository.RemoveCartItem(cartItemId, cart.Id))
                    {
                        var cartItem = await cartItemRepository.GetByIdOrNull(cartItemId);
                        if (cartItem == null)
                        {
                            transaction.Complete();
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}