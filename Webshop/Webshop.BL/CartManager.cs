﻿using System.Transactions;
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

        /*public async Task<IReadOnlyCollection<CartItemWithId>> ListCartItems(string sessionId)
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
        }*/

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
                }
            }
            return false;
        }
    }

    /*public async Task<bool> TryUpdateCartItem(int cartItemId, string sessionId, int amount)
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

    public async Task<bool> TryRemoveCartItem(int cartItemId, string sessionId)
    {
        using (var transaction = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                    TransactionScopeAsyncFlowOption.Enabled))
        {
            var cart = await cartRepository.GetCartBySessionIdOrNull(sessionId);
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
    }*/
}