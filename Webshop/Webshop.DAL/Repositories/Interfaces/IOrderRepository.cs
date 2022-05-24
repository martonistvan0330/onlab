namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyCollection<Models.Order>> GetOrders(string userId);
        Task<EF.Order> CreateNewOrder(int customerId);
        Task<(bool, Models.OrderDetails)> GetOrderDetails(int orderId, string userId);
        Task<IReadOnlyCollection<Models.ProductStockWithId>> CancelOrder(int orderId, string userId);
    }
}
