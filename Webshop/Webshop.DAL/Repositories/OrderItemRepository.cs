using Microsoft.EntityFrameworkCore;
using Webshop.DAL.EF;
using Webshop.DAL.Repositories.Interfaces;

namespace Webshop.DAL.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly WebshopDbContext dbContext;

        public OrderItemRepository(WebshopDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<(bool, int)> AddOrderItem(CartItem cartItem, int orderId, int statusId)
        {
            var dbOrderItem = new OrderItem()
            {
                Amount = cartItem.Amount,
                //Price = cartItem.Price,
                OrderId = orderId,
                ProductId = cartItem.ProductId,
                StatusId = statusId,
                SizeId = cartItem.SizeId,
            };

            dbContext.OrderItem.Add(dbOrderItem);

            try
            {
                await dbContext.SaveChangesAsync();
                return (true, dbOrderItem.Id);
            }
            catch
            {
                return (false, -1);
            }
        }

        public async Task<int> GetAmountById(int orderItemId)
        {
            return await dbContext.OrderItem
                                       .Where(oi => oi.Id == orderItemId)
                                       .Select(oi => oi.Amount)
                                       .SingleOrDefaultAsync();
        }
    }
}
