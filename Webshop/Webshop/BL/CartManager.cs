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

        public async Task<IReadOnlyCollection<CartItemWithId>> ListCartItems(string sessionId)
        {
            var cart = await cartRepository.GetCartBySessionIdOrNull(sessionId);
            if (cart == null)
            {
                return Array.Empty<CartItemWithId>();
            }
            else
            {
                return await cartItemRepository.ListCartItems(cart.Id);
            }
        }

        public async Task<bool> TryAddCartItem(CartItem cartItem, string sessionId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var (productSuccess, productId) = await productManager.GetProductIdByName(cartItem.Product.Name);
                if (productSuccess)
                {
                    var (sizeSuccess, sizeId) = await productManager.GetSizeIdByName(cartItem.Size);
                    if (sizeSuccess)
                    {
                        var cart = await cartRepository.GetCartBySessionIdOrNull(sessionId);
                        if (cart == null)
                        {
                            var userId = await sessionManager.GetUserIdBySessionIdOrNull(sessionId);
                            cart = await cartRepository.CreateNewCart(userId.Value, sessionId);
                        }
                        var (cartItemSuccess, cartItemId) = await cartItemRepository.AddCartItem(cartItem, cart.Id, productId, sizeId);
                        if (cartItemSuccess)
                        {
                            var (stockSuccess, stock) = await productManager.GetStockByNameSize(cartItem.Product.Name, cartItem.Size);
                            var amount = await cartItemRepository.GetAmountById(cartItemId);
                            if (stockSuccess && stock >= amount)
                            {
                                transaction.Complete();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        public async Task<bool> TryUpdateCartItem(int cartItemId, string sessionId, int amount)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                var cart = await cartRepository.GetCartBySessionIdOrNull(sessionId);
                if (cart != null)
                {
                    if (await cartItemRepository.UpdateCartItem(cartItemId, cart.Id, amount))
                    {
                        var cartItem = await cartItemRepository.GetByIdOrNull(cartItemId);
                        var (stockSuccess, stock) = await productManager.GetStockByNameSize(cartItem.Product.Name, cartItem.Size.Name);
                        if (stockSuccess && stock >= amount)
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
