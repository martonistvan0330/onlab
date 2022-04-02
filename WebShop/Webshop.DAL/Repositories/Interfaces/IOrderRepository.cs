namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<EF.Order> CreateNewOrder(int customerId);
    }
}
