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

        public async Task<bool> TryCreateOrder(int customerId, string sessionId)
        {
            using (var transaction = new TransactionScope(
                        TransactionScopeOption.Required,
                        new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead },
                        TransactionScopeAsyncFlowOption.Enabled))
            {
                if (await customerManager.ExistsById(customerId, sessionId))
                {
                    var order = await orderRepository.CreateNewOrder(customerId);
                    var cartItems = await cartManager.ListCartItems(sessionId);
                    bool success = true;
                    foreach (var cartItem in cartItems)
                    {
                        var dbCartItem = await cartManager.GetCartItemByIdOrNull(cartItem.Id);
                        if (dbCartItem != null)
                        {
                            var (orderItemSuccess, orderItemId) = await orderItemRepository.AddOrderItem(dbCartItem, order.Id, order.StatusId);
                            if (orderItemSuccess)
                            {
                                var (stockSuccess, stock) = await productManager.GetStockByNameSize(cartItem.Product.Name, cartItem.Size);
                                var amount = await orderItemRepository.GetAmountById(orderItemId);
                                if (!(stockSuccess && stock >= amount))
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
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
