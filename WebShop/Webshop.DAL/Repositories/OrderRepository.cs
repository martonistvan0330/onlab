using Webshop.DAL.EF;
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
    }
}
