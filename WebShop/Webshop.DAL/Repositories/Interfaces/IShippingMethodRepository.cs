namespace Webshop.DAL.Repositories.Interfaces
{
    public interface IShippingMethodRepository
    {
        Task<(bool, int)> GetIdByMethod(string method);
    }
}
