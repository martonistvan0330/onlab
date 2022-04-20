using System.Transactions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.BL
{
    public class OrderManager
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly SessionManager sessionManager;
        private readonly CustomerManager customerManager;
        private readonly CartManager cartManager;
        private readonly ProductManager productManager;

        public OrderManager(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            SessionManager sessionManager,
            CustomerManager customerManager,
            CartManager cartManager,
            ProductManager productManager
            )
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.sessionManager = sessionManager;
            this.customerManager = customerManager;
            this.cartManager = cartManager;
            this.productManager = productManager;
        }

        public async Task<bool> ValidateSessionId(string sessionId)
        {
            return await sessionManager.ValidateSessionId(sessionId);
        }

        public async Task<(bool, int)> TryCreateOrder(string userId, int customerId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                if (await customerManager.ExistsById(userId, customerId))
                {
                    var order = await orderRepository.CreateNewOrder(customerId);
                    var cartItems = await cartManager.ListCartItems(userId);
                    bool success = true;
                    foreach (var cartItem in cartItems)
                    {
                        var (orderItemSuccess, orderItemId) = await orderItemRepository.AddOrderItem(cartItem, order.Id, order.StatusId);
                        if (orderItemSuccess)
                        {
                            var stock = await productManager.GetStockByProductSize(cartItem.ProductId, cartItem.SizeId);
                            var amount = await orderItemRepository.GetAmountById(orderItemId);
                            if (stock >= amount)
                            {
                                if (await productManager.UpdateStock(cartItem.ProductId, cartItem.SizeId, amount))
                                {
                                    if (!(await cartManager.TryRemoveCartItem(cartItem.Id, userId)))
                                    {
                                        success = false;
                                    }
                                }
                                else
                                {
                                    success = false;
                                }
                            }
                            else
                            {
                                success = false;
                            }
                        }
                        else
                        {
                            success = false;
                        }
                    }
                    if (success)
                    {
                        transaction.Complete();
                        return (true, order.Id);
                    }
                }
                return (false, -1);
            }
        }
    }
}
