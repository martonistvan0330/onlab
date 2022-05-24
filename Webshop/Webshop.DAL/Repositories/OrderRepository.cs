using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Extensions;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebshopDbContext dbContext;

        public OrderRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<Order> CreateNewOrder(int customerId)
        {
            var order = new Order()
            {
                Date = DateTime.Now,
                Deadline = DateTime.Now,
                StatusId = 1,
                CustomerId = customerId
            };

            dbContext.Order.Add(order);
            await dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IReadOnlyCollection<Models.Order>> GetOrders(string userId)
        {
            return await dbContext.Order
                            .FilterByUserId(userId)
                            .GetOrders();
        }

        public async Task<(bool, Models.OrderDetails)> GetOrderDetails(int orderId, string userId)
        {
            var orderDetails = await dbContext.Order
                                        .WithCustomer()
                                        .WithShippingInfo()
                                        .WithPaymentInfo()
                                        .WithOrderItems()
                                        .FilterByUserId(userId)
                                        .FindById(orderId)
                                        .Select(o => o.GetOrderDetails())
                                        .SingleOrDefaultAsync();
            if (orderDetails == null)
            {
                return (false, null);
            }
            else
            {
                return (true, orderDetails);
            }
        }

		public async Task<IReadOnlyCollection<Models.ProductStockWithId>> CancelOrder(int orderId, string userId)
        {
            var order = await dbContext.Order
                                        .WithOrderItems()
                                        .FilterByUserId(userId)
                                        .FindById(orderId)
                                        .SingleOrDefaultAsync();
            if (order == null)
			{
                throw new Exception("order not found");
			}
            if (order.StatusId == 6)
			{
                throw new Exception("order already cancelled");
			}
            if (order.StatusId == 5)
            {
                throw new Exception("order already delivered");
            }
            order.StatusId = 6;
            dbContext.Order.Update(order);
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.StatusId = 6;
                dbContext.OrderItem.Update(orderItem);
            }
            try
			{
                
                await dbContext.SaveChangesAsync();
                return order.GetProductStocks();
            }
            catch
			{
                return Array.Empty<Models.ProductStockWithId>();
			}
        }
	}
}
